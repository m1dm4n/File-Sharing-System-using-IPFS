import hashlib
import os
import socket
import json
import threading
import ssl
from sqlalchemy import create_engine, Column, String, ForeignKey, Integer
from sqlalchemy.orm import sessionmaker, relationship, declarative_base
import base64
import signal

block_size = 4 * 1024 * 1024
server_address = ('0.0.0.0', 5000)
certificate_file = './enkai.id.vn/certificate.crt'
private_key_file = './enkai.id.vn/ec-private-key.pem'
db_connection_string = 'postgresql://postgres:hoang@localhost:5432/IPFS_manager'

terminate_server = False

Base = declarative_base()

class User(Base):
    __tablename__ = 'users'
    username = Column(String, primary_key=True)
    password_hash = Column(String)
    salt = Column(String)
    public_key = Column(String)
    encrypted_private_key = Column(String)
    files = relationship('File')

class File(Base):
    __tablename__ = 'files'
    file_name = Column(String, primary_key=True)
    file_hash = Column(String, unique=True)
    username = Column(String, ForeignKey('users.username'))
    blocks = relationship('Block')

class Block(Base):
    __tablename__ = 'blocks'
    block_hash = Column(String, primary_key=True)
    file_hash = Column(String, ForeignKey('files.file_hash'))
    block_index = Column(Integer)

def handle_register(data):
    username = data['username']
    password = data['password']
    public_key = data['public_key']
    encrypted_private_key = data['encrypted_private_key']

    # Check if the user already exists
    user = session.query(User).filter_by(username=username).first()
    if user:
        response = {'success': False, 'message': 'User already exists'}
    else:
        # Generate salt and calculate password hash
        salt = os.urandom(16).hex()
        password_hash = hashlib.sha256((password + salt).encode()).hexdigest()

        # Create a new user record
        new_user = User(
            username=username,
            password_hash=password_hash,
            salt=salt,
            public_key=public_key,
            encrypted_private_key=encrypted_private_key
        )
        session.add(new_user)
        session.commit()

        response = {'success': True, 'message': 'Registration successful'}

    print(response)
    return response

def handle_login(data):
    username = data['username']
    password = data['password']

    # Retrieve the user from the database
    user = session.query(User).filter_by(username=username).first()
    if user:
        # Calculate the password hash with the stored salt
        salt = user.salt
        password_hash = hashlib.sha256((password + salt).encode()).hexdigest()

        # Check if the password hash matches
        if password_hash == user.password_hash:
            response = {'success': True, 'message': 'Login successful'}
        else:
            response = {'success': False, 'message': 'Invalid password'}
    else:
        response = {'success': False, 'message': 'User not found'}

    print(response)
    return response

def handle_upload(data):
    username = data['username']
    file_name = data['file_name']
    file_hash = data['file_hash']
    file_content = base64.b64decode(data['file_content'])
    
    # Calculate the SHA256 hash of the file content
    file_content_hash = hashlib.sha256(file_content).hexdigest()
    
    if file_content_hash != file_hash:
        return {'success': False, 'message': "File's hash mismatch"}
    
    if not handle_login(data)['success']:
        return {'success': False, 'message': "You are not logged in"}

    # Check if the user exists
    user = session.query(User).filter_by(username=username).first()
    if user:
        # Check if the file already exists for the user
        existing_file = session.query(File).filter_by(file_name=file_name, username=username).first()
        if existing_file:
            response = {'success': False, 'message': 'File already exists'}
        else:
            # Create a new file record
            new_file = File(
                file_name=file_name,
                file_hash=file_hash,
                username=username
            )
            session.add(new_file)
            session.commit()
            
            # Split the file into 4MB blocks and save them to the Blocks table
            block_size = 4 * 1024 * 1024  # 4MB
            blocks = []
            for index, i in enumerate(range(0, len(file_content), block_size)):
                block_data = file_content[i:i+block_size]
                block_hash = hashlib.sha256(block_data).hexdigest()

                # Save the block content to a local file with the block hash as the filename
                block_path = './blocks/' + block_hash
                with open(block_path, 'wb') as block_file:
                    block_file.write(block_data)

                block = Block(block_hash=block_hash, file_hash=file_hash, block_index=index)
                session.add(block)
                blocks.append(block)

            session.commit()

            response = {'success': True, 'message': 'File uploaded successfully'}
    else:
        response = {'success': False, 'message': 'User not found'}

    print(response)
    return response

