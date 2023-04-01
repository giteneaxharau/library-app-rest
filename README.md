Library App REST API
This is a REST API for a library app made in .NET 7. The API allows users to perform CRUD operations on books, authors, and publishers.

Installation
To install the API, clone the repository and open the solution in Visual Studio 2022. Build the solution to restore the packages.

To run the API, set the "LibraryApp.Api" project as the startup project and press F5. The API will be hosted on https://localhost:5001 by default.

Usage
The API provides the following endpoints:

GET /api/books: Get a list of all books.
GET /api/books/{id}: Get a book by ID.
POST /api/books: Add a new book.
PUT /api/books/{id}: Update an existing book.
DELETE /api/books/{id}: Delete a book by ID.
GET /api/authors: Get a list of all authors.
GET /api/authors/{id}: Get an author by ID.
POST /api/authors: Add a new author.
PUT /api/authors/{id}: Update an existing author.
DELETE /api/authors/{id}: Delete an author by ID.
GET /api/publishers: Get a list of all publishers.
GET /api/publishers/{id}: Get a publisher by ID.
POST /api/publishers: Add a new publisher.
PUT /api/publishers/{id}: Update an existing publisher.
DELETE /api/publishers/{id}: Delete a publisher by ID.
All endpoints require authentication. Users must provide a valid JWT token in the Authorization header. To obtain a JWT token, send a POST request to /api/auth/token with a JSON body containing a valid username and password.

Contributing
Contributions are welcome! Please submit a pull request if you would like to contribute to this project.

License
This project is licensed under the MIT License. See the LICENSE file for more information.
