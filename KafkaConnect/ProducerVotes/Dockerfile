#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Project.WebApiProducerVotes/Project.WebApiProducerVotes.csproj", "Project.WebApiProducerVotes/"]
COPY ["Project.Infra/Project.Infra.csproj", "Project.Infra/"]
COPY ["Project.Domain/Project.Domain.csproj", "Project.Domain/"]
RUN dotnet restore "Project.WebApiProducerVotes/Project.WebApiProducerVotes.csproj"
COPY . .
WORKDIR "/src/Project.WebApiProducerVotes"
RUN dotnet build "Project.WebApiProducerVotes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project.WebApiProducerVotes.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.WebApiProducerVotes.dll"]