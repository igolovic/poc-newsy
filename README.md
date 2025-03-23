# poc-newsy
Docker-hosted set of applications representing POC news application made of .NET Core RESTful web API, Entity Framework Core, PostgreSQL database, and custom IdentityServer4 application for authentication. Separate Github project available on this Github profile contains application frontend written in Vue.JS which uses Web API from this project.   

**Functionality**   
Retrieval of all articles, articles by user, single article by ID, adding of new article.   
Retrieval of all users, single user by ID.   
One article can have multiple authors.   
   
**Architecture**   
Architectural pattern is "Clean Architecture" ("Onion Architecture, chosen due to its preference for abstraction instead of concretion which makes it easy to change e.g. UIs or storage technologies. Decoupling and separation-of-concerns is achieved by using it.    
Database consists of tables Article, User (represents authors of articles), ArticleUser (bridging table).    
   
Project https://github.com/igolovic/poc-newsy-editor-viewer contains VUe.js UI for listing and adding articles. It invokes web API and authenticates as client using IdentityServer4 application.   
   
**Prerequisites**   
Docker engine must be installed on PC. 
With changing Docker Engine versions and nuget packages, it is very likely that after few years this setup will throw various errors. For these errors to be resolved different workarounds might have to be applied. Updating to latest stabile versions of .NET and nuget packages was vital in fixing these errors so far, also some syntactic changes to docker-compose.yml were neccessary.    
   
**Components**   
All web applications are therefore hosted in Docker container: 
1 - web API -> in Docker: api (api:dev) -> URL: https://localhost:5001     
2 - custom written IdentityServer4 application -> in Docker: identityserver (identityserver:dev) -> URL: https://host.docker.internal:44343    
3 - PostgreSQL-instance with database -> in Docker: newsy_db (postgres)    
4 - PostgreSQl UI (pg4admin) -> in DOcker: dpage/pgadmin4:latest (pgadmin4_container) -> URL: http://localhost:5050/browser/    
     
![image](https://github.com/user-attachments/assets/eeb1718d-d1c3-4139-a2e9-405c9cc0d2c8)
       
**Installation and troubleshooting of projects, certificates, database in Docker container**   
1 - Open project in VS and run "Docker Compose" instead of usual "Debug", Docker engine must be installed and running.     
   
2 - Perform updates and fixes for error that might arrise to the new versions of Docker, Windows, nuget packages which will might have change and get into conflicts or other problems.     
    
3 - Run root-api-identity-certs.ps1 located in the root folder in admin-mode Powershell. This will install certificates to local PC's store and regenerate .cer and .pfx certificates for test-CA, API, IdentityServer4 application, so they cann be copied to container. See comments in the Powershell script for detailed explanation.    
After all certificates are generated (root.cer, root.pfx, newsy_web.pfx, identityserver.pfx), "Docker Compose" will have automatically copied the certificates. They are needed for HTTPS communication between API, IdentityServer4 application and the poc-newsy-editor-viewer. HTTPS is required - HTTP version does not function properly with IdentityServer4.   
    
![image](https://github.com/user-attachments/assets/4222999f-8afc-4815-82f5-71442ed54717)
        
4 - After eventual errors are fixed, certificates successfully copied, and database deployed to instance of PostgreSQL, Visual Studio should open website for testing API: https://localhost:5001/swagger/index.html to test API.     
    
5 - Run script test-data-insert.sql to insert test data for the service. pg4admin (UI for PostgreSQL) can be found on http://localhost:5050/browser/ (credentials are in config files in VS).    
   
**Authentication and authorization**   
It is provided by custom IdentityServer4 (OAuth2/OpenID-Connect) server implementation which protects web API on https://localhost:5001.   
IdentityServer4 enables authentication and authorized access for poc-newsy-editor-viewer application and other Javascript/mobile applications.   
There are two clients supported, one for UI editor-viewer application (https://github.com/igolovic/poc-newsy-editor-viewer), other for any other Javascript/mobile client.     
Setup of IdentityServer4-authentication in Docker and related certificates was significantly helped by article: https://mjarosie.github.io/dev/2020/09/24/running-identityserver4-on-docker-with-https.html.        
    
**Development tools**   
GIT, Visual Studio 2022, Docker, PostgreSQL Docker image.   
    
**TODO**   
- full CRUD web API for articles    
- full CRUD web API for users    
- automatic adding of test users for demo database ("seed")    
- checking of real users from database during OAuth2/OpenID-Connect check instead of test users    
