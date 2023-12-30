# Use the .NET Core SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the entire project to the container
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Create a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory in the container
WORKDIR /app

# Copy the published output from the build stage to the runtime image
COPY --from=build /app/out .

# Expose the port that the app is listening on (change if your app uses a different port)
EXPOSE 80

# Set the ASP.NET Core environment variable to 'Development'
# ENV ASPNETCORE_ENVIRONMENT=Development

# Command to run the application when the container starts
ENTRYPOINT ["dotnet", ".net-qlhs-server.dll"]
