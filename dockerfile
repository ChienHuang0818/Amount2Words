# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copy csproj and restore
COPY NumberToWordsApp.csproj ./
RUN dotnet restore NumberToWordsApp.csproj

# copy other files and publish
COPY . ./
RUN dotnet publish NumberToWordsApp.csproj -c Release -o /app

# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "NumberToWordsApp.dll"]

	
