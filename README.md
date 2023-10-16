# PokemonReviewApp

Welcome to PokemonReviewApp! This project is an ASP.NET Core Web API application developed as part of a tutorial series by Teddy Smith. It covers a wide range of ASP.NET Core Web API concepts and techniques.

## Table of Contents
- [Project Overview](#project-overview)
- [Key Features](#key-features)
- [Lessons Learned](#lessons-learned)
- [Getting Started](#getting-started)
- [Contributing](#contributing)

## Project Overview
<a name="project-overview"></a>
PokemonReviewApp is a practical demonstration of building a functional ASP.NET Core Web API, featuring:

1. **Models and Relationships:**
   - Defining data models for Pokemon, Owners, Categories, and Reviews.
   - Establishing relationships between these entities.

2. **Entity Framework Core:**
   - Integrating Entity Framework Core for efficient database operations.

3. **Controllers and Actions:**
   - Creating controllers for CRUD (Create, Read, Update, Delete) operations.
   - Implementing actions for managing Pokemon, Owners, and Categories.

4. **Dependency Injection and Repository Pattern:**
   - Employing dependency injection for modularity and testability.
   - Utilizing the repository pattern for data access.

5. **Searching and Filtering:**
   - Implementing functionality for searching and filtering Pokemon, Owners, and Categories.

6. **Versioning:**
   - Incorporating versioning to manage API changes over time.

7. **Documentation:**
   - Documenting API endpoints for enhanced developer experience.
   - Leveraging Swagger UI for interactive API documentation.

8. **Error Handling:**
   - Handling errors and providing appropriate HTTP status codes.

## Key Features
<a name="key-features"></a>
- **Pokemon Management:**
   - Create, read, update, and delete Pokemon.
   - Associate Pokemon with Owners and Categories.
   - Post and manage reviews for each Pokemon.

- **Owner Management:**
   - Create, read, update, and delete Owners.
   - Associate Owners with Pokemon.

- **Category Management:**
   - Create, read, update, and delete Categories.
   - Associate Categories with Pokemon.

- **Interactive API Documentation:**
   - Explore and test API endpoints using Swagger UI.

## Lessons Learned
<a name="lessons-learned"></a>
During the development of PokemonReviewApp, we've gained valuable insights into:

- Building a structured ASP.NET Core Web API application.
- Implementing data models and relationships.
- Leveraging Entity Framework Core for efficient database operations.
- Designing controllers and actions for CRUD operations.
- Promoting modularity and testability through dependency injection and the repository pattern.
- Enhancing the developer experience with interactive API documentation using Swagger UI.
- Adding searching and filtering functionality for improved data retrieval.
- Managing API versioning to accommodate changes over time.
- Documenting API endpoints for better clarity and understanding.

## Getting Started
<a name="getting-started"></a>
To run this project locally, follow these steps:

1. Clone the repository: `git clone https://github.com/your-username/PokemonReviewApp.git`
2. Navigate to the project directory: `cd PokemonReviewApp`
3. Restore NuGet packages: `dotnet restore`
4. Run the application: `dotnet run`

## Contributing
<a name="contributing"></a>
Contributions to this project are welcome! If you find any issues or have ideas for improvements, please open an issue or create a pull request.
