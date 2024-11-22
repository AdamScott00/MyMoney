FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyMoney/MyMoney.csproj", "./"]
RUN dotnet restore "MyMoney.csproj"
COPY MyMoney/. .
WORKDIR "/src/."
RUN dotnet build "MyMoney.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyMoney.csproj" -c Release -o /app/publish

FROM build AS migrations
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet MyMoney.dll"]
