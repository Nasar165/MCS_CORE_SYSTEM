# MCS CRM (NET CORE)
MCS unity CRM is a customer relationship manangemnt system developed by MCS Unity Co.,LTD in .Net
Core with Postgresql. The system functions as a all in one system providing hosting provdiders
& their clients with neccessary tools to keep track of their businesses.

### Getting started
These instructions are aimed at helping you setting up the project for development or testing purposeses.
If you wish to put the project in production please check our Deployment section.

### Prerequisites
The following tools are required for the API to function please make sure that necessary tools
are installed and if not, install them utilizing the proivders main pages.
* [.Net Core](https://dotnet.microsoft.com/download/dotnet-core/3.0) - dotnet core 3 and greater
* [Postgre Sql](https://www.postgresql.org/) - Postgre Sql (Required by default)


### Installing

#### Postgresql
```
Give examples
```
#### SQL Scirpt
This Project requires two SQL databases to function property. The main Database purpose is to 
act as a inhouse database storing clients information such as username, password tokenkeys 
and much more. While the client DB contains all the clients private information. 

##### Runnning scripts
* [MainDB](https://github.com) - Main Script
* [ClientDB](https://github.com) - Client DB Script

##### Dotnet core 3
```
Give examples
```

##### Test application
```
1. Alter application.json.sample file name to application.json
  1. Alter secreatkey in Appsettings minlenght is 8chars
  
  2. Alter mcscon to your sql database changing database, username and password. 
  NOTICE! Server is set to localhost (127.0.0.1)
  
  3. Alter Smtp to your email adding server,username,password if your are using 
  another port than 465 change port and make sure that ssl is set to false if 
  theres not ssl certifecate.
  
2. Run WebAPI
  *. If in Visual studio code press F5 for debug
  *. If command line enter dotnet watch run within mcs_api/mcs.api folder
  
3. Test API in your browser or postman with the following 
   url: https://localhost:5001/version. Expected result is version: x.x.x.x
```

### Deployment
#### Deploying to Linux (IIS)
#### Deploying With Docker (IIS)
#### Deploying to Windows (IIS)

### Built With
This web api was built with the following tools in a Linux Enviroment(CentOs 7).
* [Visual Studio Code](https://code.visualstudio.com/) - Code Editor
* [PostgreSql](https://www.postgresql.org/) - Sql Database 
* [.Net Core](https://dotnet.microsoft.com/) - Dotnet Core Runtime
* [Postmam] (https://getpostman.com/) - API Development Tool (Testing API)

### Contributing

### Versioning
We use [SemVer](http://semver.org/) for versioning. Please visit their website for more 
information to understand how different versions might affect you and your project.

### Authors

* **Nasar Eddaoui** - *Initial work* - [Nasar Eddaoui](https://github.com/Nasar165)

See also the list of [contributors](https://github.com/Nasar165/MCS_CRM_NET_CORE/graphs/contributors) who participated in this project.

### License
This project is licensed under the GNU General Public License v3.0 - see the [LICENSE](LICENSE) file for details

### Acknowledgments
* [Clearvision Properties Co., LTD](https://clearvision-properties.com/) - Providing funding & user feedback. 
* [Goldenmonkey Properties Co., LTD](https://goldenmonkey.asia) - Providining user feedback
* [Hubspot](https://www.hubspot.com) - Inspiration source
* [Chayapat Chuen A Rom ](https://github.com/freedombs) - Angular Developer helping us test the web api 
