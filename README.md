# File-Sharing-System-using-IPFS

## System structure
.
├── src/
│   ├── Blockchain
│   ├── PeerNode
│   ├── RPC
│   └── GUI
├── .config
└── setup.py

### Blockchain
 
The place where the hash of the file is stored and ensures the security features of the system.

### PeerNode

It plays the role of both a client and a server. The client sends files for storage, while the server receives requests to download files from other nodes.

### RPC

The intermediary communication between the blockchain and nodes, as well as between nodes themselves.

### GUI

The main user interface that provides a sign-up mechanism that automatically generates a private key using a pre-installed algorithm. It also provides a login mechanism that allows users to load their private key from a file.

### Config and setup

The location where system parameters are stored and automatically configured.

## Communication

All connections are encrypted using AES, and the key exchange mechanism used is CRYTAL-KYBER.
