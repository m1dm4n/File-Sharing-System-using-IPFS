import asyncio
import json
import sys
import time
from typing import Dict, List, Optional
from copy import copy
from Blockchain.Block import Block
from TCP.IOManager import ReaderWriter
from TCP.TCPClient import RemoteHandle
from TCP.TCPServer import ServerHandle
from Blockchain.Blockchain import Blockchain

FAILED_CODE = -1
SUCCESS_CODE = 1
blockchain = Blockchain(difficulty=4)
peers = set()
HOST = sys.argv[1]
PORT = int(sys.argv[2])


async def query(action: str, args: List[Optional[str]], endpoint):
    io = RemoteHandle(*endpoint).IOStream()
    await io.writeline(action.encode())
    for arg in args:
        await io.writeline(arg.encode())
    result = await io.readline(0)
    await io.writeline(b'Quit')
    return json.loads(result)

def _jsontify(data: Dict) -> str:
    return json.dumps(data, sort_keys=True)


def return_format(status: int, **kwargs):
    data = {}
    data['status'] = status
    for key, value in kwargs.items():
        data[key] = value
    return data

def create_chain_from_dump(chain_dump) -> Blockchain:
    generated_blockchain = Blockchain()
    for idx, block_data in enumerate(chain_dump):
        if idx == 0:
            continue  # skip genesis block
        block = Block(block_data["index"],
                      block_data["transactions"],
                      block_data["timestamp"],
                      block_data["previous_hash"],
                      block_data["nonce"])
        proof = block_data["hash"]
        generated_blockchain.AddNewBlock(block, proof)
    return generated_blockchain


async def new_transaction(tx_data):
    required_fields = ["from", "data"]
    for field in required_fields:
        if not tx_data.get(field):
            return return_format(FAILED_CODE, message="Invalid transaction data")
    tx_data["timestamp"] = time.time()
    blockchain.AddPendingTransaction(tx_data)
    return return_format(SUCCESS_CODE)


async def get_chain():
    chain_data = []
    for block in blockchain.SharedChain:
        cur = copy(block)
        cur.hash = cur.compute_hash()
        tmp =  cur.__dict__
        chain_data.append(tmp)
    return return_format(
        SUCCESS_CODE,
        length=len(chain_data),
        chain=chain_data,
        peers=list(peers)
    )



async def mine_unconfirmed_transactions():
    result = blockchain.MineNewBlock()
    if not result:
        return return_format(FAILED_CODE, message="No transactions to mine")
    else:
        # Making sure we have the longest chain before announcing to the network
        chain_length = len(blockchain.SharedChain)
        await consensus()
        if chain_length == len(blockchain.SharedChain):
            # announce the recently mined block to the network
            await announce_new_block(blockchain.LastBlock)
        return return_format(
            SUCCESS_CODE, 
            message="Block #{} is mined.".format(blockchain.LastBlock.index)
        )


async def register_new_peers(node_address):
    if not node_address:
        return return_format(FAILED_CODE, message="Invalid data")

    # Add the node to the peer list        
    peers.add(node_address)

    # Return the consensus blockchain to the newly registered node
    # so that he can sync
    return await get_chain()


async def register_with_existing_node(Input):
    """
    Internally calls the `register_node` endpoint to
    register current node with the node specified in the
    request, and sync the blockchain as well as peer data.
    """
    if 'node_address' not in Input:
        return return_format(FAILED_CODE, message="Invalid data")
    node_address = Input['node_address'].split(':', 1)
    if len(node_address) != 2 and not node_address[1].isdigit():
        return return_format(FAILED_CODE, message="Invalid data")

    # Make a request to register with remote node and obtain information
    response = await query("register_node", [], node_address)
    if response['status'] == SUCCESS_CODE:
        global blockchain
        global peers
        # update chain and the peers
        chain_dump = response['chain']
        blockchain = create_chain_from_dump(chain_dump)
        for peer in response['peers']:
            peers.add(tuple(peer))
        return return_format(SUCCESS_CODE, message="Joining successful")
    else:
        # if something goes wrong, pass it on to the API response
        return response


async def verify_and_add_block(block_data):
    try:
        block = Block(block_data["index"],
                      block_data["transactions"],
                      block_data["timestamp"],
                      block_data["previous_hash"],
                      block_data["nonce"])
        proof = block.compute_hash()
        blockchain.AddNewBlock(block, proof)
    except ValueError as e:
        return return_format(FAILED_CODE, message="The block was discarded by the node: " + str(e))
    except KeyError:
        return return_format(FAILED_CODE, message="Your block format is invalid!")

    return return_format(SUCCESS_CODE)


async def get_pending_tx():
    return return_format(SUCCESS_CODE, unconfirmed_transactions=blockchain.unconfirmed_TX)


async def consensus():
    """
    Our naive consnsus algorithm. If a longer valid chain is
    found, our chain is replaced with it.
    """
    global blockchain

    longest_chain = None
    current_len = len(blockchain.SharedChain)

    for node in peers:
        response = await query('chain', [], node)
        length = response['length']
        if length > current_len and Blockchain.VeriFyChain(response['chain']):
            current_len = length
            longest_chain = create_chain_from_dump(response['chain'])

    if longest_chain:
        blockchain = longest_chain
        return True

    return False

async def announce_new_block(block):
    """
    A function to announce to the network once a block has been mined.
    Other blocks can simply verify the proof of work and add it to their
    respective chains.
    """
    for peer in peers:
        res = await query('add_block', [json.dumps(block.__dict__, sort_keys=True)], peer)


async def action(rw: ReaderWriter, Endpoint) -> None:
    while True:
        out = None
        action = (await rw.readline(0)).decode()
        match action:
            case 'new_transaction':
                out = await new_transaction(json.loads(await rw.readline(0)))
            case 'chain':
                out = await get_chain()
            case 'mine':
                out = await mine_unconfirmed_transactions()
            case 'register_node':
                out = await register_new_peers(Endpoint)
            case 'register_with':
                out = await register_with_existing_node(json.loads(await rw.readline(0)))
            case 'add_block':
                out = await verify_and_add_block(json.loads(await rw.readline(0)))
            case 'Quit':
                break
            case _:
                continue
        await rw.writeline(_jsontify(out).encode())
    await rw.write_eof()


async def main(ip='127.0.0.1', port=5000):
    # Create and start the network nodes
    node1 = ServerHandle(action, ip, port)
    await node1.start()

if __name__ == '__main__':

    asyncio.run(main(HOST, PORT))
