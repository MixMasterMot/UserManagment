# UserManagment
The backend for the user management application
It uses a mongodb to store user information.
The mongodb can easily be spun up by navigating to the docker folder and runing:
 docker compose up -d
This contains some seed data.
There are 2 users that get generated as a part of this proccess:
{
    UserName: "admin",
    Password: "password"
}
{
    UserName: "string",
    Password: "string"
}

The second is convenient as it is the default values used by swagger

All endpoints are documented in swagger.
You need too obtain a token via logging in too do almost anything with the endpoints.

There are some TODOs in the code where it can be improved.

There is pageing on the GetUsers endpoint but it is not exersized by the frontend app.