# A Secure File Sharing System Based on IPFS and Blockchain

## System structure

```
.
├── src/
│   ├── Blockchain/
│   ├── IPFSNode/
│   ├── RPC/
│   └── GUI/
|   |__ DHT/
├── .config
└── setup.py
```

### Blockchain
 
The place where the hash of the file is stored and ensures the security features of the system.

### IPFSNode

It plays the role of both a client and a server. The client sends files for storage, while the server receives requests to download files from other nodes.

### RPC

The intermediary communication between the blockchain and nodes, as well as between nodes themselves.

### GUI

The main user interface that provides a sign-up mechanism that automatically generates a private key using a pre-installed algorithm. It also provides a login mechanism that allows users to load their private key from a file.

### Config and setup

The location where system parameters are stored and automatically configured.

## Communication

All connections are encrypted using AES, and the key exchange mechanism used is ECC, also for signature.

## In details.

### A network layer for IPFS protocol

Illustrating the interaction between a client, RPC (Remote Procedure Call), and other components in the IPFS network:

```
+--------------+          +--------------+          +------------------+
|    Client    |          |     RPC      |          |   IPFS Network   |
+--------------+          +--------------+          +------------------+
      |                          |                          |
      |       Step 1: Publish File / Content              |
      |------------------------------------------------->|
      |     - Connect to RPC endpoint                      |
      |     - Send request to publish file/content          |
      |     - Include file/content data in the request      |
      |     - RPC processes the request                     |
      |     - Perform necessary validations                |
      |     - Store the file/content in IPFS network        |
      |     - Generate unique content ID (CID)              |
      |     - Return the CID as the response                |
      |                                                    |
      |       Step 2: Retrieve File / Content               |
      |------------------------------------------------->|
      |     - Connect to RPC endpoint                      |
      |     - Send request to retrieve file/content         |
      |     - Include the content ID (CID) in the request   |
      |     - RPC processes the request                     |
      |     - Retrieve the requested file/content from IPFS |
      |     - Pack the file/content into the response       |
      |     - Return the response to the client             |
      |                                                    |
      |       Step 3: Search for File / Content             |
      |------------------------------------------------->|
      |     - Connect to RPC endpoint                      |
      |     - Send search request for file/content          |
      |     - Include search parameters in the request      |
      |     - RPC processes the request                     |
      |     - Search the IPFS network for matching content  |
      |     - Retrieve a list of matching files/content     |
      |     - Pack the search results into the response     |
      |     - Return the response to the client             |
      |                                                    |
      |       Step 4: Network Maintenance and Management    |
      |                                                    |
      |     - RPC handles network management tasks          |
      |     - Peer discovery and connection management      |
      |     - Consensus protocol handling                   |
      |     - Network health monitoring and maintenance     |
      |                                                    |
+--------------+          +--------------+          +------------------+
|    Client    |          |     RPC      |          |   IPFS Network   |
+--------------+          +--------------+          +------------------+
```

### Publish File / Content:
   - The client connects to the IPFS network through an IPFS node or gateway.
   - The client adds the file or content to IPFS using the "ipfs add" command, which creates a new Merkle DAG (Directed Acyclic Graph) node that represents the file or content and stores it on the IPFS network.
   - The IPFS network replicates the data across multiple nodes in a decentralized manner to ensure redundancy and availability.
   - The IPFS network generates a unique content ID (CID) for the published file/content and returns it as the response to the client.

### Retrieve File / Content:
   - The client connects to the IPFS network through an IPFS node or gateway.
   - The client retrieves a file or content from the IPFS network using the "ipfs get" command, specifying the content ID (CID) of the file/content.
   - The IPFS network looks up the content ID (CID) and returns the corresponding data to the client.
   - The client receives the requested file/content from the IPFS network.

### Search for File / Content:
   - The client connects to the IPFS network through an IPFS node or gateway.
   - The client searches for a file or content using the IPFS Distributed Hash Table (DHT) or the IPFS Search API.
   - If using the DHT, the IPFS network looks up the content ID (CID) or keyword in the DHT and returns the corresponding data to the client.
   - If using the Search API, the IPFS network searches the content index for the keyword and returns the corresponding data to the client.
   - The client receives the search results from the IPFS network.

### Incentive:
#### 1. Storage Incentives:
   - IPFS incentivizes storage providers to store and replicate content by allowing them to earn tokens for providing storage space to the network.
   - Users can pay storage providers in IPFS tokens to store their data on the IPFS network, and storage providers can earn tokens for storing and replicating the data.

#### 2. Content Discovery Incentives:
   - IPFS incentivizes content creators to publish their content on the IPFS network by making it easy for users to discover and access their content.
   - Content creators can earn tokens for publishing high-quality and popular content that is widely accessed and shared on the IPFS network.

#### 3. Network Performance Incentives:
   - IPFS incentivizes network participants to help improve the performance and reliability of the network.
   - Nodes that provide reliable and fast access to content can earn tokens for their contributions to the network.
   - Nodes that provide valuable services such as content caching or content discovery can also earn tokens for their contributions.

#### 4. Governance Incentives:
   - IPFS incentivizes network participants to participate in the governance of the network by allowing them to earn tokens for voting on important network decisions.
   - Participants can earn tokens for participating in discussions and voting on proposals that help shape the future of the IPFS network.
