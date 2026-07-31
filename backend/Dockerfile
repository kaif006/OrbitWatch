FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["backend.csproj", "./"]
RUN dotnet restore "./backend.csproj"

# Copy the rest of the code and build
COPY . .
RUN dotnet publish "backend.csproj" -c Release -o /app/publish

# Use the lightweight ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "backend.dll"]
