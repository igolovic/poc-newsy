# poc-newsy
This is a POC news application consisting of frontend written in Vue.JS which calls web API written in .NET Core, data is retrieved using Entity Framework from a relational database on SQL Server instance. Architecture follows "Clean Architecture" pattern.
   
**Functionality**   
Retrieval of all articles, articles by user, single article by ID, adding of new article.   
Retrieval of all users, single user by ID.   
One article can have multiple authors.
   
**Prerequisites**   
Docker installed on PC - PostgreSQL Docker image is pulled by Visual Studio and used to set up database by the application, also, applicaiton is hosted in Docker container   
      
**Development tools**   
GIT, Visual Studio 2022, Docker   
      
**Operation**   
Open project in VS and run "Docker Compose" instead of usual "Debug" (Docker must be running), VS starts configured image/container for web API and PostgreSQL.
Navigate to https://localhost:5001/swagger/index.html to test API, container is also configured to host pg4admin (UI for PostgreSQL) which can be found on http://localhost:5050/browser/ (credentials are in config files in VS).   
Run script test-data-insert.sql¸to insert test data for the service.   
   
**Architecture**  
Chosen pattern is "Clean Architecture" ("Onion Architecture") due to its preference for abstraction instead of concretion which makes it easy to change e.g. UIs or storage technologies.   
Also, a decoupling and Separation of Concerns is achieved by using it.   
Database consists of three tables Article, User (which are authors of articles), bridging table ArticleUser.

Project https://github.com/igolovic/poc-newsy-editor-viewer contains UI for listing articles and adding new articles and also uses this web API.   
   
**TODO**   
- full CRUD web API for articles
- full CRUD web API for users
- automatic adding of test data for demo database
