# poc-newsy
This is a POC news application consisting of frontend written in Vue.JS which calls web API written in .NET Core, data is retrieved using Entity Framework from a relational database on SQL Server instance. Architecture follows "Clean Architecture" pattern.

**Functionality**   
Retrieval of all articles, articles by user, single article by ID, adding of new article.   
Retrieval of all users, single user by ID.   
One article can have multiple authors.   
   
**Architecture**   
Chosen pattern is "Clean Architecture" ("Onion Architecture") due to its preference for abstraction instead of concretion which makes it easy to change e.g. UIs or storage technologies.   
Also, a decoupling and Separation of Concerns is achieved by using it.   
Database consists of three tables Article, User (which are authors of articles), bridging table ArticleUser.   
   
Project https://github.com/igolovic/poc-newsy-editor-viewer contains UI for listing articles and adding new articles and also uses this web API.   
   
**Prerequisites**   
Docker installed on PC - PostgreSQL Docker image is pulled by Visual Studio and used to set up database by the application. All web applications (web API, custom written IdentityServer4 application, pg4admin tool) are hosted in Docker container.   
   
**Components**   
poc-newsy web API on https://localhost:5001 (Docker)
poc-newsy IdentityServer4 on https://host.docker.internal:44343 (Docker)
pg4admin PostgreSQL UI on http://localhost:5050/browser/ (Docker)
PostgreSQL database (Docker)

**Development tools**   
GIT, Visual Studio 2022, Docker   
      
**Operation**   
Open project in VS and run "Docker Compose" instead of usual "Debug" (Docker must be running), VS starts configured image/container for web API and PostgreSQL.
Navigate to https://localhost:5001/swagger/index.html to test API, container is also configured to host pg4admin (UI for PostgreSQL) which can be found on http://localhost:5050/browser/ (credentials are in config files in VS).   
Run script test-data-insert.sql¸to insert test data for the service.   
Application will install into Docker container its own set of certificates needed for HTTPS (HTTPS is required - HTTP version does not to function properly in IdentityServer4).   
   
**Authentication and authorization**   
They are provided by custom IdentityServer4 (OAuth2/OpenID-Connect) server implementation which protects web API on https://localhost:5001.   
IdentityServer4 enables authentication and authorized access for editor-viewer application and other Javascript/mobile applications.   
There are two clients supported, one for UI editor-viewer application (https://github.com/igolovic/poc-newsy-editor-viewer), other for any other Javascript/mobile client (can be tested using the Postman requests in from file postman-https-requests-export.zip).   
      
**Installation**   
- make sure Docker is running
- connect to Docker and on the database run script test-data-insert.sql
- connect to to Swagger on https://localhost:5001/swagger/index.html
   
**TODO**   
- full CRUD web API for articles
- full CRUD web API for users
- automatic adding of test users for demo database
- checking of real users from database during OAuth2/OpenID-Connect check instead of test users
