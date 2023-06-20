from helper import Hashing
from os import remove as __fileremove, path

PREFIX = "/".join(str(__file__).split("/")[:-1]) + '/HashTableValue/'


class StorageHashTable:
    def __init__(self) -> None:
        self._map = {}
        self._prefix = PREFIX
    def get(self, key, default=None):
        try:
            name = self._map.get(key, "")
            data = self.__read_from_file(name)
            assert Hashing(data, True) == str(key)
        except Exception:
            data = default
        return data

    def keys(self):
        return self._map.keys()

    def __read_from_file(self, name):
        with open(name, "rb") as f:
            data = f.read()
        return data

    def __contains__(self, key):
        return key in self._map

    def __getitem__(self, key):
        value = self.get(key)
        if value is None:
            raise KeyError()
        return value

    def __setitem__(self, key, value):
        _name = self._prefix + str(key)
        self._map[key] = _name
        try:
            with open(_name, "wb") as f:
                f.write(value)
        except Exception:
            raise ValueError()

    def __delitem__(self, key):
        _name = self.__getitem__(key)
        __fileremove(_name)

    def __iter__(self):
        for _key, _name in self._map.items():
            yield _key, self.__read_from_file(_name)

