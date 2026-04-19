# AI Online Exam Cheating Risk Detector

A web-based system that monitors student behaviour during online exams,
calculates a cheating risk score, and alerts administrators when suspicious
activity is detected.

---

## Tech Stack

| Layer    | Technology              |
|----------|-------------------------|
| Frontend | Angular (TypeScript, SCSS) |
| Backend  | ASP.NET Core (C#)       |
| Database | MS SQL Server           |
| Auth     | JWT (JSON Web Tokens)   |

---

## Project Structure
exam-cheating-detector/
├── api/
│   ├── src/ExamCheatingDetector.API/   # ASP.NET Core backend
│   │   ├── Controllers/
│   │   ├── Services/
│   │   ├── Repositories/
│   │   ├── Models/
│   │   └── DTOs/
│   └── tests/                          # Backend unit & integration tests
├── ui/
│   ├── src/exam-cheating-detector-ui/  # Angular frontend
│   │   └── src/app/
│   │       ├── pages/
│   │       ├── services/
│   │       ├── guards/
│   │       └── models/
│   └── tests/                          # Frontend tests
├── docs/
│   └── database/schema.sql             # Database schema
├── build/                              # Build scripts (optional)
├── tools/                              # Utility scripts (optional)
└── README.md


## Mentor

Project Mentor — Alvin Mohan

