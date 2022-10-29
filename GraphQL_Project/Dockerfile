FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GraphQL_Project/GraphQL_Project.csproj", "GraphQL_Project/"]
RUN dotnet restore "GraphQL_Project/GraphQL_Project.csproj"
COPY . .
WORKDIR "/src/GraphQL_Project"
RUN dotnet build "GraphQL_Project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GraphQL_Project.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQL_Project.dll"]