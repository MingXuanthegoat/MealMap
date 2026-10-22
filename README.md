# MealMap

MealMap is a meal-planning backend. Users sign up with a budget tier, browse and manage recipes (with ingredients and estimated costs), and plan their meals by day and meal slot. The user model also stores a postcode and coordinates, intended for location-aware features later on.

This repository currently contains the API only (`MealMap.Api`). There is no frontend yet.

## Tech stack

- **ASP.NET Core** on **.NET 10** (controller-based Web API)
- **Entity Framework Core** with **SQLite** (code-first migrations)
- **JWT bearer authentication**, with passwords hashed using **BCrypt**
- **OpenAPI** + **Scalar** for interactive API docs in development

## Project structure

```
MealMap.Api/
├── Controller/        API endpoints (AuthController, RecipesController)
├── Data/              MealMapDbContext (EF Core)
├── DTOs/              Request contracts (RegisterDto, LoginDto)
├── Migrations/        EF Core migrations (InitialCreate)
├── Models/            User, Recipe, Ingredient, MealPlanEntry, BudgetTier
├── Services/          TokenService (issues JWTs)
├── Program.cs         App startup, DI, auth and middleware setup
└── appsettings.json   Connection string and JWT settings
```

## Data model

| Entity | Key fields |
| --- | --- |
| **User** | Username, PasswordHash, BudgetTier, Postcode, Latitude, Longitude |
| **Recipe** | Title, Instructions, PrepTimeMinutes, Tags, Ingredients |
| **Ingredient** | Name, Quantity, Unit, EstimatedCostPerUnit, belongs to a Recipe |
| **MealPlanEntry** | Date, Slot (Breakfast / Lunch / Dinner), links a User to a Recipe |

`BudgetTier` is one of `Budget`, `Standard` or `Premium`.

## API endpoints

### Auth (public)

| Method | Route | Description |
| --- | --- | --- |
| POST | `/api/auth/register` | Create an account. Body: `username`, `password`, `budgetTier`. Returns `{ token }`. |
| POST | `/api/auth/login` | Log in. Body: `username`, `password`. Returns `{ token }`. |

### Recipes (requires `Authorization: Bearer <token>`)

| Method | Route | Description |
| --- | --- | --- |
| GET | `/api/recipes` | List all recipes with their ingredients |
| GET | `/api/recipes/{id}` | Get one recipe with its ingredients |
| POST | `/api/recipes` | Create a recipe |
| PUT | `/api/recipes/{id}` | Update a recipe |
| DELETE | `/api/recipes/{id}` | Delete a recipe |

## Getting started

Prerequisites: the [.NET 10 SDK](https://dotnet.microsoft.com/download) and the EF Core CLI (`dotnet tool install --global dotnet-ef`).

```bash
cd MealMap.Api
dotnet restore
dotnet ef database update   # creates/updates mealmap.db
dotnet run
```

The API runs at `http://localhost:5193` (or `https://localhost:7287` with the `https` profile). In development, the interactive API docs are at:

- Scalar UI: `http://localhost:5193/scalar`
- OpenAPI JSON: `http://localhost:5193/openapi/v1.json`

To call protected endpoints, register or log in first, then send the returned token as `Authorization: Bearer <token>`. Tokens expire after 60 minutes (configurable via `Jwt:ExpiryMinutes`).

## Progress

### Done

- [x] Domain models: User, Recipe, Ingredient, MealPlanEntry, BudgetTier
- [x] EF Core DbContext with SQLite and an initial code-first migration
- [x] Recipes CRUD controller
- [x] User registration and login with BCrypt password hashing
- [x] JWT issuing (`TokenService`) and validation middleware
- [x] Recipes endpoints protected with `[Authorize]`
- [x] DTOs for auth requests, separating the API contract from the data models
- [x] OpenAPI + Scalar docs in development

### Not started / to do

- [ ] Meal plan endpoints: the `MealPlanEntry` model and table exist, but there is no controller to create, view or remove plan entries yet
- [ ] DTOs for recipes (the controller currently accepts and returns the EF entities directly)
- [ ] Recipe ownership: recipes are global and not linked to the user who created them
- [ ] Use budget tier and ingredient costs (e.g. estimate meal or weekly plan cost, filter recipes by budget)
- [ ] Location features using the user's postcode and coordinates (fields exist but aren't set or used anywhere yet)
- [ ] Input validation (e.g. password rules, required recipe fields)
- [ ] Move the JWT signing key out of `appsettings.json` into user secrets or environment variables (the current key is a placeholder)
- [ ] Add a `.gitignore` so `bin/`, `obj/` and `mealmap.db` stop being committed
- [ ] Tests
- [ ] Frontend client
