import glob
import unittest
from HashRing import Server
from dhtcommand import DHTCommand
from mydhtclient import MyDHTClient
from helper import Hashing

HOST = 1338



class TestMyDHT(unittest.TestCase):

    def setUp(self):
        host = "localhost"
        self.ports = [HOST]  # range(50140,50144)
        self.servers = []
        for port in self.ports:
            self.servers.append(Server(host, port))
        self.dht = MyDHTClient(True)

    def testUploadFiles(self):
        """ Open files in upload/ in binary mode
            and send them to dht
        """
        for i, file in enumerate(glob.glob("../upload/*")):
            with open(file, "rb") as f:
                data = f.read()
                key = Hashing(data, True)
                command = DHTCommand(DHTCommand.PUT, key, data.hex())
                response = self.dht.sendcommand(self.servers[i % len(self.ports)], command)
                print("SERVER:", response)


if __name__ == '__main__':
    unittest.main()
