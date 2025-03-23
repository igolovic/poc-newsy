# poc-newsy
This is a Docker-hosted backend part of the POC news application featuring web API written in .NET Core where data is retrieved using Entity Framework from a relational Postgre SQL database, authentication is achieved using the IdentityServer4 custom-written application. Separate Github project available on this Github profile contains application frontend written in Vue.JS which uses Web API from this project.   

**Functionality**   
Retrieval of all articles, articles by user, single article by ID, adding of new article.   
Retrieval of all users, single user by ID.   
One article can have multiple authors.   
   
**Architecture**   
Chosen architectural pattern is "Clean Architecture" ("Onion Architecture") due to its preference for abstraction instead of concretion which makes it easy to change e.g. UIs or storage technologies. Decoupling and Separation of Concerns is achieved by using it.   
Database consists of three tables Article, User (which are authors of articles), bridging table ArticleUser.   
   
Project https://github.com/igolovic/poc-newsy-editor-viewer contains VUe.js UI for listing and adding articles. It invokes web API and authenticates as client using IdentityServer4 application.   
   
**Prerequisites**   
Docker engine must be installed on PC. Container includes following child-containers: newsy_db (postgres), identityserver (identityserver:dev), api (api:dev). With changing Docker Engine versions and nuget packages it is very likely that after few years this setup will throw various errors. For these errors to be resolved different workarounds might have to be applied. It was shown updating to latest stabile versions of .NET and nuget packages was vital in fixing these errors, also some sytactic  changes to docker-compose.yml were done.    
   
**Components**   
All web applications (web API, custom written IdentityServer4 application, pg4admin tool - Postgre admin tool and UI) are hosted in Docker container:    

![image](https://github.com/user-attachments/assets/eeb1718d-d1c3-4139-a2e9-405c9cc0d2c8)

poc-newsy web API on https://localhost:5001 (Docker)  
pg4admin PostgreSQL UI on http://localhost:5050/browser/ (Docker)  
poc-newsy IdentityServer4 on https://host.docker.internal:44343 (Docker)  
PostgreSQL database (Docker)  

**Development tools**   
GIT, Visual Studio 2022, Docker, PostgreSQL Docker image   
      
**Operation of projects, database, HTTPS in context of Docker**   
Open project in VS and run "Docker Compose" instead of usual "Debug" (Docker engien must be installed and running), perform updates and fixes for error that might arrise to the new versions of Docker, Windows, nuget packages which will might have change and get into conflicts or other problems. If needed, regenerate .cer and .pfx certificates for test-CA, API, IdentityServer4 application, this is done by running root-api-identity-certs.ps1 located in the root folder, follow comments in the Powershell script, validity date in the root.cer, used to sign other certificates, must be valid.    
After eventual errors are fixed,  certificatescopied to container (root.cer, root.pfx, newsy_web.pfx, identityserver.pfx), and database copied and deployed to instance of PostgreSQL, Visual Studio should open website for testing API.    
At this point, all components hosted by Docker container - web API, IdentityServer4 application, PostgreSQL instance hosting database, PostgreSQL-admin - should be running.    
Pre-existing database should be deployed in the instance and visible through PostgreSQL-admin web application.    
Navigate to https://localhost:5001/swagger/index.html to test API.     
pg4admin (UI for PostgreSQL) can be found on http://localhost:5050/browser/ (credentials are in config files in VS).   
Run script test-data-insert.sql to insert test data for the service.   
Application should have automatically copied into the Docker container its set of mentioned certificates needed for HTTPS communication between API, IdentityServer4 application and the poc-newsy-editor-viewer. HTTPS is required - HTTP version does not function properly with IdentityServer4.   
   
**Authentication and authorization**   
It is provided by custom IdentityServer4 (OAuth2/OpenID-Connect) server implementation which protects web API on https://localhost:5001.   
IdentityServer4 enables authentication and authorized access for poc-newsy-editor-viewer application and other Javascript/mobile applications.   
There are two clients supported, one for UI editor-viewer application (https://github.com/igolovic/poc-newsy-editor-viewer), other for any other Javascript/mobile client.   
Client functionality can be emulated and tested using the Postman requests with requests I prepared in the file postman-https-requests-export.zip.   
   
Docker containerization required creation and setting up of several cryptographic objects for web API and IdentityServer4 (https://mjarosie.github.io/dev/2020/09/24/running-identityserver4-on-docker-with-https.html).
      
**Installation**   
- make sure Docker is running
- connect to Docker and on the database run script test-data-insert.sql
- connect to to Swagger on https://localhost:5001/swagger/index.html to check web API is running
   
**TODO**   
- full CRUD web API for articles
- full CRUD web API for users
- automatic adding of test users for demo database
- checking of real users from database during OAuth2/OpenID-Connect check instead of test users
