# Add some small change for reaching EOF

from asyncio.queues import Queue
from os import getenv
from sys import stdout
from typing import Optional
from hexdump import hexdump
from asyncio import StreamReader, StreamWriter


class IOManager:
    name: str
    color: str
    closed: bool
    queue: Queue[Optional[bytes]]
    buffer: bytes
    cached: bool

    def __init__(self, name: str, color: Optional[str] = None, DEBUG=False):
        self.name = name
        self.color = color if color is not None else ""
        self.closed = False
        self.queue = Queue()
        self.buffer = b""
        self.DEBUG = DEBUG
        # self.queue.task_done()

    async def write(self, data: bytes) -> None:
        if self.DEBUG:
            print(f"{self.color}[{self.name}] writing {len(data)} bytes")
            hexdump(data)
            stdout.write("\x1b[0m")
            stdout.flush()
        await self.queue.put(data)

    async def read(self, size: int) -> bytes:
        if self.closed:
            raise Exception("Unexpected EOF")
        while len(self.buffer) < size:
            chunk = await self.queue.get()
            if chunk is None:
                self.closed = True
                break
            self.buffer += chunk
        if len(self.buffer) < size:
            raise Exception("Unexpected EOF")
        data = self.buffer[:size]
        self.buffer = self.buffer[size:]
        return data

    async def readuntil(self, delimeter: bytes, drop=False) -> bytes:
        if self.closed:
            raise EOFError()
        while delimeter not in self.buffer:
            chunk = await self.queue.get()
            if chunk is None:
                self.closed = True
                break
            self.buffer += chunk
        datas = self.buffer.split(delimeter, 1)
        data = datas[0]
        self.buffer = datas[-1]
        return data if drop else data + delimeter

    async def readline(self, keepends=True) -> bytes:
        return await self.readuntil(b'\n', drop=(not keepends))

    async def read_eof(self) -> bytes:
        if self.closed:
            raise EOFError()
        while True:
            chunk = await self.queue.get()
            if chunk is None:
                self.closed = True
                break
            self.buffer += chunk
        return self.buffer


class ReaderWriter:
    reader: IOManager
    writer: IOManager

    def __init__(self, reader: IOManager, writer: IOManager):
        self.reader = reader
        self.writer = writer

    async def read(self, size: int) -> bytes:
        return await self.reader.read(size)

    async def readuntil(self, delimeter: bytes, drop=False) -> bytes:
        return await self.reader.readuntil(delimeter, drop)

    async def readline(self, keepends=True) -> bytes:
        return await self.reader.readline(keepends)

    async def read_eof(self) -> bytes:
        return await self.reader.read_eof()
    
    async def close(self) -> None:
        await self.reader.queue.put(None)
        await self.writer.queue.put(None)
        

    async def write(self, data: bytes) -> None:
        await self.writer.write(data)

    async def writeline(self, data: bytes) -> None:
        await self.writer.write(data + b'\n')

    async def write_eof(self) -> None:
        await self.writer.queue.put(None)


# class ReaderWriter:
#     reader: StreamReader
#     writer: StreamWriter

#     def __init__(self, reader: StreamReader, writer: StreamWriter):
#         self.reader = reader
#         self.writer = writer

#     async def read(self, size: int=-1) -> bytes:
#         return await self.reader.read(size)

#     async def readuntil(self, delimeter: bytes, drop=False) -> bytes:
#         data = await self.reader.readuntil(delimeter)
#         return data if (not drop) else data.rstrip(delimeter)

#     async def readline(self, keepends=True) -> bytes:
#         data = await self.reader.readline()
#         return data if keepends else data[:-1]


#     async def write(self, data: bytes) -> None:
#         await self.writer.write(data)

#     async def writeline(self, data: bytes) -> None:
#         await self.writer.write(data + b'\n')

#     async def write_eof(self) -> None:
#         await self.writer.write_eof()
