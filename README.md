# Project Setup

This document outlines the prerequisites and setup instructions for running this project.

## Prerequisites

* **.NET 9:** Ensure you have the .NET 9 SDK installed. You can download it from the official Microsoft website.
* **Angular 14+:** This project requires Angular version 14 or higher. Install Node.js and npm (Node Package Manager) from [nodejs.org](https://nodejs.org/). Then, install the Angular CLI globally:

    ```bash
    npm install -g @angular/cli
    ```

## Setup

1.  **Clone the Repository:** Clone the project repository to your local machine.

2.  **Navigate to the Angular Project:** Open a terminal or command prompt and navigate to the directory containing your Angular project (where the `angular.json` file is located).

3.  **Run `ng serve`:** Execute the following command to start the Angular development server:

    ```bash
    ng serve
    ```

    This will compile your Angular application and start a development server, typically accessible at `http://localhost:4200/`.

4.  **Run the .NET Application:** Open another terminal or command prompt, navigate to the directory containing your .NET project's `.csproj` file, and run the .NET application.

    ```bash
    dotnet run
    ```

    The .NET application will start, and you can access it in your browser.

**Important Notes:**

* If you need to change the port that angular runs on, you can use the command `ng serve --port <port number>`.
* If your .net application needs to use a different port, that can be configured in the .net applications launchSettings.json file.
* Ensure that there are no port conflicts between the Angular development server and your .NET application.
* For production deployments, remember to build your Angular application using `ng build --prod` (or `ng build --configuration production`) and serve the static files from your .NET application or a dedicated web server.

Airport Search System

Project Overview
This project is a search system for airport and airline-related data, built as part of an API Developer Technical Evaluation. The system allows users to search for airports, refine results using filters, sort results, view search history, and ensure security with token-based authentication.

Key Features
✅ Search Functionality

Users can enter queries to retrieve relevant airport/airline data.
Supports filtering based on city, country, and IATA code.
Provides sorting options for better data organization.
Implements search history suggestions (like Google/Amazon).
✅ Error Handling

Displays clear error messages if the search fails.
Logs errors for troubleshooting.
✅ Documentation

Provides API documentation for developers.
✅ Database Integration

Stores and retrieves search history.
✅ Middleware

Implements request logging and security features.
✅ Security

Uses JWT-based authentication for secure API access.
Handles token renewal and logout redirection.
✅ Performance & Scalability

Uses Azure & Elasticsearch for fast and scalable search capabilities.
Technology Stack
Backend
.NET Core 9 – API development
Azure – Cloud deployment
Elasticsearch – Full-text search engine
Entity Framework Core – ORM for database operations
Frontend
Angular – UI development
NgRx – State management
Tailwind CSS – Styling
Database
SQL Server – Storing airport & airline data
Project Assumptions
The searchable dataset consists of airport details (name, city, country, IATA, ICAO, latitude, longitude, etc.).
Search results come from Azure Search, backed by a SQL database.
JWT authentication is used for API security.
Azure is used for hosting and SQL database storage.


Local Setup Instructions

1. Clone the Repository
bash
Copy
Edit
git clone https://github.com/your-org/airport-search.git
cd airport-search
2. Backend Setup
Prerequisites
Install .NET SDK 9

Authentication Flow

User logs in → JWT token is generated.
Token is stored in NgRx store and passed in API requests.
If token expires, refresh token is used to generate a new token.
If refresh fails, user is redirected to the login page.
API Endpoints
Authentication
POST /api/auth/login → Login & generate JWT token
Search
GET /api/airports?query={searchTerm} → Search for airports
GET /api/airports/searchhistory → Get search history
Filters & Sorting
GET /api/airports?sort=city → Sort results by city
GET /api/airports?filter=country=US → Filter results by country
Deployment
This project is deployed on Azure using:

Azure App Services (Backend API)
Azure SQL Database
Azure Search
