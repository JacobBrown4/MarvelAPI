# MarvelAPI Docs
With this API, you can create and manage character entries in a database of Marvel characters, as well as their appearances on TV and in the MCU.

## Creators
- Mary B -> [github.com/MaryBeeson23](https://github.com/MaryBeeson23)
- Nick A -> [github.com/avgnand](https://github.com/avgnand)
- Zach D -> [github.com/zdearmin](https://github.com/zdearmin)

## Usage
For interacting with your database, the project includes an implementation of Swagger<sup>[\[x\]](#resources)</sup> to run queries from within your browser. Alternatively, Postman<sup>[\[x\]](#resources)</sup> is a recommended platform that allows you to save multiple customized queries.

By running the project file within the `MarvelAPI.WebAPI` assembly, the endpoints will become active, and if your server where you would like to host your data is live as well, you can begin the process of migrating, then posting to the newly created database.

The command to startup the project should look similar to:
``` 
dotnet run --project .\MarvelAPI.WebAPI\
```

## Initial Migration
With your server running, you can execute the following command to create the database and tables. If your current working directory is the same as the folder with the `.git` file, your command should look like below:
```
dotnet ef database update -p .\MarvelAPI.Data\ -s .\MarvelAPI.WebAPI\
```

## Database Schema
Here is an overview of the tables:
- Characters
    - **Name** - `string`
    - **Age** - `string` (e.g., "30s", "Immortal", "Unknown")
    - **Location** - `string` (i.e., current location in MCU)
    - **Origin** - `string` (i.e., homeworld / birthplace)
    - **Abilities** - `string` (comma-separated)
    - **Abilities Origin** - `string` (1-or-2 word description)
- Movies
    - **Title** - `string`
    - **Release Year** - `int`
- TV Shows
    - **Title** - `string`
    - **Release Year** - `int`
    - **Number of Seasons** - `int`
- Movie Appearances
    - **Character ID** - Foreign Key to Characters table
    - **Movie ID** - Foreign Key to Movies table
- TV Show Appearances
    - **Character ID** - Foreign Key to Characters table
    - **TV Show ID** - Foreign Key to TV Shows table

## Resources
- [Postman](https://www.postman.com/)
- [Swagger](https://swagger.io/)