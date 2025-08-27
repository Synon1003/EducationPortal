# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY educationportal.sln ./

# Copy project files
COPY EducationPortal.Web/EducationPortal.Web.csproj EducationPortal.Web/
COPY EducationPortal.Application/EducationPortal.Application.csproj EducationPortal.Application/
COPY EducationPortal.Data/EducationPortal.Data.csproj EducationPortal.Data/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source
COPY . .

# --- Node.js setup for Tailwind build ---
RUN apt-get update && \
    apt-get install -y curl gnupg && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm --version && node --version

# Install npm deps and build CSS inside Web project
WORKDIR /src/EducationPortal.Web
COPY EducationPortal.Web/package*.json ./
RUN npm install
RUN npm run css:build

# Publish .NET Web project (now CSS is already built)
RUN dotnet publish -c Release -o /published /p:UseAppHost=false


# Runtime stage (lighter)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080 8081

# Copy published files (including built CSS)
COPY --from=build /published ./

ENTRYPOINT ["dotnet", "EducationPortal.Web.dll"]
