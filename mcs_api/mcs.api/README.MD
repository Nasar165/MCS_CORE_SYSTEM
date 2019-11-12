# MCS API

## Prerequisites
The following knowledge is necessary to be able to utilize the web api.
lacking knowledge in one section might lead to difficulties when trying
to fetch/post data to the API.

* JSON Data
* HTTP Clients
* Bearer Authenticaton

<!--ts-->
## Table of Contents
* [Common Server Responses]()
* [Authentication]()
  * [Tokenkey]()
  * [AdminKey]()
* [Core Resourses]()
  * [Apartment]()
<!--te-->

## Common Server Responses
| Error Code | Description |
| :---: | --- |
| `200` | OK : Request was successful |
| `204` | No Content : The request response is empty |
| `400` | Bad request :  |
| `401` | Unauthorized : user not authenticated |
| `403` | Forbidden : user dont have the right privilages |
| `404` | Not found : The url does not exist or has been moved |
| `415` | Unsupported Media Type : Data is in format that is not supported |
| `429` | Too many requests : To many requests within a x amount of time |
| `500` | Internal Server Error  : An error has occured server side |

## Authentication
This section contains information regarding authentication. It aims at
helping you to get a token key and then utilize it within the header to
either fetch, put, delete or create data.

### Tokenkey
API token key is a key that is used for third party sites refered to as 
external domains that dont require administrative rights. sites with
token keys have limited access and is mostly  used to fetch data.
#### Sample API Entry
| Info | Description |
| ------------- | ------------- |
| `Title` | Get a API Token |
| `Url` | /apiauth |
| `Method` | POST |
| `Url Parameters` | TokenKey:string {Required} <br/> GroupKey:int {Required} |
| `Success Response` | Status Code: 200 <br/> Response: {TokenKey:"Key", authType:"Bearer", expiration: datetime}|
| `Error Response` | Status Code: 401 |

#### Sample API Request
```
Add Sample Here
```
### Useraccount
A useraccount token key poses more rights then a token key it is used for
adminiistrative purposes and thus has rights to access all controllers.
Admink keys also have rights to put,create,delete rights if they are of the
correct role.
#### Sample Useraccount Entry
| Info | Description |
| ------------- | ------------- |
| `Title` | Get a user Token |
| `Url` | /userauth |
| `Method` | POST |
| `Url Parameters` | username:string {Required} <br/> password:string {Required} |
| `Success Response` | Status Code: 200 <br/> Response: {TokenKey:"Key", authType:"Bearer", expiration: datetime}|
| `Error Response` | Status Code: 401 |
```
Add Sample Here
```
## Core Resourses
### Apartments

#### ADMIN KEY COMMANDS
| Info | Description |
| ------------- | ------------- |
| `Title` | Get all apartments |
| `Url` | /apartments |
| `KeyType` | Admin Key |
| `Method` | GET |
| `Success Response` | Status Code: 200 <br/> Returns an array of apartments |
| `Error Response` | Status Code: 401 : Authenticate again <br/> Status Code 403 : Wrong Key type |

#### Sample output
```
[
  {
    "unit_Id": "U234",
    "resale": true,
    "unit_Type_Id": 3,
    "building": "A32",
    "bathroom": 1,
    "bedroom": 2,
    "size": 43.33,
    "landSize": 0,
    "floor": 2,
    "ownership_Type_Id": 1,
    "ref_Id": 34,
    "title": "Slendid apartment with sea view
    "description": "a great opportunity for a seaview apartment comprissing
    of one room with all the amneties that you might need to live a long 
    and happy life.",
    "rating": 4
    "price": 2500000
    "parent_Ref_Id": 1,
    "address_Id": 1,
    "vr_Image": "https://matterport.com/asdusadajd#
    "main_Image": "1/34/auction.png",
    "website": true
  }
  {
    "unit_Id": "U233,
    "resale": true,
    "unit_Type_Id": 3,
    "building": "A32",
    "bathroom": 1,
    "bedroom": 2,
    "size": 43.33,
    "landSize": 0,
    "floor": 2,
    "ownership_Type_Id": 1,
    "ref_Id": 34,
    "title": "Great investment opportunity whith sea view
    "description": "a great opportunity for a seaview apartment comprissing
    of one room with all the amneties that you might need to live a long 
    and happy life.",
    "rating": 4
    "price": 4500000
    "parent_Ref_Id": 1,
    "address_Id": 1,
    "vr_Image": "https://matterport.com/132dsadewqe2#
    "main_Image": "1/34/auction.png",
    "website": true
  } 
]
```

| Info | Description |
| ------------- | ------------- |
| `Title` | Get singel apartment |
| `Url` | /apartments/{ref_id} |
| `KeyType` | Admin Key |
| `Method` | GET |
| `Success Response` | Status Code: 200 <br/> Returns a single apartment |
| `Error Response` | Status Code: 401 : Authenticate again <br/> Status Code 403 : Wrong Key type |

