# use .net sdk to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy the csproj file and restore the dependencies
COPY *.csproj ./
RUN dotnet restore

# copy the rest of the files and publish the project
COPY . ./
RUN dotnet publish -c Release -o /app

# use .net runtime to run the project
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Amount2Words.dll"]
