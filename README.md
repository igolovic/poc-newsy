# poc-newsy
Docker-hosted fullstack POC news application featuring Vue.js frontend SPA and web API written in .NET Core where data is retrieved using Entity Framework from a relational Postgre SQL database. Frontend is implemented using Vue.js 2.6 with bootstrap-vue for UI.   
  
## Functionality
Retrieval of all articles, articles by user, single article by ID, adding of new article.   
Retrieval of all users, single user by ID.   
One article can have multiple authors.   
   
**Architecture**   
FRONTEND      
Frontend is a SPA multi-component application following organization layed out by the Vue.js CLI.      
   
BACKEND   
Architectural pattern is "Clean Architecture" ("Onion Architecture") due to its preference for abstraction instead of concretion which makes it easy to change e.g. UIs or storage technologies. Decoupling and Separation of Concerns is achieved by using it.   
Database consists of three tables Article, User (which are authors of articles), bridging table ArticleUser.   
      
Project https://github.com/igolovic/poc-newsy-editor-viewer contains VUe.js UI for listing and adding articles. It invokes web API and authenticates as client using IdentityServer4 application.   
   
**Prerequisites**   
Vue.js 2.6 CLI   
Bootstrap-vue 2.22.0   
poc-newsy web API on https://localhost:5001 (Docker)
poc-newsy IdentityServer4 on https://host.docker.internal:44343 (Docker)
pg4admin PostgreSQL UI on http://localhost:5050/browser/ (Docker)
PostgreSQL database (Docker)

Docker engine must be installed on PC - PostgreSQL Docker image is pulled by Visual Studio and used to set up database by the application. All web applications (web API, custom written IdentityServer4 application, pg4admin tool - Postgre admin tool and UI) are hosted in Docker container.   
   
**Components**   
editor-viewer frontend web application on https://localhost:8080/
poc-newsy web API on https://localhost:5001 (Docker)  
poc-newsy IdentityServer4 on https://host.docker.internal:44343 (Docker)  
pg4admin PostgreSQL UI on http://localhost:5050/browser/ (Docker)  
PostgreSQL database (Docker)  

**Development tools**   
Visual Studio Code, Visual Studio 2022, GIT, Docker, PostgreSQL Docker image   
      
**Operation**   
FRONTEND AND BACKEND   
Run the application in VS Code to connect to web applications in Docker container (https://github.com/igolovic/poc-newsy/blob/master/README.md). Add articles by selecting users/authors (one article can have multiple authors) and by entering plain text or HTML as article content.   

BACKEND ONLY   
Open project in VS and run "Docker Compose" instead of usual "Debug" (Docker must be running), VS starts configured image/container for web API and PostgreSQL.
Navigate to https://localhost:5001/swagger/index.html to test API, container is also configured to host pg4admin (UI for PostgreSQL) which can be found on http://localhost:5050/browser/ (credentials are in config files in VS).   
Run script test-data-insert.sql¸to insert test data for the service.   
Application will install into the Docker container its own set of certificates needed for HTTPS (HTTPS is required - HTTP version does not function properly in IdentityServer4).   
   
**Authentication and authorization**   
They are provided by custom IdentityServer4 (OAuth2/OpenID-Connect) implementation which protects web API on https://localhost:5001.   
IdentityServer4 enables authentication and authorized access for editor-viewer application and other Javascript/mobile applications.   
There are two clients supported, one for UI editor-viewer application (https://github.com/igolovic/poc-newsy-editor-viewer), other for any other Javascript/mobile client (can be tested using the Postman requests using the file postman-https-requests-export.zip).   
   
Docker containerization required creation and setting up of several cryptographic objects for web API and IdentityServer4 (https://mjarosie.github.io/dev/2020/09/24/running-identityserver4-on-docker-with-https.html).
      
**Installation**   
- make sure Docker is running
- connect to Docker and on the database run script test-data-insert.sql
- connect to to Swagger on https://localhost:5001/swagger/index.html
   
**TODO**   
- full CRUD web API for articles
- full CRUD web API for users
- automatic adding of test users for demo database
- checking of real users from database during OAuth2/OpenID-Connect check instead of test users
