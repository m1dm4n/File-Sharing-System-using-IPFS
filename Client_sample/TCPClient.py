import socket
import ssl
import json
import hashlib
import base64

server_address = ('enkai.id.vn', 5000)

def send_request(action, data):
    request = {'action': action, 'data': data}
    request_data = json.dumps(request).encode()

    # Create a TCP socket and wrap it with SSL/TLS
    client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    ssl_context = ssl.create_default_context(ssl.Purpose.SERVER_AUTH)
    ssl_client_socket = ssl_context.wrap_socket(client_socket, server_hostname=server_address[0])

    # Connect to the server
    ssl_client_socket.connect(server_address)

    # Send the request to the server
    ssl_client_socket.sendall(request_data)

    # Receive and decode the response
    response_data = ssl_client_socket.recv(4096)
    response = json.loads(response_data.decode())

    # Close the socket
    ssl_client_socket.close()

    return response

def register(username, password, public_key, encrypted_private_key):
    data = {
        'username': username,
        'password': password,
        'public_key': public_key,
        'encrypted_private_key': encrypted_private_key
    }

    response = send_request('register', data)
    return response

def login(username, password):
    data = {'username': username, 'password': password}

    response = send_request('login', data)
    return response

def upload(username, password, file_name, file_content):
    data = {'username': username, 'password': password, 'file_name': file_name,
            'file_content': base64.b64encode(file_content.encode()).decode()}
    
    file_hash = hashlib.sha256(file_content.encode()).hexdigest()
    
    data['file_hash'] = file_hash

    response = send_request('upload', data)
    return response

def download_user_files(username, password):
    # Prepare the request data
    data = {
        'username': username,
        'password': password
    }

    # Send the download user files request
    response = send_request('download', data)
    return response

def list_user_files(username, password):
    data = {
        'username': username,
        'password': password
    }
    response = send_request('list_files', data)
    return response

if __name__ == '__main__':
    # Example usage:
    register_response = register('john123', 'password123', 'public_key', 'encrypted_private_key')
    print(register_response)

    login_response = login('john123', 'password123')
    print(login_response)

    upload_response = upload('john123', 'password123', 'test4.txt', 'aaaaaaaaa')
    print(upload_response)
    
    download_response = download_user_files('john123', 'password123')
    print(download_response)
    
    list_file_response = list_user_files('john123', 'password123')
    print(list_file_response)
