# activity-sign-up
Activity Sign Up web application programming interface

# intention
this application is an attempt to replicate the onion architecture

it uses dapper with ambient context to access the SQL database using stored procedures from the database

the controllers call the service layer with it's model to execute an action

the service layer calls the validation layer and if the model is valid, the repository method is called

some tests are written which can be executed at build time to determine if all the classes do what they are supposed to




