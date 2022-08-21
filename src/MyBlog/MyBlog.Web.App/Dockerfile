#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyBlog.Web.App/MyBlog.Web.App.csproj", "MyBlog.Web.App/"]
COPY ["MyBlog.Application/MyBlog.Application.csproj", "MyBlog.Application/"]
COPY ["MyBlog.Domain/MyBlog.Domain.csproj", "MyBlog.Domain/"]
COPY ["MyBlog.Share/MyBlog.Share.csproj", "MyBlog.Share/"]
COPY ["MyBlog.Infrastructure/MyBlog.Infrastructure.csproj", "MyBlog.Infrastructure/"]
RUN dotnet restore "MyBlog.Web.App/MyBlog.Web.App.csproj"
COPY . .
WORKDIR "/src/MyBlog.Web.App"
RUN dotnet build "MyBlog.Web.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyBlog.Web.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyBlog.Web.App.dll"]