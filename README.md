# activity-sign-up
Activity Sign Up web application

# running the application
## In full development mode
caveats to running the application in development are that you will need the following installed
1. Docker 
2. Git
3. Visual Studio
4. VS Code
5. SQL Server
6. dotnetcore 3.1
7. nodejs

to do this you will need to do the following thing, (which are not written in lines of code, because if you have those things installed, I trust you know what I mean)
1. clone the repository 
2. create a database on your server, a login and user for the application
3. restore the basic database using `/db/create_restore.sql`
4. using Visual Studio, open `/api/ActivitySignUpApi.sln`
5. run the Visual Studio IIS Express option to watch the API process the data passed to/from db and client
6. open a unix terminal in the root folder
7. run `./docker-dev-start.sh` to start a docker container with the proxy and client services running in it.


## In "Demo" Mode
You can also run the application  on a machine with only the following installed
1. Docker Desktop
2. Git & git bash

todo this you run the following command in git bash to start a docker container at http://localhost that uses the API, client, db and proxy almost as it would in production, as in this case the database is built into a container, and is inacessible to a database administrator.
1. clone the repository
2. run `/.docker-local-start.sh`, 


## My Demo
the database for this demo is the same as the one in the local (built into a container on the docker host) and gets replaced every time I push out a newer version of the database container.  I have pre-seeded it with 5 events for 2022 that should disappear after their target date.  Others should be able to be added with the plus in the top right.
1. go to [`https://activity.acnicholls.com`](https://activity.acnicholls.com)

# stopping the application
run `./docker-dev-stop.sh` or `./docker-local-stop.sh`

# removing the application from your docker instance
run `./docker-dev-down.sh` or `./docker-local-down.sh`

# purpose
This application was created as an part of an interview process, and then I decided to keep upgrading the original submission to practice Angular TypeScript

# project timeline
about 70 hrs total
 - api ~ 20 hrs 
 - client ~ 35 hrs
 - debugging ~ 10 hrs
 - dockerizing ~ 5 hrs
 I won't bother with maintenance time of the demo site.  It's still an ongoing process. 

# overview
The project expects little input from the user, only their basic contact details.  Name and Email.  That will create a user record for the Activity they have chosen, and they can then begin to communicate with the other participants via the comments section.  As the date for an activity is reached, it will drop off the list, and users can create new activities with a single click and some data.

# documentation
you will find the project desciription in [this](https://github.com/acnicholls/activity-sign-up/tree/master/docs) folder, along will the documentation that I created to outline my process, my thoughts on the UI and the database.  The documentation is less than I would try to give for a project with a longer timeline, but due to the time constraint, it was mostly hand-written.  I normally would transcribe the UML to Visio documents, and for the UI I normally would expect the client to provide "what they want" but since I have recently learned about Figma, I will soon begin to use that.

# the application
this application is an Angular 11 user interface with a dotnetcore5.0 C# REST Web Api, which stores it's data in a SQL Server database, built using the Onion architecture pattern.  There is an NGINX reverse proxy handling all the requests for both the front end and the api, to alleviate CORS issues and protect the other ports of the remaining containers.

# my vision
my vision was to produce a website that displayed a list of available activities when the user first visits.  The user would click on an activity and be taken to a page that would display more in-depth details about the activity as well as a sign-up form.  If the user submits the sign up form, the page then switches the view of the activity to a list of participants on the left, with a list of comments on the right.  

# possible future enhancements
- real-time comments/chat window, via a websocket project
- add a splash page where the user enters their details to start, use that data as the 'single sign on' for all activities.  User then clicks a button to sign up for events

# development enhancements
- add a webpacker image to the docker container, to make live-reloading much faster.

# caveats
- the repository exposes user secrets for things like database passwords, etc.  It shouldn't, but this is needed to work on someone else's machine in the local demo mode..  Normally these would/could be environment variables, or in a file on a build server that only a senior developer has access to, or in a key vault on Azure/AWS, basically somewhere safe where any person getting their hands on the repository would not normally have access to them.  In this case since the server is entirely closed to modifacation after release
