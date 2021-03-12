# activity-sign-up
Activity Sign Up web application

# running the application
1. clone the repository to a machine with Docker installed
2. run `./docker-dev-start.sh` to start a docker container with the application running in it.
## or
1. go to `https://activity.acnicholls.com`

# stopping the application
run `./docker-dev-stop.sh`

# purpose
This application was created as an part of an interview process

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
- the repository exposes user secrets for things like database passwords, etc.  It shouldn't, but this needed to work on someone else's machine.  Normally these would/could be environment variables, or in a file on a build server that only a senior developer has access to, or in a key vault on Azure/AWS, basically somewhere safe where any person getting their hands on the repository would not normally have access to them.  
