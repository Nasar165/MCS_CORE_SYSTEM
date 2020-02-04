# Deployment

![Xeroxcore logo](https://github.com/Xeroxcore/Xeroxcore/blob/master/resources/images/microsoft-logo.png)

## Introduction

Deploying the API to IIS is not difficult at all it is by far easier than docker and other forms of
deployment. This document outlines how to Deploy the API to IIS in a Windows environment.

## Prerequisites

The following tools are required for the API to function please make sure that necessary toolsa
are installed and if not, install them utilizing the provider's main pages.

- [IIS](https://www.iis.net/) - Internet Information Service
- [ASP.NET 3.5](https://docs.microsoft.com/en-us/iis/get-started/whats-new-in-iis-8/iis-80-using-aspnet-35-and-aspnet-45) - ASP.NET 3.5
- [Windows Hosting Bundle](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-aspnetcore-3.1.1-windows-hosting-bundle-installer) - Windows Hosting Bundle

## IIS

This section outlines how to install the API and its necessary features for it to function properly
Under any circumstances should this be seen a security or configuration guide?

### Add required features

Some features might be missing from you IIS this section will explain how to add features needed to
run net core applications.

#### ASP.NET 3.5

```
1. Open the control panel then click Programs then click Turn Windows features on or off
2. Once the window opens navigate to Internet information service > World Wide Web Services >
Application Development Features then checkbox ASP.NET 3.5
3. Restart IIS and then load the page.
```

#### Windows Hosting Bundle

```
1. go and download the hosting bundle from Windows Hosting Bundle link found in Prerequisites.
2. Run the installer and follow the steps.
3. Restart IIS and then load the app page.
```

## Publish and deploy to IIS

```
1. open cmd and navigate to the API folder where the sln file is located.
2 run following command dotnet publish -c Release -o publish.
3. navigate to the publish folder and copy all the files in the
out folder to your IIS website folder.
4. go to the browser and run yourdomain.com/healthcheck
```
