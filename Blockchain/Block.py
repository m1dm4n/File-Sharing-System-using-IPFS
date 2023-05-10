from hashlib import sha256
import json

class Block:
    def __init__(self, index, transactions, timestamp, previous_hash, nonce=0):
        self.index = index
        self.transactions = transactions
        self.timestamp = timestamp
        self.previous_hash = previous_hash
        self.nonce = nonce

    def compute_hash(self):
        """
        A function that return the hash of the block contents.
        """
        block_string = json.dumps(self.__dict__, sort_keys=True)
        return sha256(block_string.encode()).hexdigest()

    def __hash__(self) -> int:
        """
        For build-in object that require Hashing like set(), dict()
        """
        return int(self.compute_hash(), 16)

    def __str__(self) -> str:
        return json.dumps(self.__dict__, sort_keys=True)
