from time import time
from helper import jsondumps, jsonloads

class DHTCommand():
    PUT = 1
    GET = 2
    DEL = 3
    HASKEY = 4
    PURGE = 5
    LEAVE = 6
    REMOVE = 7
    JOIN = 8
    ADDNODE = 9
    WHEREIS = 10
    BALANCE = 11
    HTTPGET = 12
    HTTPGETKEY = 13
    UNKNOWN = 99
    allcommands = \
        {1: "PUT",
         2: "GET",
         3: "DEL",
         4: "HASKEY",
         5: "PURGE",
         6: "LEAVE",
         7: "REMOVE",
         8: "JOIN",
         9: "ADDNODE",
         10: "WHEREIS",
         11: "BALANCE",
         12: "HTTPGET",
         13: "HTTPGETKEY",
         99: "UNKNOWN"}
    SEPARATOR = chr(30)  # This is the ASCII 30-character aka record delimiter

    def __init__(self, action=None, key="", value="", timestamp=None):
        """ Initialize a command with `key`, `action` and `value`
            if value is a file object read through it to get the size
            and then rewind it.
            `timestamp` is seconds since epoch and it will be set
            to current time if None.
        """
        if action is not None and action not in self.allcommands:
            raise Exception("Invalid command:", action)
        self.action = action or self.UNKNOWN
        self.key = str(key)
        self.forwarded = False
        self.timestamp = timestamp or time()
        self.value = str(value)

    @staticmethod
    def from_json(command):
        """ Parse a json to DHTCommand object
        """
        dhtc = DHTCommand()
        dhtc.action = command.get("action", DHTCommand.UNKNOWN)
        if isinstance(dhtc.action, str):
            dhtc.action = DHTCommand.allcommands.get(
                dhtc.action, 
                DHTCommand.UNKNOWN
            )
        dhtc.key = str(command.get("key", ""))
        dhtc.value = str(command.get("value", ""))
        dhtc.forwarded = command.get("forwarded", False)
        dhtc.timestamp = command.get("timestamp", time())
        return dhtc

    @staticmethod
    def parse(command):
        return jsonloads(command, object_hook=DHTCommand.from_json)

    def getmessage(self):
        """ Returns a padded message consisting of `size`:`command`:`value`:0...
        """
        return self.__str__()

    def __str__(self):
        """ Return a reader friendly representation of the message
        """
        return jsondumps(self.__dict__, ensure_ascii=True, sort_keys=True)
