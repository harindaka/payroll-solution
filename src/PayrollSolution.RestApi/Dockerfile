#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/PayrollSolution.RestApi/PayrollSolution.RestApi.csproj", "src/PayrollSolution.RestApi/"]
COPY ["src/PayrollSolution.Application/PayrollSolution.Application.csproj", "src/PayrollSolution.Application/"]
COPY ["src/PayrollSolution.Domain/PayrollSolution.Domain.csproj", "src/PayrollSolution.Domain/"]
COPY ["src/PayrollSolution.Infrastructure/PayrollSolution.Infrastructure.csproj", "src/PayrollSolution.Infrastructure/"]
RUN dotnet restore "src/PayrollSolution.RestApi/PayrollSolution.RestApi.csproj"
COPY . .
WORKDIR "/src/src/PayrollSolution.RestApi"
RUN dotnet build "PayrollSolution.RestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PayrollSolution.RestApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PayrollSolution.RestApi.dll"]