#### Sample output
```
{
    "unit_Id": "U234",
    "resale": true,
    "unit_Type_Id": 3,
    "building": "A32",
    "bathroom": 1,
    "bedroom": 2,
    "size": 43.33,
    "landSize": 0,
    "floor": 2,
    "ownership_Type_Id": 1,
    "ref_Id": 34,
    "title": "Slendid apartment with sea view
    "description": "a great opportunity for a seaview apartment comprissing
    of one room with all the amneties that you might need to live a long 
    and happy life.",
    "rating": 4
    "price": 2500000
    "parent_Ref_Id": 1,
    "address_Id": 1,
    "vr_Image": "https://matterport.com/asdusadajd#
    "main_Image": "1/34/auction.png",
    "website": false
}
```

#### API KEY COMMANDS
| Info | Description |
| ------------- | ------------- |
| `Title` | Get all apartments where website is true|
| `Url` | /apartments/getapartments |
| `KeyType` | API Key |
| `Method` | GET |
| `Success Response` | Status Code: 200 <br/> Returns an array of apartments where website is true |
| `Error Response` | Status Code: 401 : Authenticate again <br/> Status Code 403 : Wrong Key type|

#### Sample output
```
[
  {
    "unit_Id": "U234",
    "resale": true,
    "unit_Type_Id": 3,
    "building": "A32",
    "bathroom": 1,
    "bedroom": 2,
    "size": 43.33,
    "landSize": 0,
    "floor": 2,
    "ownership_Type_Id": 1,
    "ref_Id": 34,
    "title": "Slendid apartment with sea view
    "description": "a great opportunity for a seaview apartment comprissing
    of one room with all the amneties that you might need to live a long 
    and happy life.",
    "rating": 4
    "price": 2500000
    "parent_Ref_Id": 1,
    "address_Id": 1,
    "vr_Image": "https://matterport.com/asdusadajd#
    "main_Image": "1/34/auction.png",
    "website": true
  }
  {
    "unit_Id": "U233,
    "resale": true,
    "unit_Type_Id": 3,
    "building": "A32",
    "bathroom": 1,
    "bedroom": 2,
    "size": 43.33,
    "landSize": 0,
    "floor": 2,
    "ownership_Type_Id": 1,
    "ref_Id": 34,
    "title": "Great investment opportunity whith sea view
    "description": "a great opportunity for a seaview apartment comprissing
    of one room with all the amneties that you might need to live a long 
    and happy life.",
    "rating": 4
    "price": 4500000
    "parent_Ref_Id": 1,
    "address_Id": 1,
    "vr_Image": "https://matterport.com/132dsadewqe2#
    "main_Image": "1/34/auction.png",
    "website": true
  } 
]
```

| Info | Description |
| ------------- | ------------- |
| `Title` | Get singel apartment |
| `Url` | /apartments/getapartment/{ref_id} |
| `KeyType` | API Key |
| `Method` | GET |
| `Success Response` | Status Code: 200 <br/> Returns a single apartment where website is true |
| `Error Response` | Status Code: 401 : Authenticate again <br/> Status Code 403 : Wrong Key type|

#### Sample output
```
{
    "unit_Id": "U234",
    "resale": true,
    "unit_Type_Id": 3,
    "building": "A32",
    "bathroom": 1,
    "bedroom": 2,
    "size": 43.33,
    "landSize": 0,
    "floor": 2,
    "ownership_Type_Id": 1,
    "ref_Id": 34,
    "title": "Slendid apartment with sea view
    "description": "a great opportunity for a seaview apartment comprissing
    of one room with all the amneties that you might need to live a long 
    and happy life.
    "rating": 4
    "price": 2500000
    "parent_Ref_Id": 1,
    "address_Id": 1,
    "vr_Image": "https://matterport.com/asdusadajd#
    "main_Image": "1/34/auction.png",
    "website": true
}
```

#### Tools
| Command | Description |
| --- | --- |
| git status | List all new or modified files |
| git diff | Show file differences that haven't been staged |

| First Header  | Second Header |
| ------------- | ------------- |
| Content Cell  | Content Cell  |
| Content Cell  | Content Cell  |

| Command | Description |
| --- | --- |
| `git status` | List all *new or modified* files |
| `git diff` | Show file differences that **haven't been** staged |

| Left-aligned | Center-aligned | Right-aligned |
| :---         |     :---:      |          ---: |
| git status   | git status     | git status    |
| git diff     | git diff       | git diff      |

| Name     | Character |
| ---      | ---       |
| Backtick | `         |
| Pipe     | \|        |
