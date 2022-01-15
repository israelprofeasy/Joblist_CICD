#build phase
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o publish

#run phase
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://*:$PORT
ENTRYPOINT ["dotnet", "JobListingApp.dll"]

#CMD ASPNETCORE_URLS=http://*:$PORT dotnet JobListingApp.dll