def handle_download_request(data):
    file_hash = data['file_hash']
    #file_name = data['file_name']
    
    if not handle_login(data)['success']:
        return {'success': False, 'message': "You are not logged in"}

    # Retrieve the file blocks from the Blocks table
    blocks = session.query(Block).filter_by(file_hash=file_hash).order_by(Block.block_index).all()
    if blocks:
        # Merge the blocks into the complete file content
        file_content = b''
        for block in blocks:
            block_path = './blocks/' + block.block_hash
            with open(block_path, 'rb') as block_file:
                file_content += block_file.read()

        # Calculate the hash of the downloaded file content
        downloaded_file_hash = hashlib.sha256(file_content).hexdigest()

        if downloaded_file_hash == file_hash:
            # Encode the file content as base64
            encoded_content = base64.b64encode(file_content).decode()

            # Prepare the response with the encoded file content
            response = {
                'success': True,
                'message': 'File downloaded successfully',
                'file_content': encoded_content,
                'file_hash': file_hash
            }
        else:
            response = {
                'success': False,
                'message': 'Downloaded file hash does not match the provided hash'
            }
    else:
        response = {
            'success': False,
            'message': 'File not found'
        }

    print(response)
    return response

def handle_download_request2(data):
    username = data['username']
    
    if not handle_login(data)['success']:
        return {'success': False, 'message': "You are not logged in"}


    # Retrieve all files for the given username
    files = session.query(File).filter_by(username=username).all()

    if not files:
        response = {
            'success': False,
            'message': 'No files found for the user'
        }
        return response

    # Create a dictionary to hold the file contents
    file_contents = {}

    # Iterate over each file and retrieve its content
    for file in files:
        file_hash = file.file_hash
        file_name = file.file_name

        # Retrieve the blocks for the file from the Blocks table
        blocks = session.query(Block).filter_by(file_hash=file_hash).order_by(Block.block_index).all()

        if blocks:
            # Merge the blocks into the complete file content
            file_content = b''
            for block in blocks:
                block_path = './blocks/' + block.block_hash
                with open(block_path, 'rb') as block_file:
                    file_content += block_file.read()

            # Calculate the hash of the downloaded file content
            downloaded_file_hash = hashlib.sha256(file_content).hexdigest()

            if downloaded_file_hash == file_hash:
                # Encode the file content as base64
                encoded_content = base64.b64encode(file_content).decode()

                # Store the file content in the dictionary
                file_contents[file_name] = encoded_content

    if file_contents:
        response = {
            'success': True,
            'message': 'Files downloaded successfully',
            'file_contents': file_contents
        }
    else:
        response = {
            'success': False,
            'message': 'No files could be downloaded'
        }

    return response

def handle_list_files(data):
    username = data['username']

    # Check if the user exists
    user = session.query(User).filter_by(username=username).first()
    if user:
        files = session.query(File).filter_by(username=username).all()
        file_list = [{'file_name': file.file_name, 'file_hash': file.file_hash} for file in files]
        response = {'success': True, 'files': file_list}
    else:
        response = {'success': False, 'message': 'User not found'}

    print(response)
    return response

def handle_request(client_socket):
    while True:
        try:
            # Receive and parse the request
            request_data = client_socket.recv(4096).decode()
            request = json.loads(request_data)

            action = request['action']
            data = request['data']
            print(data)
            
            # Perform action based on the request
            if action == 'register':
                response = handle_register(data)
            elif action == 'login':
                response = handle_login(data)
            elif action == 'upload':
                response = handle_upload(data)
            elif action == 'download':
                if 'file_hash' in data:
                    response = handle_download_request(data)
                else:
                    response = handle_download_request2(data)
            elif action == 'list_files':
                response = handle_list_files(data)
            else:
                response = {'success': False, 'message': 'Invalid action'}

            # Send the response back to the client
            response_data = json.dumps(response).encode()
            client_socket.sendall(response_data)
        except Exception as e:
            print(e)
            break

    # Close the client socket
    client_socket.close()
    print('Client socket closed')
    
def signal_handler(signum, frame):
    global terminate_server
    print('Terminating the server...')
    terminate_server = True

if __name__ == '__main__':
    # Set up signal handler for graceful termination
    signal.signal(signal.SIGINT, signal_handler)

    # Load SSL/TLS context
    context = ssl.create_default_context(ssl.Purpose.CLIENT_AUTH)
    context.load_cert_chain(certfile=certificate_file, keyfile=private_key_file)

    engine = create_engine(db_connection_string)
    Session = sessionmaker(bind=engine)
    session = Session()

    Base.metadata.create_all(engine)

    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind(server_address)
    server_socket.listen(5)

    print(f'TCP server listening on {server_address[0]}:{server_address[1]}')

    while not terminate_server:
        client_socket, client_address = server_socket.accept()
        print(f'New connection from {client_address[0]}:{client_address[1]}')

        # Wrap client socket with SSL/TLS
        ssl_client_socket = context.wrap_socket(client_socket, server_side=True)

        thread = threading.Thread(target=handle_request, args=(ssl_client_socket,))
        thread.start()
        thread_id = thread.ident
        print(f'Thread {thread_id} started')

    # Close the server socket
    server_socket.close()

    print('Server terminated.')
    