# AzureDocumentIntelligence

AzureDocumentIntelligence is a .NET 9.0 web API project that leverages Azure's AI Form Recognizer for document analysis. It provides endpoints to analyze documents from bytes, URLs, or uploaded files.

## Features
- Analyze documents using Azure Form Recognizer.
- Middleware for signature validation to secure API requests.
- Configurable settings for Azure and security keys.

## Project Structure
- **Controllers**: Contains API controllers for handling requests.
- **Middlewares**: Includes middleware for request validation.
- **Models**: Defines data models used across the application.
- **Services**: Contains service classes for business logic.
- **Tests**: Placeholder for unit tests.

## Configuration
1. Copy `appsettings.json.template` to `appsettings.json`.
2. Update the following placeholders with your Azure and security details:
   - `AzureDocumentIntelligence:Endpoint`
   - `AzureDocumentIntelligence:ApiKey`
   - `Security:SecretKey`

## Running the Application
1. Build the project using Visual Studio or the .NET CLI.
2. Run the application using the `dotnet run` command.
3. Access the API at `http://localhost:5156` or `https://localhost:7162`.

## Docker Support
The project includes a Dockerfile for containerization. To build and run the Docker container:
1. Build the image: `docker build -t azure-document-intelligence .`
2. Run the container: `docker run -p 8080:8080 -p 8081:8081 azure-document-intelligence`

## Endpoints
- `POST /analyze-bytes/{modelId}`: Analyze document from byte array.
- `POST /analyze-url/{modelId}`: Analyze document from a URL.
- `POST /analyze-file/{modelId}`: Analyze document from an uploaded file.

## Dependencies
- Azure.AI.FormRecognizer
- Microsoft.AspNetCore.OpenApi
- Moq (for testing)
- Xunit (for testing)

## License
This project is licensed under the MIT License.