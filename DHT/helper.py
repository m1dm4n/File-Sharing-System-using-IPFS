from json import loads as jsonloads, dumps as jsondumps
import asyncio
import hashlib

def Hashing(data,ishex=False):
    if isinstance(data, str):
        data = data.encode()
    elif all(not isinstance(data, _t) for _t in [bytes, bytearray]):
        data = str(data).encode()
    H = hashlib.sha256()
    H.update(data)
    return H.hexdigest() if ishex else H.digest()
