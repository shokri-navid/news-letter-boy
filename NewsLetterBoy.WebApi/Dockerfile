#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NewsLetterBoy.WebApi/NewsLetterBoy.WebApi.csproj", "NewsLetterBoy.WebApi/"]
COPY ["NewsLetterBoy.Service/NewsLetterBoy.Service.csproj", "NewsLetterBoy.Service/"]
COPY ["NewsLetterBoy.Model/NewsLetterBoy.Model.csproj", "NewsLetterBoy.Model/"]
COPY ["NewsLetterBoy.Repository/NewsLetterBoy.Repository.csproj", "NewsLetterBoy.Repository/"]
RUN dotnet restore "NewsLetterBoy.WebApi/NewsLetterBoy.WebApi.csproj"
COPY . .
WORKDIR "/src/NewsLetterBoy.WebApi"
RUN dotnet build "NewsLetterBoy.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsLetterBoy.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsLetterBoy.WebApi.dll"]