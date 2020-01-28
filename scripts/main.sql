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
DROP TABLE IF EXISTS roles CASCADE;
DROP TABLE IF EXISTS token CASCADE;
DROP TABLE IF EXISTS roles_token CASCADE;
DROP TABLE IF EXISTS roles_useraccount CASCADE;
    
CREATE TABLE customer(
    customer_id SERIAL PRIMARY KEY NOT NULL,
    company_name VARCHAR(250) DEFAULT 'name not set' NOT NULL,
    tax_id VARCHAR(30) DEFAULT '' NOT NULL,
    domain_url VARCHAR(300) DEFAULT '' NOT NULL
);

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

CREATE TABLE token (
    tokenkey_id SERIAL PRIMARY KEY NOT NULL,
    tokenkey VARCHAR(50) NOT NULL,
    groupkey VARCHAR(50) NOT NULL,
    database_id int NOT NULL,
    active BOOLEAN DEFAULT '0' NOT NULL,
    FOREIGN KEY (database_id) REFERENCES database_list(database_id)
);

CREATE TABLE useraccount (
    useraccount_id SERIAL PRIMARY KEY NOT NULL,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(250) NOT NULL,
    database_id int NOT NULL,
    active BOOLEAN DEFAULT '0' NOT NULL,
    FOREIGN KEY (database_id) REFERENCES database_list(database_id)
);

CREATE TABLE authactivity(
    authactivity_id SERIAL PRIMARY KEY NOT NULL,
    username VARCHAR(50) NOT NULL,
    date timestamp NOT NULL
);

CREATE TABLE roles (
    role_id SERIAL PRIMARY KEY NOT NULL,
    name varchar(75) NOT NULL
);

CREATE TABLE roles_token(
    role_id int NOT NULL,
    tokenkey_id int NOT NULL,
    PRIMARY KEY (role_id, tokenkey_id),
    FOREIGN KEY (role_id) REFERENCES roles(role_id),
    FOREIGN KEY (tokenkey_id) REFERENCES token(tokenkey_id)
);

CREATE TABLE roles_useraccount(
    role_id int NOT NULL,
    useraccount_id int NOT NULL,
    PRIMARY KEY (role_id, useraccount_id),
    FOREIGN KEY (role_id) REFERENCES roles(role_id),
    FOREIGN KEY (useraccount_id) REFERENCES useraccount(useraccount_id)
);

GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO $username;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO $username;
