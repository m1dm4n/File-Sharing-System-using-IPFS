from Models import *
from werkzeug.security import generate_password_hash,check_password_hash

### This is test function, make sure that database on instance working correctly
def query_all_users()->list:
    users = User.query.all()
    return users

## Function that insert new user to database "users"
def insert_new_user(username,password_hash,public_key,enc_private_key,enc_filelist = "") ->User: ##pls modify enc_filelist
    new_user = User(
        username=username,
        password_hash=password_hash,
        public_key=public_key,
        enc_private_key=enc_private_key,
        enc_filelist=enc_filelist
        )
    Session.add(new_user)
    Session.commit()
    return new_user

### check if username already exists or not?
def is_username_exists(username:str)->bool:
    user = User.query.filter_by(username=username).first()
    return user is not None

### Funtion that manage when new user register need to query 
def register(username:str, password:str, user_pubkey:str, user_enc_privkey:str) ->None: 
    ## Check if username already exists before submit user on database
    if is_username_exists(username=username):
        return "Username already exists!"
    password_hash = generate_password_hash(password = password,method='pbkdf2',salt_length=16)

    ## coding process for calculate public key & resolve encrypted private key


    ##temp
    insert_new_user(username,password_hash,"tempPub","tempPri")

register('duc','123')
users = query_all_users()
for u in users:
    print(u)
    