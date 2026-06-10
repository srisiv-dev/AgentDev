# .NET API Project Structure

All .NET API projects must follow this standardized folder structure:

## Required Projects

### `<projectName>.API`
- **Purpose**: Controllers
- **Responsibility**: Handle HTTP requests and responses
- **Contains**: API controllers, route definitions

### `<projectName>.Core`
- **Purpose**: Business Logic Layer
- **Responsibility**: Implement all business logic with services and providers
- **Contains**: 
  - Services (business logic implementation)
  - Providers (external service/API calls)
  - Domain models
  - Interfaces

### `<projectName>.Helper`
- **Purpose**: Helper Classes & Utilities
- **Responsibility**: Provide reusable helper functionality
- **Contains**: 
  - Dapper utilities/helpers
  - RestSharp handlers
  - Common helper classes

### `<projectName>.Extension`
- **Purpose**: Extension Methods
- **Responsibility**: Extend existing classes with additional functionality
- **Contains**: Extension method classes for built-in and custom types

### `<projectName>.Utility`
- **Purpose**: Utility Classes
- **Responsibility**: Provide utility functions and common operations
- **Contains**: Utility classes for configuration, logging, constants, etc.

## Example Directory Layout

MyProject/
├── MyProject.API/
│ ├── Controllers/
│ └── appsettings.json
├── MyProject.Core/
│ ├── Services/
│ ├── Providers/
│ └── Models/
├── MyProject.Helper/
│ ├── Dapper/
│ └── RestSharp/
├── MyProject.Extension/
│ └── *.cs (extension methods)
└── MyProject.Utility/
└── *.cs (utility classes)


## Best Practices

- Dependencies flow downward: API → Core → Helper/Extension/Utility
- Core projects should have no dependency on API projects
- Avoid circular dependencies
- Keep each project focused on its responsibility