import threading
from helper import *
from dhtcommand import DHTCommand
from StorageHashTable import StorageHashTable

class MyDHTTable():
    """ Represents the hash table
        This is really just a dictionary with some convenience methods.
        Most of it is used to render a html-page for debugging purposes.
    """
    def __init__(self,server_name,hash_ring):
        self._map = StorageHashTable()
        self._timemap = {}
        self.hash_ring = hash_ring
        self.server_name = server_name
        self._lock = threading.RLock()

    def __str__(self):
        """ Returns a string representation of the map
        """
        values = []
        for key in self._map.keys():
            values.append(key + ": " + self._map[key])
        return "\n".join(values)

    def get_keys(self):
        """ Returns all keys currently in the map
            Lock first, just in case
        """
        self._lock.acquire()
        keys = self._map.keys()
        self._lock.release()
        return keys
    
    def getsizewithsuffix(self,size):
        """ Adds a suffix to `size` and returns
            "`size` suffix"
        """
        if size > 1024*1024*1024:
            return str(size/(1024*1024*1024)) + " GB"
        elif size > 1024*1024:
            return str(size/(1024*1024)) + " MB"
        elif size > 1024:
            return str(size/1024) + " KB"
        else:
            return str(size) + " B"

    def perform(self,command):
        """ Perform `command` on this map
            return BAD_COMMAND if the command is invalid
        """
        status = {"success": False}
        self._lock.acquire()
        if command.action == DHTCommand.PUT:
            """ Put key and value in map """
            _value = bytes.fromhex(command.value)
            if Hashing(_value, True) != command.key:
                status["status"] = "loss of data integrity".upper()
            else:
                status["success"] = True
                self._map[command.key] = _value
                self._timemap[command.key] = command.timestamp
                status["status"] = "PUT OK "+command.key

        elif command.action == DHTCommand.GET or command.action == DHTCommand.HTTPGETKEY:
            """ Get value from map if key exists """
            _value = self._map.get(command.key, None).hex()
            if _value is not None:
                status["success"] = True
                status["value"] = _value
            else:
                status["status"] = "ERR_VALUE_NOT_FOUND"

        elif command.action == DHTCommand.DEL:
            """ Delete key from map if it exists """
            if command.key in self._map:
                del self._map[command.key]
                status["success"] = True
                status["status"] = "DEL OK "+command.key
            else:
                status["status"] = "ERR_VALUE_NOT_FOUND"

        elif command.action == DHTCommand.HASKEY:
            """ Return the timestamp if key is found, else 0.0 (epoch) """
            _value = self._map.get(command.key, None)
            if _value is not None:
                status["success"] = True
                status["timestamp"] = _value
            else: 
                status["timestamp"]= 0.0

        elif command.action == DHTCommand.PURGE:
            """ Remove all keys in this map that don't belong here """
            for key in self._map.keys():
                if self.server_name not in self.hash_ring.get_replicas(key):
                    del self._map[key]
            status["success"] = True
            status["status"] = "PURGE ok"
        else:
            status["status"] = "BAD_COMMAND: "+str(command)

        self._lock.release()
        return status