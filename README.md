# EducationPortal

Running the application on https://localhost:7133 | http://localhost:5012
```bash
cd EducationPortal.Web
dotnet run
```

### Docker
Db-only:
```bash
docker compose up educationportalsqldb
```
Both containers: this will run the application on http://localhost:8080
```bash
docker compose up
```

### Migrations

```bash
dotnet ef migrations add MigrationName --project EducationPortal.Data -o Migrations --startup-project EducationPortal.Web

dotnet ef database update --project EducationPortal.Data --startup-project EducationPortal.Web
```

### Testing
Unit tests:
```bash
cd EducationPortal.Tests
dotnet test
dotnet test --filter "FullyQualifiedName~CourseServiceTests"
```

### Tailwind DaisyUI
The application should be responsive and mobile friendly due to tailwind  
Installed as development dependency
```bash
cd EducationPortal.Web
npm init --yes
npm install -D tailwindcss @tailwindcss/cli
npm install -D tailwindcss@latest postcss autoprefixer
npm install -D daisyui
```

Used to build css before dotnet build
```bash
npm run css:build
```