FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV PATH="$PATH:/root/.dotnet/tools"

COPY ["Evacuation.sln", "./"]
COPY ["Evacuation.Api/Evacuation.Api.csproj", "Evacuation.Api/"]
COPY ["Evacuation.Application/Evacuation.Application.csproj", "Evacuation.Application/"]
COPY ["Evacuation.Domain/Evacuation.Domain.csproj", "Evacuation.Domain/"]
COPY ["Evacuation.Infrastructure/Evacuation.Infrastructure.csproj", "Evacuation.Infrastructure/"]

RUN dotnet restore "Evacuation.Api/Evacuation.Api.csproj"

COPY . .

WORKDIR "/src/Evacuation.Api"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

COPY ./entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

ENTRYPOINT ["/entrypoint.sh"]
