# Distributed Task Queue (DTQ) Monorepo

A distributed task processing and queue orchestration platform featuring a .NET Clean Architecture backend and an Angular 19+ state-driven frontend.

---

## 📁 Repository Structure

```text
Distributed-Task-Queue-monorepo/
├── docs/                             # Architecture diagrams, ADRs, and documentation
├── src/
│   ├── frontend/                     # Angular Single Page Application
│   │   ├── src/
│   │   │   ├── app/                  # Application components & routing
│   │   │   │   ├── not-found/        # 404 handler component
│   │   │   │   ├── queue/            # Queue dashboard view
│   │   │   │   ├── sidebar/          # Navigation sidebar component
│   │   │   │   ├── task/             # Individual task inspection component
│   │   │   │   ├── tasks-list/       # Task management and listing component
│   │   │   │   ├── app.component.ts  # Root component
│   │   │   │   ├── app.config.ts     # Application & NgRx store config
│   │   │   │   └── app.routes.ts     # Route definitions
│   │   │   └── state/                # NgRx Store (Actions, Reducers, Selectors, Effects, Facades)
│   │   ├── angular.json
│   │   └── package.json
│   │
│   └── server/                       # .NET Backend Solution
│       ├── DTQ.sln
│       ├── DTQ.Domain/               # Domain entities, Task State Machine, Interfaces
│       ├── DTQ.Application/          # Use cases, MediatR Commands/Queries, Orchestrator Services
│       ├── DTQ.Infrastructure/       # EF Core, Persistence, Message Brokers, External Services
│       └── DTQ.Api/                  # ASP.NET Core Web API, Controllers, Minimal Endpoints
├── .gitignore                        # Global ignore rules (.NET, Node, OS, IDE)
└── README.md
```

---

## 🚀 Getting Started

### Prerequisites
- **Node.js**: `v20+` & `npm`
- **.NET SDK**: `8.0+`

### Frontend Setup
```bash
cd src/frontend
npm install
npm run start
```
The client will be served at `http://localhost:4200/`.

### Backend Setup
```bash
cd src/server
dotnet restore
dotnet build
```