FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80

RUN apk add --no-cache icu-libs

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["AdaptiveWebInterfaces-WebAPI.csproj", "./"]
RUN dotnet restore "./AdaptiveWebInterfaces-WebAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AdaptiveWebInterfaces-WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdaptiveWebInterfaces-WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdaptiveWebInterfaces-WebAPI.dll"]
