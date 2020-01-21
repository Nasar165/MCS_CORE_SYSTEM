![Xeroxcore logo](https://github.com/Xeroxcore/Xeroxcore/blob/master/Xeroxcore_Logo.png)
<br/><br/>
## Introduction
Xeroxcore system intends to resolve a business requirement concerning a framework fitted to handling
multiple databases. The core system comes with pre-built-in tools & customer managment to speed up
your development. The system is well suited for an organisation that needs a framework suited to
handling multiple databases and more than one client.

<!--ts-->
## Table of Contents
* [Getting started](#getting-started)
* [Prerequisites](#prerequisites)
* [Installing](#installing)
  * [PostgreSQL](#postgresql)
    * [Linux-CentOS](#linux-centos)
    * [Windows 10](#windows-10)
  * [SQL Script](#sql-script)
  * [Dotnet core 3](#dotnet-core-3)
    * [Linux-CentOS](#linux-centos-1)
    * [Windows 10](#windows-10-1)
* [Deployment](#deployment)
* [Built With](#built-with)
* [Contributing](#contributing)
* [Versioning](#versioning)
* [Authors](#authors)
* [License](#license)
* [Acknowledgments](#acknowledgments)

  
<!--te-->

### Getting started

These instructions are aimed at helping you setting up the project for development or testing purposes.
If you wish to put the project in production, please check our [Deployment section](#deployment).

### Prerequisites

The following tools are required for the API to function please make sure that necessary tools
are installed and if not, install them utilizing the providers main pages.

- [.Net Core](https://dotnet.microsoft.com/download/dotnet-core/3.0) - dotnet core 3 and greater
- [PostgreSQL](https://www.postgresql.org/) - PostgreSQL (Required by default)

### Installing

This section will help you to setup your development environment. For production deployment please
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
  2. sudo systemctl start postgresql # to start postgresql.
  3. sudo systemctl enable postgresql # Tell centos to start postgresql on startup.

3. Alter user password for posgres account
  1.sudo passwd posgres
  2.Enter password and confirm.

4. Change Account to postgres
  1. su - postgres. (login in shell as postgres)
  2. enter your password.
  3. enter psql to enter postgresql command line tool.

5. Connect to psql
  1. sudo su <username> (You will be prompted to insert password)
  2. enter psql and enter (You are now in postgresql shell);

6. Connect to SQL using external app
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
3. Go to start and search for PgAdmin and run it.
4. Once the browser opens the PgAdmin page enter your password then select the database
and enter the password again if promted to.
5. You can now use PostgreSql.
```

##### SQL Script

This Project requires two SQL databases to function property. The main Database purpose is to
act as a inhouse database storing clients information such as username, password tokenkeys
and much more. While the client DB contains all the client’s private information.

###### Running scripts

- [MainDB](https://github.com/Xeroxcore/Xeroxcore/blob/master/scripts/main.sql) - Main Script

#### Dotnet core 3

This installation process was made for centos 7 & Windows 10 and might not work on your system. if you are
utilizing an alternative OS or distribution then please check the [.Net Core](https://dotnet.microsoft.com/download/linux-package-manager/rhel/sdk-current) website for more info. Ps you might need to enable
third party repositories.

##### Linux-CentOS
```
begin by opening a new shell window and follow the steps bellow:
1. sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
2. sudo yum install dotnet-runtime-3.0.x86_64 -y
3. enter the following command after the installation is complete to verify the installation
  dotnet --version
```
###### Windows 10 
```
1. Go tofollowing url and download the [.Net Core SDK](https://dotnet.microsoft.com/download).
2. Open the install dotnet-sdk-3.x.x-win-x64.exe.
3. follow the installation instructions.
4. Open CMD(Command prompt) and paste dotnet --Version to verify installation.
```

##### Test application

This section describes how to test the webapi and make sure that it is working correctly.
this is a simple test and will only verify that the installation was successful.

```
1. Alter application.json.sample file name to application.json
  1. Alter secreatkey in Appsettings minlenght is 8chars

  2. Alter mcscon to your SQL database changing database, username and password.
  NOTICE! Server is set to localhost (127.0.0.1)

  3. Alter Smtp to your email adding server, username, password if you are using
  another port than 465 change port and make sure that SSL is set to false if
  there’s not SSL certificate.

2. Run WebAPI
  *. If in Visual studio code press F5 for debug
  *. If command line, enter dotnet watch run within mcs_api/mcs.api folder

3. Test API in your browser or postman with the following
   url: https://localhost:5001/version. Expected result is version: x.x.x.x
```

### Deployment

Deployment section is comming soon

### Built With

This web API was built with the following tools in a Linux Environment (CentOS 7).

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
