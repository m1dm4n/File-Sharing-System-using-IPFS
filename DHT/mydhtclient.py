from _socket import *
import logging
from socket import error as socket_error
import sys
from HashRing import Server
from cmdapp import CmdApp
from dhtcommand import DHTCommand
from helper import *


class MyDHTClient(CmdApp):
    def __init__(self, verbose=False, logfile=None):
        """A MyDHT client for interacting with MyDHT servers
        """
        CmdApp.__init__(self, verbose=verbose, logfile=logfile)
        self.usage = \
            """
            -h, --hostname
              specify hostname (default: localhost)
            -p, --port
              specify port (default: 50140)
            -c, --command
              put, get, del, haskey, purge, remove, whereis, balance
            -k, --key
              specify key
            -val, --value
              specify a (string) value
            -f, --file
              specify a file value
"""

    def send_to_socket(self, data, socket):
        """ Send all `data` to `socket`
        """
        socket.sendall(data + b'!endf!')


    def read_from_socket(self, socket):
        """  Read data from `socket`
        """
        buffer = b""
        while True:
            request_data: bytes = socket.recv(4096)
            buffer += request_data
            if request_data.endswith(b'!endf!'):
                buffer = buffer.rstrip(b'!endf!')
                break
        return buffer

    def sendcommand(self, server, command: DHTCommand):
        """ Sends a `command` to a `server` in the ring
        """

        # If command isn't already a DHTCommand, create one

        for retry in range(3):
            logging.debug("sending command to: %s %s try number: %d", str(server), str(command), retry)
            sock = socket(AF_INET, SOCK_STREAM)

            try:
                sock.connect((server.bindaddress()))
                # If value send the command and the size of value
                self.send_to_socket(str(command).encode(), sock)
                # Send value to another server

                data = self.read_from_socket(sock)

                sock.close()
                return jsonloads(data)
            except Exception as E:
                sock.close()
                logging.error("Error connecting to server: %s", str(E))
            
        logging.error(
            "Server (%s) did not respond during 3 tries, giving up", str(server))
        return None

    def cmdlinestart(self):
        """ Parse command line parameters and start client
        """
        try:
            port = int(self.getarg("-p") or self.getarg("--port", 50140))
            host = self.getarg("-h") or self.getarg("--hostname", "localhost")
            key = self.getarg("-k") or self.getarg("--key")
            server = Server(host, port)
            command = self.getarg('-c') or self.getarg('--command')
            value = self.getarg("-val") or self.getarg("--value")
            file = self.getarg("-f") or self.getarg("--file")
            outfile = self.getarg("-o") or self.getarg("--outfile")

            logging.debug("command: %s %s %s %s", str(
                server), command, key, value)
            if command is None or server is None or file and value:
                self.help()

            command = self.get_command(command)
            if file:
                f = open(file, "rb")
                command = DHTCommand(command, key, f)
            else:
                command = DHTCommand(command, key, value)

            if outfile:
                # File was an argument, supply it
                with open(outfile, "wb") as out:
                    print(self.sendcommand(server, command, out))
            else:
                # Print output
                data = self.sendcommand(server, command)
                print(data)
            if file:
                f.close()
        except TypeError:
            self.help()


if __name__ == "__main__":
    (MyDHTClient().cmdlinestart())
