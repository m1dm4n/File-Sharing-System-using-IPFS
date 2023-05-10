import asyncio
from IOManager import IOManager, ReaderWriter
from typing import Callable, Any


class NetworkNode:
    def __init__(self, main: Callable[[ReaderWriter], Any], ip: str, port: int):
        self.ip = ip
        self.port = port
        self.main = main

    async def start(self):
        server = await asyncio.start_server(
            self.handle_connection,
            self.ip,
            self.port
        )
        print(f"Node started at {self.ip}:{self.port}")

        async with server:
            await server.serve_forever()

    async def Stdin_to_Manager(self, manager: IOManager, reader: asyncio.StreamReader):
        while True:
            data = await reader.read(1024)
            if len(data) == 0 or data.strip() == b'EOF':
                break
            await manager.write(data)
        await manager.queue.put(None)
        print('Stdin end')

    async def Manager_to_Stdout(self, manager: IOManager, writer: asyncio.StreamWriter):
        while True:
            chunk = await manager.queue.get()
            if chunk is None:
                break
            writer.write(chunk)
            await writer.drain()
        print('Stdout end')

    async def handle_connection(self, reader: asyncio.StreamReader, writer: asyncio.StreamWriter):
        address = writer.get_extra_info('peername')
        print(f"Accepted connection from {address}")
        Client_IO = IOManager(f"{address[0]}:{address[1]}", color="\x1b[31m")
        Server_IO = IOManager(f"{self.ip}:{self.port}", color="\x1b[34m")
        main_task = self.main(ReaderWriter(Client_IO, Server_IO))
        Client_task = self.Stdin_to_Manager(Client_IO, reader)
        Server_task = self.Manager_to_Stdout(Server_IO, writer)
        await asyncio.gather(main_task, Client_task, Server_task)
        writer.write_eof()
        await writer.drain()
        await writer.wait_closed()
        print(f"Closed connection from {address}")
