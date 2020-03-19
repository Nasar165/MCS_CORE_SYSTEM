/* Create Database */
CREATE DATABASE defaultdatabase;

/* Create Useraccount */
CREATE USER defaultuser WITH
    LOGIN
    NOSUPERUSER
    NOCREATEDB
    NOCREATEROLE
    INHERIT
    NOREPLICATION
    CONNECTION LIMIT -1
    PASSWORD 'SG,npuLc2?';

\c defaultdatabase

/* Create Tables */
CREATE TABLE customer(
    customer_id SERIAL PRIMARY KEY NOT NULL,
    company_name VARCHAR(250) DEFAULT 'name not set' NOT NULL,
    tax_id VARCHAR(30) DEFAULT '' NOT NULL,
    domain_url VARCHAR(300) DEFAULT '' NOT NULL
);

INSERT INTO customer (company_name,tax_id, domain_url) VALUES('Xeroxcore','0955-331-122','https://xeroxcore.org');

CREATE TABLE contact(
    contact_id SERIAL PRIMARY KEY NOT NULL,
    name VARCHAR(100) DEFAULT '' NOT NULL,
    telephone text DEFAULT '' NOT NULL,
    email text DEFAULT '' NOT NULL,
    position VARCHAR(100) DEFAULT '' NOT NULL,
    address text DEFAULT '' NOT NULL,
    customer_id INT NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES customer(customer_id)
);

INSERT INTO contact (name,telephone,email,position,address,customer_id) 
VALUES('Nasar', '{"telephone":"+660-955-886-699"}', 'test@xeroxcore.org', 'CEO', 'GG raod 85/55 Muang Phuket, Thailand 83000',1);

CREATE TABLE database_list(
    database_id SERIAL PRIMARY KEY NOT NULL,
    database_name VARCHAR(250) NOT NULL,
    ip VARCHAR(20) NOT NULL DEFAULT '127.0.0.1', 
    port INT NOT NULL DEFAULT 5432,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    dbm VARCHAR(100) DEFAULT 'NpgSQL' NOT NULL,
    customer_id INT NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES customer(customer_id)
);

INSERT INTO database_list (database_name,username,password,dbm, customer_id) VALUES('testdb','mcsuser','Nasar165','NpgSql', 1);

CREATE TABLE token (
    token_id SERIAL PRIMARY KEY NOT NULL,
    tokenkey VARCHAR(50) NOT NULL,
    groupkey VARCHAR(50) NOT NULL,
    database_id INT NOT NULL,
    active BOOLEAN DEFAULT '0' NOT NULL,
    FOREIGN KEY (database_id) REFERENCES database_list(database_id)
);

INSERT INTO token (tokenkey,groupkey,database_id,active) VALUES('#we321$$awe',12,1,'1');

CREATE TABLE useraccount (
    useraccount_id SERIAL PRIMARY KEY NOT NULL,
    username VARCHAR(250) NOT NULL,
    password VARCHAR(250) NOT NULL,
    database_id INT NOT NULL,
    active BOOLEAN DEFAULT '0' NOT NULL,
    athempts INT NOT NULL DEFAULT 0,
    lockout TIMESTAMP,
    FOREIGN KEY (database_id) REFERENCES database_list(database_id)
);

INSERT INTO useraccount (username,password,database_id,active) VALUES('0mFrB1cJ995oZj1wb1N6YA==','0zrYwyRKtp35kFlnsQPfgw==',1,'1');

CREATE TABLE authactivity(
    authactivity_id SERIAL PRIMARY KEY NOT NULL,
    username VARCHAR(50) NOT NULL,
    date timestamp NOT NULL
);

CREATE TABLE roles (
    role_id SERIAL PRIMARY KEY NOT NULL,
    name varchar(75) NOT NULL
);

INSERT INTO roles (name) VALUES('Root');
INSERT INTO roles (name) VALUES('Admin');
INSERT INTO roles (name) VALUES('Support');
INSERT INTO roles (name) VALUES('TokenAuth');

CREATE TABLE roles_token(
    role_id INT NOT NULL,
    token_id INT NOT NULL,
    PRIMARY KEY (role_id, token_id),
    FOREIGN KEY (role_id) REFERENCES roles(role_id),
    FOREIGN KEY (token_id) REFERENCES token(token_id)
);
INSERT INTO roles_token (role_id, token_id) VALUES(4,1);

CREATE TABLE roles_useraccount(
    role_id INT NOT NULL,
    useraccount_id int NOT NULL,
    PRIMARY KEY (role_id, useraccount_id),
    FOREIGN KEY (role_id) REFERENCES roles(role_id),
    FOREIGN KEY (useraccount_id) REFERENCES useraccount(useraccount_id)
);

INSERT INTO roles_useraccount (role_id, useraccount_id) VALUES(1,1);
INSERT INTO roles_useraccount (role_id, useraccount_id) VALUES(2,1);

/* Grant Privilages to tables */
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO defaultuser;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO defaultuser;

/* Start functions*/
CREATE OR REPLACE FUNCTION selectuser(userid INT)
  RETURNS TABLE (username TEXT, password TEXT, database_id INT, active BOOLEAN)
AS $$
SELECT username, password,database_id,active 
FROM useraccount 
WHERE useraccount_id = userid;
$$ LANGUAGE SQL;

CREATE OR REPLACE FUNCTION selectuser(username TEXT)
  RETURNS TABLE (username TEXT, password TEXT, database_id INT, active BOOLEAN)
AS $$
SELECT username, password,database_id,active 
FROM useraccount 
WHERE username = username;
$$ LANGUAGE SQL;

CREATE OR REPLACE FUNCTION selecttoken(tokenk VARCHAR(50))
  RETURNS TABLE (tokenkey VARCHAR(50), groupkey VARCHAR(50), database_id INT, active BOOLEAN)
AS $$
SELECT tokenkey, groupkey, database_id, active 
FROM token 
WHERE tokenkey = tokenk;
$$ LANGUAGE SQL;

CREATE OR REPLACE FUNCTION selecttoken(tokenid INT)
  RETURNS TABLE (tokenkey VARCHAR(50), groupkey VARCHAR(50), database_id INT, active BOOLEAN)
AS $$
SELECT tokenkey, groupkey, database_id, active 
FROM token 
WHERE token_id = tokenid;
$$ LANGUAGE SQL;

CREATE OR REPLACE FUNCTION selectdatabase(databaseid INT)
  RETURNS TABLE (database_name VARCHAR(250), ip VARCHAR(20), port INT, username VARCHAR(50), password VARCHAR(50), dbm VARCHAR(100))
AS $$
SELECT database_name, ip, port, username, password, dbm 
FROM database_list 
WHERE database_id = databaseid;
$$ LANGUAGE SQL;

CREATE OR REPLACE FUNCTION getuserroles(userid INT)
  RETURNS TABLE (name varchar(75))
AS $$
SELECT name FROM roles 
INNER JOIN roles_useraccount ON roles_useraccount.role_id = roles.role_id
WHERE roles_useraccount.useraccount_id = userid;
$$ LANGUAGE SQL;

CREATE OR REPLACE FUNCTION gettokenroles(tokenid INT)
  RETURNS TABLE (name varchar(75))
AS $$
SELECT name FROM roles 
INNER JOIN roles_token ON roles_token.role_id = roles.role_id
WHERE roles_token.token_id = tokenid
$$ LANGUAGE SQL;

/* Start procedures */