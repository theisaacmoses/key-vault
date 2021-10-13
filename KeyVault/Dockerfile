FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["KeyVault/KeyVault.csproj", "KeyVault/"]
RUN dotnet restore "KeyVault/KeyVault.csproj"
COPY . .
WORKDIR "/src/KeyVault"
RUN dotnet build "KeyVault.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "KeyVault.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KeyVault.dll"]