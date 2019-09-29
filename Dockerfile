FROM mcr.microsoft.com/dotnet/core/runtime:3.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["TVlPQi5Db2Rl/TVlPQi5Db2Rl.csproj", "TVlPQi5Db2Rl/"]
RUN dotnet restore "TVlPQi5Db2Rl/TVlPQi5Db2Rl.csproj"
COPY . .
WORKDIR "/src/TVlPQi5Db2Rl"
RUN dotnet build "TVlPQi5Db2Rl.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TVlPQi5Db2Rl.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TVlPQi5Db2Rl.dll"]
