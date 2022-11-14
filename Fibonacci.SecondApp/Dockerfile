#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Fibonacci.SecondApp/Fibonacci.SecondApp.csproj", "Fibonacci.SecondApp/"]
#COPY ./certf.crt /usr/local/share/ca-certificates/certf.crt
RUN dotnet restore "Fibonacci.SecondApp/Fibonacci.SecondApp.csproj"
COPY . .
WORKDIR "/src/Fibonacci.SecondApp"
RUN dotnet build "Fibonacci.SecondApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fibonacci.SecondApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fibonacci.SecondApp.dll"]