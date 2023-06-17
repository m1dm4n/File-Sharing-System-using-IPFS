from __future__ import absolute_import

import asyncio
from .IOManager import IOManager, ReaderWriter
from typing import Callable, Tuple, Any

class ServerHandle:
    def __init__(self, main: Callable[[ReaderWriter, Tuple[str, int]], Any], ip: str, port: int, DEBUG=False):
        self.ip = ip
        self.port = port
        self.main = main
        self.DEBUG = DEBUG

    async def serve_forever(self):
        server = await asyncio.start_server(
            self.handle_connection,
            self.ip,
            self.port
        )
        print(f"Node started at {self.ip}:{self.port}")

        async with server:
            await server.serve_forever()
        server.close()
        await server.wait_closed()

    async def handle_connection(self, reader: asyncio.StreamReader, writer: asyncio.StreamWriter):
        address = writer.get_extra_info('peername')
        print(f"Accepted connection from {address}")
        try:
            Client_IO = IOManager(
                f"{address[0]}:{address[1]}", color="\x1b[31m", DEBUG=self.DEBUG
            )
            Server_IO = IOManager(f"Server", color="\x1b[34m", DEBUG=self.DEBUG)
            RW = ReaderWriter(Client_IO, Server_IO)
            await asyncio.gather(
                (self.main(
                    RW,
                    (self.ip, self.port)
                )),
                self._pipe_outbound(RW, writer),
                self._pipe_inbound(reader, RW),
                return_exceptions=True
            )
        except Exception as e:
            print("Something wrong:", str(e))
        finally:
            print(f"Closed connection from {address}")
            # writer.write_eof()
            writer.close()
            await writer.wait_closed()

    async def _pipe_outbound(self, manager: ReaderWriter, writer: asyncio.StreamWriter):
        while True:
            chunk = await manager.writer.queue.get()
            if chunk is None:
                break
            writer.write(chunk)
            await writer.drain()
        print("Stdout end!")

    async def _pipe_inbound(self, reader: asyncio.StreamReader, manager: ReaderWriter):
        while True:
            data = await reader.read(1024)
            if len(data) == 0:
                break
            await manager.reader.write(data)
        await manager.close()
        print("Stdin end!")
