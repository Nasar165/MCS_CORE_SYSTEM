CREATE DATABASE $databasename;

CREATE USER $username WITH
    LOGIN
    NOSUPERUSER
    NOCREATEDB
    NOCREATEROLE
    INHERIT
    NOREPLICATION
    CONNECTION LIMIT -1
    PASSWORD '$password';
          
DROP TABLE IF EXISTS customer CASCADE;
DROP TABLE IF EXISTS contact CASCADE;
DROP TABLE IF EXISTS useraccount CASCADE;
DROP TABLE IF EXISTS database_list CASCADE;
DROP TABLE IF EXISTS tokenkey CASCADE;
DROP TABLE IF EXISTS authactivity CASCADE;
    
CREATE TABLE customer(
    customer_id SERIAL PRIMARY KEY NOT NULL,
    company_name VARCHAR(250) DEFAULT 'name not set' NOT NULL,
    tax_id VARCHAR(30) DEFAULT '' NOT NULL,
    dbd_url VARCHAR(300) DEFAULT '' NOT NULL,
    domain_url VARCHAR(300) DEFAULT '' NOT NULL
);

CREATE TABLE contact(
    contact_id SERIAL PRIMARY KEY NOT NULL,
    name VARCHAR(100) DEFAULT '' NOT NULL,
    telephone VARCHAR(50) DEFAULT '' NOT NULL,
    email VARCHAR(100) DEFAULT '' NOT NULL,
    position VARCHAR(100) DEFAULT '' NOT NULL,
    address VARCHAR(100) DEFAULT '' NOT NULL,
    customer_id INT NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES customer(customer_id)
);

CREATE TABLE database_list(
    database_id SERIAL PRIMARY KEY NOT NULL,
    database_name VARCHAR(250) NOT NULL,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    dbm VARCHAR(100) DEFAULT 'NpgSQL' NOT NULL
);

insert into database_list (database_name,username,password,dbm) values('testdb','mcsuser','Nasar165','NpgSql');

CREATE TABLE token (
    tokenkey_id SERIAL PRIMARY KEY NOT NULL,
    tokenkey VARCHAR(50) NOT NULL,
    groupkey VARCHAR(50) NOT NULL,
    database_id int NOT NULL,
    active BOOLEAN DEFAULT '0' NOT NULL,
    FOREIGN KEY (database_id) REFERENCES database_list(database_id)
);

insert into token (tokenkey,groupkey,database_id,active) values('#we321$$awe',12,1,'1');

CREATE TABLE useraccount (
    useraccount_id SERIAL PRIMARY KEY NOT NULL,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    database_id int NOT NULL,
    active BOOLEAN DEFAULT '0' NOT NULL,
    FOREIGN KEY (database_id) REFERENCES database_list(database_id)
);

insert into useraccount (username,password,database_id,active) values('nasar','0zrYwyRKtp35kFlnsQPfgw==',1,'1');

CREATE TABLE authactivity(
    authactivity_id SERIAL PRIMARY KEY NOT NULL,
    username VARCHAR(50) NOT NULL,
    date timestamp NOT NULL
);

GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO $username;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO $username;
