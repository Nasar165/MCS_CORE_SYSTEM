![Xeroxcore logo](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/images/Xeroxcore_Logo.png)
<br/><br/>
[![CodeFactor](https://www.codefactor.io/repository/github/xeroxcore/xeroxcore/badge)](https://www.codefactor.io/repository/github/xeroxcore/xeroxcore)
[![Xeroxcore logo](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/images/docker-supported.png)](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/deployment/Docker.md)

###### Docker Deployemnts
[![Azure](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/images/Azure.png)](https://azure.microsoft.com/en-us/)
[![AWS](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/images/aws.png)](https://azure.microsoft.com/en-us/)
[![Digitalocaen](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/images/digitalocean.png)](https://azure.microsoft.com/en-us/) <br/>
<sup>* P = Pass * PE = Pending * F = failed</sup>

## Introduction

Xeroxcore system intends to resolve a business requirement concerning a framework fitted to handling
multiple databases. The core system comes with pre-built-in tools & customer management to speed up
your development. The system is well suited for an organisation that needs a framework suited to
handling multiple databases and more than one client.

<!--ts-->

## Table of Contents

- [Getting started](#getting-started)
- [Prerequisites](#prerequisites)
- [Appsettings configuration](#appsettings-configuration)
- [Installing](#installing)
  - [PostgreSQL](#postgresql)
    - [Linux-CentOS](#linux-centos)
    - [Windows 10](#windows-10)
  - [SQL Script](#sql-script)
  - [Dotnet core 3](#dotnet-core-3)
    - [Linux-CentOS](#linux-centos-1)
    - [Windows 10](#windows-10-1)
- [Deployment](#deployment)
- [Built With](#built-with)
- [Contributing](#contributing)
- [Versioning](#versioning)
- [Authors](#authors)
- [License](#license)
- [Acknowledgments](#acknowledgments)

<!--te-->

### Getting started

These instructions are aimed at helping you set up the project for development or testing purposes.
If you wish to put the project in production, please check our [Deployment section](#deployment).

### Prerequisites

The following tools are required for the API to function please make sure that necessary tools
are installed and if not, install them utilizing the provider's main pages.

- [.Net Core](https://dotnet.microsoft.com/download/dotnet-core/3.0) - dotnet core 3 and greater
- [PostgreSQL](https://www.postgresql.org/) - PostgreSQL (Required by default)

### Appsettings Configuration

Configuring your application before deployment is crucial for smooth operation. This section will
help you understand how altering values in the settings file impacts the behaviour of your application.
keep in mind that altering certain settings during production might have unforeseen effects so
be careful and test the API before deploying it to production.

```
Filepath src/api/appsettings.json
```

```
{
  "AppSettings": {
    "JWTKey": "MyNewSuperMegeKeyForJwt11",
    "AESKey": "b14ca5898a4e4133bbce2ea2315a1916",
    "Domain": "yourdomain.com",
    "ExportLogHttp": true,
    "LogAsJson": true,
    "Docker": true
  },
  "ConnectionStrings": {
    "default": "Server=127.0.0.1;port=5432;Database=defaultdatabase;Uid=defaultuser;Pwd=SG,npuLc2?;",
    "docker": "Server=database;port=5432;Database=defaultdatabase;Uid=defaultuser;Pwd=SG,npuLc2?;"
  },
  "Smtp": {
    "server": "url",
    "username": "mail",
    "password": "pw",
    "Port": 465,
    "SSl": true
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

#### JWTKey

The symmetric key is linked to JWT token authentication, you must set a secure key and keep it safe. Altering this key-value while users are authenticated will
cause their keys to becoming invalid.

#### AesKey

AesKey is used in AES encryption that is used to encrypt data before being stored in the database.
the key is also used to decrypt data. Changing this key will cause all data stored to
remain in a permanent encrypted state. Trying to decrypt the data will fail.

#### ExportLogHttp

ExportLogHttp allows you the toggle between true and false setting it to true allows the application
to export your log files through HTTP protocol. The default value is set to false.

##### LogAsJson

LogAsJson tells the EventLogger class to log all events as a JSON string allowing you the user
to get the result as a JSON string when using ExportLogHttp. The default value is set to true.

#### Docker

Docker informs the API if it is being run in a container that has been deployed using
docker-compose this is important to make sure that the API connects to PostgreSQL container.
set to true if using docker-compose false of running with kestrel or IIS. The default value
is set to false.

#### ConnectionStrings

SQL connections strings are stored in this section check scenarios bellow and adjust your
connection string according to your specifications.

```
 "ConnectionStrings": {
    "default": "Server=127.0.0.1;port=5432;Database=defaultdatabase;Uid=defaultuser;Pwd=SG,npuLc2?;",
    "docker": "Server=database;port=5432;Database=defaultdatabase;Uid=defaultuser;Pwd=SG,npuLc2?;"
  },
```

##### Default

Alter this connection string if you are planning to run your SQL server or if the connection
string requires additional parameters. PS make sure that Docker is set to false.

##### Docker

Alter this connection string according to your specifications if you have multiple containers
connected. PS make sure that Docker is set to true.

#### Smtp credentials

Here you will configure the mail server that will be used as the primary mail for sending
errors informing you if something has gone wrong or if a specific event has occurred
that requires your attention.

```
"Smtp": {
    "server": "url",
    "username": "mail",
    "password": "pw",
    "Port": 465,
    "SSl": true
  },
```

### Installing

This section will help you to set up your development environment. For production deployment please
check the [Deployment section](#deployment). Since the app was developed in CentOS 7, most of the
commands might be centos related.

#### PostgreSQL

The following installation is made for Linux (centos 7) & Windows10. If you are using a different distribution or OS then
please check PostgreSQL website or distribution website for more information

##### Linux-CentOS

```
1. sudo yum install -y postgresql-server.x86_64

2. Initialize database
  1. sudo postgresql-setup initdb
  2. sudo systemctl start postgresql # to start PostgreSQL.
  3. sudo systemctl enable postgresql # Tell centos to start PostgreSQL on startup.

3. Alter user password for posgres account
  1. sudo passwd posgres
  2. Enter a password and confirm.

4. Change Account to postgres
  1. su - postgres. (login into the shell as postgres user)
  2. enter your password.
  3. enter psql to enter PostgreSQL command-line tool.

5. Connect to psql
  1. sudo su <username> (You will be prompted to insert password)
  2. enter psql and enter (You are now in PostgreSQL shell);

6. Connect to SQL using an external app
  1. login to psql then enter show hba_file ;
  2. edit the pg_hba.conf file.
  3. change ipv4 connection from ident to trust this will
  allow other forms of authentication like username and password;

```

#### Windows 10

```
1. Go to Enterprise DB Website and download the latest Postgres Version
[Enterprise DB](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)
2. Execute the downloaded file postgresql-x.x-x-windows-x64.exe and follow the
installation steps.
2.1 Make sure to remember the password as you will need it later to sign in to PgAdmin.
3. Go to Start and search for PgAdmin and run it.
4. Once the browser opens the PgAdmin page enter your password then select the database
and enter the password again if prompted to.
5. You can now use PostgreSql.
```

##### SQL Script

This Project requires two SQL databases to function properly. The main Database purpose is to
act as an in-house database storing clients information such as username, password token keys
and much more. While the client DB contains all the client’s private information.

###### Running scripts

- [MainDB](https://github.com/Xeroxcore/Xeroxcore/blob/master/scripts/main.sql) - Main Script

#### Dotnet core 3

This installation process was made for centos 7 & Windows 10 and might not work on your system. if you are
utilizing an alternative OS or distribution then please check the [.Net Core](https://dotnet.microsoft.com/download/linux-package-manager/rhel/sdk-current) website for more info. Ps, you might need to enable
third-party repositories.

##### Linux-CentOS

```
begin by opening a new shell window and follow the steps below:
1. sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
2. sudo yum install dotnet-sdk-3.0.x86_64 -y
3. enter the following command after the installation is complete to verify the installation
  dotnet --version
```

###### Windows 10

```
1. Go to the following URL and download the [.Net Core SDK](https://dotnet.microsoft.com/download).
2. Open the install dotnet-sdk-3.x.x-win-x64.exe.
3. follow the installation instructions.
4. Open CMD(Command prompt) and paste dotnet --Version to verify the installation.
```

##### Test application

This section describes how to test the web API and make sure that it is working correctly.
this is a simple test and will only verify that the installation was successful.

```
1. Alter application.json.sample file name to application.json
  1. Alter JWTKey in Appsettings min length is 8chars

  2. Alter default to your SQL database changing the database, username and password.
  NOTICE! The server is set to localhost (127.0.0.1)

  3. Alter Smtp to your email adding the server, username, password if you are using
  another port than 465 change port and make sure that SSL is set to false if
  there’s not an SSL certificate.

2. Run WebAPI
  *. If in Visual studio code press F5 for debugging
  *. If command line, enter dotnet watch run within mcs_api/mcs.api folder

3. Test API in your browser or postman with the following
   url: https://localhost:5001/version. Expected result is version: x.x.x.x
```

### Deployment

Deploying the API to a real server requires minimal configuration depending
on your server environment we currently support IIS and Docker. We have listed
our supported deployment options down below for you to read. Please note
That under no circumstances is deployment related to server configurations.

- [Docker](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/deployment/Docker.md) - Partialy Supported
- [IIS](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/deployment/IIS.md) - Full support

### Built With

API was built with the following tools in a Linux Environment (CentOS 7).

- [Visual Studio Code](https://code.visualstudio.com/) - Code Editor
- [PostgreSQL](https://www.postgresql.org/) - SQL Database
- [.Net Core](https://dotnet.microsoft.com/) - Dotnet Core Runtime
- [Postmam](https://getpostman.com/) - API Development Tool (Testing API)

### Contributing

### Versioning

We use [SemVer](http://semver.org/) for versioning. Please visit their website for more
information to understand how different versions might affect you and your project.

### Authors

- **Nasar Eddaoui** - _Initial work_ - [Nasar Eddaoui](https://github.com/Nasar165)

See also the list of [contributors](https://github.com/Xeroxcore/Xeroxcore//graphs/contributors) who participated in this project.

### License

This project is licensed under the GNU General Public License v3.0 - see the [LICENSE](LICENSE) file for details

### Acknowledgments

- [Hubspot](https://www.hubspot.com) - Inspiration source
- [Chayapat Chuen A Rom ](https://github.com/freedombs) - Angular Developer helping us test the web api
