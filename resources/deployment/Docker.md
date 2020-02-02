# Docker Deployment

![Xeroxcore logo](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/images/Moby-logo.png)

## Introduction

Xeroxcore works great with docker simplifying deployment to AWS, Azure or any hosting provider that
provides docker deployment. This document will help you to create a docker image and then test it
locally before deployment to your cloud or hosting provider.

## Prerequisites

The following tools are required for the API to function properly please make sure that necessary tools
are installed and if not, install them utilizing the provider's main pages.

- [Docker](https://www.docker.com/) - Docker Container
- [PostgreSQL](https://www.postgresql.org/) - PostgreSQL (Required by default)
- [.Net Core](https://dotnet.microsoft.com/download/dotnet-core/3.0) - dotnet core 3 and greater

## Deploying Docker with docker-compose
## Running Docker compose
Docker compose makes everything simple it requires minimal steps allowing you to simply run
a couple of commands and go on with your life.

```
1. navigate to src in the project and execute the following command
docker-compose build
2. run the application docker-compose up
3. go to the url localhost:8080/version to verify the application.
```


## Deploying Docker with external Postgresql

### AppSettings
The API is now running in a container which means that the connectionstring needs to be modiied
to function properly since the IP / domain has changed.

```
1. navigate to src/api/appsettings.json
2. go to ConnectionStrings and alter the connection string with the name docker.
3. now you will have to adjust the connection string as needed to connect to your 
postgresql server.
```

### Building Docker image

This section shows you how to build your Docker image and test it it to make sure that it works
as intended before deploying it to your hosting provider or cloud service provider.

```
1. docker build -t <account/appname> .
2. verify after success docker ps
```

#### Deploying docker Image to docker

```
1. docker run -p <8080:80> -p <8081:443> <account/appname>
2. access following url http://localhost/healthcheck
3. if postgreSql fails then go to [Altering PostgreSQL Settings](altering-postgresql-settings)
3.1. check error log for other errors to do this read the following section
[Debugging Errors](debugging-errors)
```

#### Debugging Errors

```
1. run following command docker exec -it <CONTAINER ID> /bin/bash
2. navigate to logs folder logs/error
3. now print the log to the console cat error.txt
```

#### Altering PostgreSQL Settings (Skip this step if you are running Postgres in a container)

A few settings have to be altered for the API to function well with docker. This is only
relevant if you are using posgreSQL outside of docker. if your postgresql is stored
in a docker container then check docker documentation on how to connect
multiple containers.

##### Linux Centos 7

This section explains how to alter PostgreSQL settings in Linux Centos 7 to function
properly with the API when it's runnning in a container.

###### pg_hba.cong

```
1. login to psql then enter show hba_file;.
2. alter the following values host all all all md5 or (host all all <Client IP> md5)
3. change ipv4 connection from ident to trust this will
allow other forms of authentication like username and password.
4. now restart postgresql for the setting to take effect.
```

###### postgresql.conf

```
1. locate postgresql.conf file located in PostgreSQL\<Version>\data
2. alter the following values from listen_address='127.0.0.1' to listen_addresses = '*'
3. now
```

#### Windows 10

This section explains how to alter PostgreSQL settings in windows 10 to function
properly with the API when it's runnning in a container.

###### pg_hba.cong

```
1. locat pg_hba.conf file located in PostgreSQL\<Version>\data
2. alter the following values host all all all md5 or (host all all <Client IP> md5)
3. change ipv4 connection from ident to trust this will
allow other forms of authentication like username and password.
4. now restart postgresql for the setting to take effect.
```

###### postgresql.conf

```
1. locate postgresql.conf file located in PostgreSQL\<Version>\data
2. alter the following values from listen_address='127.0.0.1' to listen_addresses = '*'
3. now restart postgresql for the setting to take effect.
```
