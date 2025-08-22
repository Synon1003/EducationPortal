# EducationPortal

Running the application
```bash
cd EducationPortal.Web
dotnet run
```


### Migrations

```bash
dotnet ef migrations add MigrationName --project EducationPortal.Data -o Migrations --startup-project EducationPortal.Web

dotnet ef database update --project EducationPortal.Data --startup-project EducationPortal.Web
```


### Tailwind DaisyUI
Install as development dependency
```bash
npm init --yes
npm install -D tailwindcss @tailwindcss/cli
npm install -D tailwindcss@latest postcss autoprefixer
npm install -D daisyui
```

Use before dotnet build
```bash
npm run css:build
```