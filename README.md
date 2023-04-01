# Library App REST API

This is a REST API for a library app made in .NET 7. The API allows users to perform CRUD operations on books, authors, and publishers.

## Installation

To install the API, clone the repository and open the solution in Visual Studio 2022. Build the solution to restore the packages.

To run the API, set the "LibraryApp.Api" project as the startup project and press F5. The API will be hosted on `https://localhost:5001` by default.

## Usage

The API provides the following endpoints:

- `GET /api/v1/books`: Get a list of all books.
- `GET /api/v1/books/{id}`: Get a book by ID.
- `POST /api/v1/books`: Add a new book.
- `PUT /api/v1/books/{id}`: Update an existing book.
- `DELETE /api/v1/books/{id}`: Delete a book by ID.
- `POST /api/v1/books/uploadimage`: Upload an image in 'multipart/form-data'

- `GET /api/v1/authors`: Get a list of all authors.
- `GET /api/v1/authors/{id}`: Get an author by ID.
- `POST /api/v1/authors`: Add a new author.
- `PUT /api/v1/authors/{id}`: Update an existing author.
- `DELETE /api/v1/authors/{id}`: Delete an author by ID.
- `GET /api/v1/authors/users`: Get all the users that have a role of Author

- `GET /api/v1/categories`: Get a list of all categories.
- `GET /api/v1/categories/{id}`: Get a publisher by ID.
- `POST /api/v1/categories`: Add a new publisher.
- `PUT /api/v1/categories/{id}`: Update an existing publisher.
- `DELETE /api/v1/categories/{id}`: Delete a publisher by ID.

PUT & POST endpoints require authentication. Users must provide a valid JWT token in the `Authorization` header. To obtain a JWT token, send a POST request to `/api/v1/userauth/login` with a JSON body containing a valid username and password.

## Contributing

Contributions are welcome! Please submit a pull request if you would like to contribute to this project.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.
