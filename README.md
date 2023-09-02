#User Story
- I want to be able to register a new employee so that they can be added to the database.
- I want to be able to view the list of all employees to have an overview of who is part of the team.
- I want to be able to edit the information of an existing employee to update data such as name, position, and contact information.
- I want to be able to delete an employee from the system if they leave the company or are no longer needed.

- I want to be able to create an account in the system by providing my name, email address, and a secure password, so that I can access the application's features.
- As a registered user, I want to be able to log in to my account with my email address and password to access the personalized features of the application.

#Libraries
- Dapper
- FluentValidation
- Npgsql
- XUnit
- FluentAssertions
- Moq

#Some cURLs
Login
```
curl --location 'http://localhost:5001/api/login' \
--header 'Content-Type: application/json' \
--data-raw '{
"email": "test@test.com",
"password": "test@test.com"
}'
```

Create User
```
curl --location 'http://localhost:5001/api/register' \
--header 'Authorization: Bearer <token>' \
--header 'Content-Type: application/json' \
--data-raw '{
"name": "Victor",
"email": "victor@victor.com",
"password": "12345"
}'
```

Create Employee
```
curl --location 'http://localhost:5002/api/employees' \
--header 'Content-Type: application/json' \
--data '{
"name": "Victor",
"document": "123456789",
"birthedAt": "1990-01-01"
}'
```

Read Employee
```
curl --location 'http://localhost:5002/api/employees/c34005f5-aba8-466e-9bb0-d220bce9d4ab'
```

Update Employee
```
curl --location --request PATCH 'http://localhost:5002/api/employees/c34005f5-aba8-466e-9bb0-d220bce9d4ab' \
--header 'Content-Type: application/json' \
--data '{
    "name": "Victor 3",
    "document": "10203040",
    "birthedAt": "1991-01-01"
}'
```

Delete Employee
```
curl --location --request DELETE 'http://localhost:5002/api/employees/c34005f5-aba8-466e-9bb0-d220bce9d4ab'
```
