FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN sed -i'.bak' 's/$/ contrib/' /etc/apt/sources.list
RUN apt-get update; apt-get install -y ttf-mscorefonts-installer fontconfig
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WCS.WebApi/WCS.WebApi.csproj", "WCS.WebApi/"]
COPY ["WCS.ClassDTO/WCS.ClassDTO.csproj", "WCS.ClassDTO/"]
COPY ["WCS.Common/WCS.Common.csproj", "WCS.Common/"]
COPY ["WCS.ClassDomain/WCS.ClassDomain.csproj", "WCS.ClassDomain/"]
COPY ["WCS.DataLayer/WCS.DataLayer.csproj", "WCS.DataLayer/"]
COPY ["WCS.FluentAPI/WCS.FluentAPI.csproj", "WCS.FluentAPI/"]
COPY ["WCS.Service/WCS.Service.csproj", "WCS.Service/"]
COPY ["WCS.InterfaceService/WCS.InterfaceService.csproj", "WCS.InterfaceService/"]
RUN dotnet restore "WCS.WebApi/WCS.WebApi.csproj"
COPY . .
WORKDIR "/src/WCS.WebApi"
RUN dotnet build "WCS.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WCS.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WCS.WebApi.dll"]
