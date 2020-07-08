FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Boticario.WebApi/Boticario.WebApi.csproj", "Boticario.WebApi/"]
RUN dotnet restore "Boticario.WebApi/Boticario.WebApi.csproj"
COPY . .
WORKDIR "/src/Boticario.WebApi"
RUN dotnet build "Boticario.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Boticario.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Boticario.WebApi.dll"]
