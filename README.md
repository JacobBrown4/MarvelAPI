<div id="top"></div>
<br/>
<div align="center">
  <a href="https://www.marvel.com/">
    <img src="images/logo.svg" alt="Logo" width="200" height="100">
  </a>
</div>

<!-- HEADER -->
# MarvelAPI Docs
With this API, you can register as a user, then, create and manage character entries in a database of Marvel characters, as well as their appearances on TV and in the MCU<sup>[\[x\]](#resources)</sup>.

<br/>
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li><a href="#creators">Creators</a></li>
    <li><a href="#setup">Setup</a></li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#database-schema">Database Schema</a></li>
    <li>
        <a href="#schemas">Schemas</a>
        <ul>
            <li><a href="#characters">Characters</a></li>
            <li><a href="#movies">Movies</a></li>
            <li><a href="#tv-shows">TV Shows</a></li>
            <li><a href="#movie-appearances">Movie Appearances</a></li>
            <li><a href="#tv-show-appearances">TV Show Appearances</a></li>
            <li><a href="#users">Users</a></li>
        </ul>
    </li>
    <li><a href="#resources">Resources</a></li>
  </ol>
</details>
<br/>

<!-- CREATORS -->
## Creators
- Mary B. -> [github.com/MaryBeeson23](https://github.com/MaryBeeson23)
- Nick A. -> [github.com/avgnand](https://github.com/avgnand)
- Zach D. -> [github.com/zdearmin](https://github.com/zdearmin)

<br />

<!-- SETUP -->
## Setup
To start, you will need to clone the repository into your desired directory. Everything you need to access and run this API is included in this repository. You do not need any external dependencies.

To clone the repository, run this command in your terminal:
```
git clone https://github.com/zdearmin/MarvelAPI.git
```

For interacting with your database, the project includes an implementation of Swagger<sup>[\[x\]](#resources)</sup> to run queries from within your browser. Alternatively, Postman<sup>[\[x\]](#resources)</sup> is a recommended platform that allows you to save multiple customized queries.

With your server running, you can execute the following command to create the database and tables. If your current working directory is the same as the folder with the `.git` file, your command should look like below:
```
dotnet ef database update -p .\MarvelAPI.Data\ -s .\MarvelAPI.WebAPI\
```
<p align="right">(<a href="#top">back to top</a>)</p>

<!-- USAGE -->
## Usage
By running the project file within the `MarvelAPI.WebAPI` assembly, the endpoints will become active, and if your server where you would like to host your data is live as well, you can begin the process of migrating, then posting to the newly created database.

The command to startup the project should look similar to:
``` 
dotnet run --project .\MarvelAPI.WebAPI\
```
This API utilizes JSON Web Tokens (JWT)<sup>[\[x\]](#resources)</sup> for authentication and authorization. All `GET` endpoints are open access. All other endpoints are restricted to users with authorization. Authorization can be granted in the form of a token. Steps to generating a token are listed below:

1. Create a new user at the `POST /api/User/Register` endpoint.

2. Enter your newly created "Username" and "Password" at the `POST /api/Token` enpoint. If the username and password match a valid user, a new token will be generated. The generated token can then be added to the authorization fields in either Postman or Swagger to gain access to remaining CRUD methods.

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- DATABASE SCHEMA -->
## Database Schema
Here is an overview of the tables:
- Characters
    - **Id** - `int`
    - **Name** - `string`
    - **Age** - `string` (e.g., "30s", "Immortal", "Unknown")
    - **Location** - `string` (i.e., current location in MCU)
    - **Origin** - `string` (i.e., homeworld / birthplace)
    - **Abilities** - `string` (comma-separated)
    - **Abilities Origin** - `string` (1-or-2 word description)
    - **Aliases** - `string` (comma-separated)
    - **Status** - `string` (i.e., "Alive", "Dead", "Unknown")
- Movies
    - **Id** - `int`
    - **Title** - `string`
    - **Release Year** - `int`
- TV Shows
    - **Id** - `int`
    - **Title** - `string`
    - **Release Year** - `int`
    - **Number of Seasons** - `int`
- Movie Appearances
    - **Id** - `int`
    - **Character ID** - Foreign Key to Characters table
    - **Movie ID** - Foreign Key to Movies table
- TV Show Appearances
    - **Id** - `int`
    - **Character ID** - Foreign Key to Characters table
    - **TV Show ID** - Foreign Key to TV Shows table
- Users
    - **Id** - `int`
    - **Email** - `string` (i.e., example@gmail.com)
    - **Username** - `string` (minimum length of 4 characters)
    - **Password** - `string` (minimum length of 4 characters)
    - **First Name** - `string`
    - **Last Name** - `string`
    - **Date Created** - `DateTime`
<p align="right">(<a href="#top">back to top</a>)</p>

<!-- SCHEMAS -->
## Schemas
Each table in the database is equipped with functionality for Creating, Reading, Updating, and Deleting (aka CRUD). Details for information needed to complete requests are provided below. All requests and returns are represented in JSON format.

<br />

<!-- CHARACTERS -->
### **Characters**
<br />

Create a new character: 

`POST /api/Character`
- Request completed using `Body`

*Example Request:*
```
{
  "fullName": "string",
  "age": "string"
}
```
<br />

Get all characters:

`GET /api/Character`
- No request `Route` or `Body` required

*Example Response:*
```
[
  {
    "id": 0,
    "fullName": "string"
  }
]
```
<br />

Get a character by Id:

`GET /api/Character/{characterId}`
- Request completed using `Route` *int* characterId

*Example Response:*
```
{
  "id": 0,
  "fullName": "string",
  "age": "string",
  "location": "string",
  "origin": "string",
  "abilities": "string",
  "abilitiesOrigin": "string",
  "aliases": "string",
  "status": "string",
  "movies": [
    {
      "id": 0,
      "title": "string"
    }
  ],
  "tvShows": [
    {
      "id": 0,
      "title": "string"
    }
  ]
}
```

<br />

Get a character by aliases:

`GET /api/Character/Aliases/{aliases}`
- Request completed using `Route` *string* aliases

*Example Response:*
```
[
    {
        "id": 0,
        "fullName": "string",
        "aliases": "string"
    }
]
```

<br />

Get all characters with ability:

`GET /api/Character/Abilities/{ability}`
- Request completed using `Route` *string* ability

*Example Response:*
```
[
  {
        "id": 0,
        "fullName": "string",
        "abilities": "string"
  }
]
```
<br />

Update a character:

`PUT /api/Character/{characterId}}`
- Request completed using `Route` *int* characterId and  `Body`

*The following properties will be left unchanged if the corresponding values in the request body are sent as empty strings*

*Example Request:*
```
{
  "fullName": "string",
  "age": "string",
  "location": "string",
  "origin": "string",
  "abilities": "string",
  "abilitiesOrigin": "string",
  "aliases": "string",
  "status": "string"
}
```
<br />

Delete a character:

`DELETE /api/Character/{characterId}}`
- Request completed using `Route` *int* characterId
- Response will be status 200 OK if successful

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- MOVIES -->
### **Movies**
<br/>
Create a new movie: 

`POST /api/Movie`
- Request completed using `Body`

*Example Request:*
```
{
  "title": "string",
  "releaseYear": 0
}
```
<br />

Get all movies:

`GET /api/Movie`
- No request `Route` or `Body` required

*Example Response:*
```
[
  {
    "id": 0,
    "title": "string"
  }
]
```

<br />

Get a movie by Id:

`GET /api/Movie/{movieId}`
- Request completed using `Route` *int* movieId

*Example Response:*

```
{
  "id": 0,
  "title": "string",
  "releaseYear": 0,
  "characters": [
    {
      "id": 0,
      "fullName": "string"
    }
  ]
}
```

<br />

Get a movie by title:

`GET /api/Movie/{movieTitle}`
- Request completed using `Route` *string* movieTitle

*Example Response:*
```
{
  "id": 0,
  "title": "string",
  "releaseYear": 0,
  "characters": [
    {
      "id": 0,
      "fullName": "string"
    }
  ]
}
```

<br />

Update a movie:

`PUT /api/Movie/{movieId}`
- Request completed using `Route` *int* movieId and `Body`

*Example Request:*
```
{
  "title": "string",
  "releaseYear": 0
}
```

<br />

Delete a movie:

`DELETE /api/Movie/{movieId}`
- Request completed using `Route` *int* movieId
- Response will be status 200 OK if successful

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- TV SHOWS -->
### **TV Shows**

<br/>

Create a new TV show: 

`POST /api/TVShow`
- Request completed using `Body`

*Example Request:*
```
{
  "title": "string",
  "releaseYear": 0,
  "seasons": 0
}
```
<br />

Get all TV shows:

`GET /api/TVShow`
- No request `Route` or `Body` required

*Example Response:*
```
[
  {
    "id": 0,
    "title": "string"
  }
]
```

<br />

Get a TV show by Id:

`GET /api/TVShow/{tvShowId}`
- Request completed using `Route` *int* tvShowId

*Example Response:*

```
{
  "id": 0,
  "title": "string",
  "releaseYear": 0,
  "seasons": 0,
  "characters": [
    {
      "id": 0,
      "fullName": "string"
    }
  ]
}
```

<br />

Get a TV show by title:

`GET /api/TVShow/{tvShowTitle}`
- Request completed using `Route` *string* tvShowTitle

*Example Response:*
```
{
  "id": 0,
  "title": "string",
  "releaseYear": 0,
  "seasons": 0,
  "characters": [
    {
      "id": 0,
      "fullName": "string"
    }
  ]
}
```

<br />

Update a TV show:

`PUT /api/TVShow/{tvShowId}`
- Request completed using `Route` *int* tvShowId and `Body`

*Example Request:*
```
{
  "title": "string",
  "releaseYear": 0,
  "seasons": 0
}
```

<br />

Delete a TV show:

`DELETE /api/tvShow/{tvShowId}`
- Request completed using `Route` *int* tvShowId
- Response will be status 200 OK if successful

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- MOVIE APPEARANCES -->
### **Movie Appearances**

<br />

Create a new movie appearance:

`POST /api/MovieAppearance`
-  Request completed using `Body`

*Example Request:*
```
{
  "characterId": 0,
  "movieId": 0
}
```

<br />

Get all movie appearances:

`GET /api/MovieAppearance`
- No request `Route` or `Body` required

*Example Response:*
```
[
  {
    "id": 0,
    "character": "string",
    "movie": "string"
  }
]
```

<br />

Get a movie appearance by Id:

`GET /api/MovieAppearance/{movieAppearanceId}`
- Request completed using `Route` *int* movieAppearanceId

*Example Response:*
```
{
  "id": 0,
  "characterId": 0,
  "character": "string",
  "movieId": 0,
  "movie": "string"
}
```

<br />

Update a movie appearance:

`PUT /api/MovieAppearance/{movieAppearanceId}`
- Request completed using `Route` *int* movieAppearanceId and `Body`

*Example Request:*
```
{
  "characterId": 0,
  "movieId": 0
}
```

<br />

Delete a movie appearance:

`DELETE /api/MovieAppearance/{movieAppearanceId}`
- Request completed using `Route` *int* movieAppearanceId
- Response will be status 200 OK if successful

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- TV SHOW APPEARANCES -->
### **TV Show Appearances**

<br />

Create a new TV show appearance:

`POST /api/TVShowAppearance`
-  Request completed using `Body`

*Example Request:*
```
{
  "characterId": 0,
  "tvShowId": 0
}
```

<br />

Get all TV show appearances:

`GET /api/TVShowAppearance`
- No request `Route` or `Body` required

*Example Response:*
```
[
  {
    "id": 0,
    "character": "string",
    "tvShow": "string"
  }
]
```

<br />

Get a TV show appearance by Id:

`GET /api/TVShowAppearance/{tvShowAppearanceId}`
- Request completed using `Route` *int* tvShowAppearanceId

*Example Response:*
```
{
  "id": 0,
  "characterId": 0,
  "character": "string",
  "tvShowId": 0,
  "tvShow": "string"
}
```

<br />

Update a TV show appearance:

`PUT /api/TVShowAppearance/{tvShowAppearanceId}`
- Request completed using `Route` *int* tvShowAppearanceId and `Body`

*Example Request:*
```
{
  "characterId": 0,
  "tvShowId": 0
}
```

<br />

Delete a TV show appearance:

`DELETE /api/TVShowAppearance/{tvShowAppearanceId}`
- Request completed using `Route` *int* tvShowAppearanceId
- Response will be status 200 OK if successful

<br />

<!-- USERS -->
### **Users**

<br />

Generate a new token:

`POST /api/Token`
-  Request completed using `Body`

*Example Request:*
```
{
  "username": "string",
  "password": "string"
}
```

<br />

Create a new user:

`POST /api/User/Register`
-  Request completed using `Body`

*Example Request:*
```
{
  "email": "user@example.com",
  "username": "string",
  "password": "string",
  "confirmPassword": "string"
}
```

<br />

Get all users:

`GET /api/User`
- No request `Route` or `Body` required

*Example Response:*
```
[
  {
    "id": 0,
    "username": "string",
    "email": "string",
    "firstName": "string",
    "lastName": "string",
    "dateCreated": "2022-07-26T17:58:53.980Z"
  }
]
```

<br />

Get a user by Id:

`GET /api/User/{userId}`
- Request completed using `Route` *int* userId

*Example Response:*
```
{
  "id": 0,
  "username": "string",
  "email": "string",
  "firstName": "string",
  "lastName": "string",
  "dateCreated": "2022-07-26T18:01:56.579Z"
}
```

<br />

Update a user:

`PUT /api/User/{userId}`
- Request completed using `Route` *int* userId and `Body`

*Example Request:*
```
{
  "username": "string",
  "email": "user@example.com",
  "password": "string",
  "firstName": "string",
  "lastName": "string"
}
```

<br />

Delete a user:

`DELETE /api/User/{userId}`
- Request completed using `Route` *int* userId
- Response will be status 200 OK if successful

<br />

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- RESOURCES -->
## Resources
Below is a list of resources we would like to give credit to as each one played a role in the development of this API.
- [Marvel](https://www.marvel.com/)
- [Swagger](https://swagger.io/)
- [Postman](https://www.postman.com/)
- [JWT](https://jwt.io/introduction/)
- [ASP.NET](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0)
- [Visual Studio Code](https://code.visualstudio.com/)
- [GitHub](https://github.com/)
- [Trello](https://trello.com/)
- [dbdiagram](https://dbdiagram.io/home)
<p align="right">(<a href="#top">back to top</a>)</p>