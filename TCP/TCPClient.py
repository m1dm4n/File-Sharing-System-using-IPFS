import asyncio
from .IOManager import ReaderWriter, IOManager


class RemoteHandle:
    address: str
    port: int
    started: bool
    RW: ReaderWriter

    def __init__(self, address: str, port: int):
        self.address = address
        self.port = port
        self.RW = None
        print(f"Opening connection to {self.address}:{self.port}")
        Server_IO = IOManager(f"{self.address}:{self.port}", color="\x1b[31m")
        Client_IO = IOManager(f"Client", color="\x1b[34m")
        self.RW = ReaderWriter(Server_IO, Client_IO)
        asyncio.create_task(self.start())

    def IOStream(self) -> ReaderWriter:
        return self.RW

    async def start(self) -> None:
        reader, writer = await asyncio.open_connection(self.address, self.port)
        try:
            await asyncio.gather(
                self._pipe_inbound(reader, self.RW.reader),
                self._pipe_outbound(self.RW.writer, writer)
            )
        except Exception as e:
            print("Error: ", str(e))

    async def _pipe_inbound(self, reader: asyncio.StreamReader, manager: IOManager):
        while not reader.at_eof():
            data = await reader.read(1024)
            await manager.write(data)
        await manager.queue.put(None)
        print('Stdin end')

    async def _pipe_outbound(self, manager: IOManager, writer: asyncio.StreamWriter):
        while True:
            chunk = await manager.queue.get()
            if chunk is None:
                break
            writer.write(chunk)
            await writer.drain()
        print('Stdout end')
        writer.close()
