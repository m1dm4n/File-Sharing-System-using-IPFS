from sqlalchemy import create_engine, Column, Integer, String
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import scoped_session, sessionmaker

engine = create_engine('postgresql://zucjhjco:U-DdtUEf9f11U4oi6Ubc8obLELwXMXXj@rajje.db.elephantsql.com/zucjhjco')
Session = scoped_session(sessionmaker(bind=engine))
Base = declarative_base()
Base.query = Session.query_property()
class User(Base):
    __tablename__ = 'users'
    username = Column(String(100), primary_key=True)
    password_hash = Column(String(100), nullable=False)
    public_key = Column(String(300), nullable=False)
    enc_private_key = Column(String(300), nullable=False)
    enc_filelist = Column(String(300), nullable=False)
    def __init__(self, username,password_hash, public_key, enc_private_key, enc_filelist):
        self.username = username
        self.password_hash = password_hash
        self.public_key = public_key
        self.enc_private_key= enc_private_key
        self.enc_filelist = enc_filelist
    def __str__(self):
        return f"""Username: {self.username}, Public key: {self.public_key}"""
Session.remove()
        