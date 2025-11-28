# Talabat API (ASP.NET Core Web API)

**One-line Description:**
A complete food-ordering RESTful API built using **Clean Architecture**, featuring Generic Repositories, Generic Services, Authentication/Authorization, and secure request handling.

---

## 3-Point Professional Description

1. **Scalable Clean Architecture:** Implemented using a clean, modular structure with **Generic Repositories** and **Generic Services**, ensuring high maintainability, reusability, and separation of concerns.
2. **Core Ordering Features:** Includes **Product Module**, **Cart (In-Memory DB)**, and **Order Processing** with state management, validation, and calculation logic.
3. **Advanced Security Layer:** Full **JWT Authentication & Authorization**, custom **Security Module**, and **Custom Middlewares** for error handling, logging, and request validation.

---

## Table of Contents

* [Features](#features)
* [Architecture Overview](#architecture-overview)
* [Tech Stack](#tech-stack)
* [Getting Started](#getting-started)

  * [Prerequisites](#prerequisites)
  * [Environment Variables](#environment-variables)
  * [Run Locally](#run-locally)
* [API Endpoints (Summary)](#api-endpoints-summary)
* [Examples](#examples)
* [Testing](#testing)
* [Project Structure (High-Level)](#project-structure-high-level)
* [Contributing](#contributing)
* [License](#license)
* [Contact](#contact)

---

## Features

* Clean Architecture (API / Application / Domain / Infrastructure)
* Generic Repository & Generic Service layers
* Product CRUD operations
* Cart module using **In-Memory Database** (ideal for fast dev & testing)
* Order processing and validation flows
* JWT Authentication & Authorization
* Custom Middlewares (Error Handler, Logger, Request Validator)
* DTOs + AutoMapper
* RESTful design following best practices

---

## Architecture Overview

This project follows a clean, modular, and scalable architecture, organized into three main layers: **API**, **Core**, and **Infrastructure**.

### **API Layer** (`/API`)

* **.apis** → Extensions, Middlewares, Program.cs
* **.controllers** → Controllers, Error handling controllers

### **Core Layer** (`/Core`)

* **.application** → Exceptions, Mapping Profiles, Services, Dependency Injection setup
* **.application.abstraction** → Common interfaces, DTOs, Service contracts
* **.domain** → Common logic, Contracts, Entities, Specification Pattern implementation

### **Infrastructure Layer** (`/Infrastructure`)

* **.infrastructure** → Basket Repository, Dependency Injection extensions
* **.infrastructure.persistence** → Database Context, Identity configuration, EF Core Repositories, Unit of Work, Persistence-level DI

### Additional Implementations

* **Pagination Support:** Implemented standardized pagination across listing endpoints.
* **Specification Pattern:** Fully applied to handle filtering, sorting, pagination, and dynamic query building following clean separation of concerns.

---

## Tech Stack

* **Language:** C# (.NET 9)
* **Framework:** ASP.NET Core Web API
* **Database:** SQL Server + InMemory provider for Cart
* **ORM:** Entity Framework Core
* **Authentication:** JWT Bearer
* **Mapping:** AutoMapper
* **Patterns:** Clean Architecture, Repository Pattern

---

## Getting Started

### Prerequisites

* .NET SDK 9
* SQL Server / LocalDB
* Git

### Environment Variables

Use `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
  "StoreContext": "Server=.;Database=StoreContext;Trusted_Connection=True;TrustServerCertificate=True;",
  "IdentityContext": "Server=.;Database=Talabat.Apis.Identity;Trusted_Connection=True;TrustServerCertificate=True;",
  "Redis": "localhost:6379"
},
"Urls": {
  "ApiBaseUrl": "https://localhost:7101"
},
"RedisSettings": {
  "TimeToLiveInDays": "30"
},
"JWT": {
  "Key": "Your_Secret_Key_Should_Be_At_Least_32_Characters",
  "Issuer": "TalabatIdentity",
  "Audience": "TalabatUsers",
  "DurationInMinutes": 10
}
}
```

### Run Locally

```bash
git clone <REPO_URL>
cd talabat-api

dotnet restore
dotnet build

# Apply migrations (if used)
dotnet ef database update --project src/Infrastructure --startup-project src/API

# Run application
dotnet run --project src/API
```

---

## API Endpoints (Summary)

* `POST /api/auth/register` — Register new user
* `POST /api/auth/login` — Generate JWT token
* `GET /api/products` — Get all products
* `GET /api/products/{id}` — Get single product
* `POST /api/cart` — Add/update cart items
* `GET /api/cart` — View cart contents
* `POST /api/orders` — Create an order
* `GET /api/orders/{id}` — View order details

---

## Examples

### Login Example

```bash
curl -X POST https://localhost:5001/api/auth/login \
 -H "Content-Type: application/json" \
 -d '{"email":"user@example.com","password":"P@ssw0rd"}'
```

### Add Item to Cart

```bash
curl -X POST https://localhost:5001/api/cart \
 -H "Content-Type: application/json" \
 -d '{"productId": 12, "quantity": 2}'
```


## Project Structure (High-Level)

```
/src
  /API                # Controllers, DTOs, Middlewares
  /Application        # Services, Interfaces, Validation
  /Domain             # Entities, Enums, Domain Logic
  /Infrastructure     # EF Core, Repositories
/tests                # Test projects
```

---

## Contributing

Feel free to open Issues or submit Pull Requests.

---

## License

MIT License

---

## Contact

* **Name:** Your Name
* **Email:** [noura.ahmed7258@gmail.com](mailto:noura.ahmed7258@gmail.com)
* **LinkedIn:** https://www.linkedin.com/in/noura-ahmed-36779b304

