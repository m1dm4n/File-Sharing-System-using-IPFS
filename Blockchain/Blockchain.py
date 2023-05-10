from .Block import *
import time

NULL_HASH = '6e340b9cffb37a989ca544e6bb780a2c78901d3fb33738768511a30617afa01d'


class Blockchain:
    # Default difficulty of our PoW algorithm
    difficulty = 2

    def __init__(self, chain=None, difficulty=None):
        if isinstance(difficulty, int) and difficulty > 0:
            Blockchain.difficulty = difficulty
        self.unconfirmed_TX = []
        self.SharedChain = chain
        if self.SharedChain is None:
            self.SharedChain = []
            self.CreateGenesisBlock()

    def CreateGenesisBlock(self):
        """
        A function to generate genesis block and appends it to
        the chain. The block has index 0, previous_hash as 0, and
        a valid hash.
        """
        GenesisBlock = Block(0, [], 0, NULL_HASH)
        GenesisBlock.hash = GenesisBlock.compute_hash()
        self.SharedChain.append(GenesisBlock)

    @property
    def LastBlock(self):
        return self.SharedChain[-1]

    def AddNewBlock(self, block, proof):
        """
        A function that adds the block to the chain after verification.
        Verification includes:
        * Checking if the proof is valid.
        * The previous_hash referred in the block and the hash of latest block
          in the chain match.
        """
        previous_hash = self.LastBlock.hash

        if previous_hash != block.previous_hash:
            raise ValueError("Previous hash incorrect")

        if not Blockchain.IsValidProof(block, proof):
            raise ValueError("Block proof invalid")

        block.hash = proof
        self.SharedChain.append(block)

    @staticmethod
    def ProofOfWork(block):
        """
        Function that tries different values of nonce to get a hash
        that satisfies our difficulty criteria.
        """
        block.nonce = 0

        computed_hash = block.compute_hash()
        target = '0' * Blockchain.difficulty
        while not computed_hash.startswith(target):
            block.nonce += 1
            computed_hash = block.compute_hash()

        return computed_hash

    def AddPendingTransaction(self, transaction):
        self.unconfirmed_TX.append(transaction)

    @classmethod
    def IsValidProof(cls, block, block_hash):
        """
        Check if block_hash is valid hash of block and satisfies
        the difficulty criteria.
        """
        return (block_hash.startswith('0' * Blockchain.difficulty) and
                block_hash == block.compute_hash())

    @classmethod
    def VeriFyChain(cls, chain):
        result = True
        previous_hash = NULL_HASH

        for block in chain:
            block_hash = block.hash
            # remove the hash field to recompute the hash again
            # using `compute_hash` method.
            delattr(block, "hash")

            if not cls.IsValidProof(block, block_hash) or \
                    previous_hash != block.previous_hash:
                result = False
                break

            block.hash, previous_hash = block_hash, block_hash

        return result

    def MineNewBlock(self):
        """
        This function serves as an interface to add the pending
        transactions to the blockchain by adding them to the block
        and figuring out Proof Of Work.
        """
        if not self.unconfirmed_TX:
            return False

        LastBlock = self.LastBlock

        new_block = Block(
            index=LastBlock.index + 1,
            transactions=self.unconfirmed_TX,
            timestamp=time.time(),
            previous_hash=LastBlock.hash
        )

        proof = self.ProofOfWork(new_block)
        self.AddNewBlock(new_block, proof)

        self.unconfirmed_TX = []

        return True
