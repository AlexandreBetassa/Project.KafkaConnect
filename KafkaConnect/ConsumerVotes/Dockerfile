#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Project.WebApiVotes/Project.WebApiVotes.csproj", "Project.WebApiVotes/"]
COPY ["Project.Infra/Project.Infra.csproj", "Project.Infra/"]
COPY ["Project.Kafka/Project.Kafka.csproj", "Project.Kafka/"]
COPY ["Project.Repository/Project.Repository.csproj", "Project.Repository/"]
COPY ["Project.Services/Project.Services.csproj", "Project.Services/"]
RUN dotnet restore "Project.WebApiVotes/Project.WebApiVotes.csproj"
COPY . .
WORKDIR "/src/Project.WebApiVotes"
RUN dotnet build "Project.WebApiVotes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project.WebApiVotes.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.WebApiVotes.dll"]