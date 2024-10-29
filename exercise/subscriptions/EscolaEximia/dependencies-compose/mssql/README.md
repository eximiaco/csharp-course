# Overview

This is example of Microsoft SQL server in Docker (Linux) container, with initialization script which creates database with schema after start-up.

It's simplified version of [demo which was shown at the Nordic Infrastructure Conference (NIC) 2017 in Oslo, Norway on Feb 3, 2017](https://github.com/twright-msft/mssql-node-docker-demo-app).
Node.js app was removed as well as importing data from _.csv_ file - the point of this example is to show how to run and initialize database.

In my opinion running app which needs access to DB should be done from different Docker image, app should not stay in one Docker image with database.

Unfortunately MsSql doesn't have procedure to run scripts when DB started properly for the first time, [like for example MySQL does](https://www.softwaredeveloper.blog/docker-compose-introduction-dotnet-core-app-composed-with-mysql-database#mysql-db),
so initialization is made with _sleep_ hack:
1. Start DB
2. At the same time run initialization script:
    1. Wait 90seconds to be sure that DB has fully started
    2. Run SQL script via SqlCmd utility

# Running Database
## Clone demo for the first time
First, create a folder on your host and then git clone this project into that folder:
```
git clone https://github.com/SoftwareDeveloperBlog/mssql-docker-initialization-demo
```

## Build image
To run the demo you just need to build the image (from root directory):
```
docker build -t db-demo .
```

## Run container
Then, you need to run the container:
```
docker run -p 1433:1433 -d db-demo
```
If you want to see logs you can skip _-d_ (_detach_) flag, but then be careful with _CTRL+C_ not to stop the container.
You can also run container in detach mode, then check its name or id with `docker ps` and use:
```
docker logs ContainerIdOrName -f
```

You can also use [trick with _xargs_ explained on SoftwareDeveloper.Blog](https://www.softwaredeveloper.blog/docker-logs-from-last-run-container):
```
docker run -p 1433:1433 -d db-demo | xargs docker logs -f
```

We use _-f_ flag to _follow_ new logs.
You can also set your custom name to container, for example:
```
docker run -p 1433:1433 -d --name test-db db-demo
docker logs test-db -f
```
But exactly the same name can be given only once.

## Connect to database
Then you can connect to the _SQL Server_ with _SQL Server Management Studio_ (_SSMS_).
To do it from your host machine provide _localhost_ as Server name.
Choose _SQL Server Authentication_ and provide _sa_ user with password from Dockerfile.

## Troubleshooting
**Problems with SSMS:** [Upgrade _SSMS_ to the newest version. At the time of writing, you need to restart _SSMS_ every time you start new container, to have new internal name of DB updated.](https://www.softwaredeveloper.blog/mssms-invalid-urn-filter-on-server)

**Problems with started container:** [Be careful to have Unix line endings style in scripts which you pass to image, as Windows style may cause problems (usually with not clear message).](https://www.softwaredeveloper.blog/docker-run-problem-no-such-file-or-directory-or-other-strange-message)
