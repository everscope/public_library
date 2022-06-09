
# Public Library

Public Library is ASP.NET Core Web API created to do book traking and borrowing and tracking patrons for a public library.

## How to run

Go to the folder where you want to save the project and open the Command line there (or press Win + R, enter cmd, and click run, with cd command move to wanted folder).

**_Notice, that you have to have Git installed on your PC_**

In console, print:

    git clone https://github.com/everscope/public_library.git

After the project has been cloned, print this:

    cd public_library/"Public Library"
    dotnet run

In the console, among logs, you will be able to find this text with the address:

    Microsoft.Hosting.Lifetime[14]
    Now listening on: https://localhost:7079
    info: Microsoft.Hosting.Lifetime[14]
    Now listening on: http://localhost:5079
    info: Microsoft.Hosting.Lifetime[0]

Open your browser and go to the first address + "/swagger" (in this case `localhost:7079/swagger`) or second address + "/swagger" (`localhost:5079/swagger`). (notice, that in your case your ports can be different).

Instead of using swagger you can use any software you like to send requests to adresses above (Postman, for example)

When you finish, go back to the console and press `Ctrl+C` to stop the application.

  

## How to use:

You can test web api with swagger, but also you can send next requests:

 - #### Attendance/:
   
	- `attendance/increase` - addes one visitor to visitors ammount
   
   - `attendance/decrease` - removes one visitor from visitors ammount
   
	-  `attendance/get` - returns current ammount of visitors

  

- #### Book/:

	- `/book/new` - adds new book to database

		Content:

		    {
		    "title": "string",
		    "author": "string",
		    "description": "string"
		    }

	-	`/book/delete/{id}` - removes book with id

	- `/bood/getId/{title}/{author}` - returns id of books which has requested title and author

	- `/book/all` - returns list of all books

	- `/book/move/{id}` - moves book which has reauested id to sended position

		Content:

		    "position":"string"

	- `/book/setStatus/{id}` - changes BookState of book with requested id

		Content:

		    bookState = 0/1/2

	- `/book/get/{id}` - returns book by requested id

  

- #### /Issue:

	- `/issue/new` - creates new issue with book and patron

		Content:

		    {
		    "bookId": "string", 
		    "patronId": "string"
		    }

	- `/issue/all` - returns list of all issues

	- `/issue/close/{id}` - closes issue with requestes id

  

- #### /Patron:

	- `/patron/new` - creates new patron

		Content:

		    {
		    "name": "string",
		    "surname": "string",
		    "email": "string",
		    "password": "string"
		    }

	- `/patron/delete` - removes patron with requested parameters

		Content:

		    {
		    "name": "string",
		    "surname": "string",
		    "email": "string",
		    "password": "string"
		    }

	- `/patron/all` - returns list of all patrons

	- `/patron/{id}` - returns patron with requested id

	- `/patron/{name}/{surname}/{email}` - returns user with requested parameters

	- `/patron/delete/{id}` - removes patron with requested id

## Used technologies

-   ASP.NET Core
-   Entity Framework
-   Serilog
-   XUnit
-   FluentAssertions
-   Moq
  

 - Razor Pages

   

 - HTML + CSS + JQuerry

   

 - XUnit

   
   

 - FluentAssertions

  
