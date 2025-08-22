# EducationPortal

## Migrations

```bash
dotnet ef migrations add MigrationName --project EducationPortal.Data -o Migrations --startup-project EducationPortal.Web
dotnet ef database update --project EducationPortal.Data --startup-project EducationPortal.Web
```