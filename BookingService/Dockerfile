#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookingService.Presentation/BookingService.Presentation.csproj", "BookingService.Presentation/"]
COPY ["BookingService.Application/BookingService.Application.csproj", "BookingService.Application/"]
COPY ["BookingService.Domain/BookingService.Domain.csproj", "BookingService.Domain/"]
COPY ["BookingService.Infrastructure/BookingService.Infrastructure.csproj", "BookingService.Infrastructure/"]
RUN dotnet restore "BookingService.Presentation/BookingService.Presentation.csproj"
COPY . .
WORKDIR "/src/BookingService.Presentation"
RUN dotnet build "BookingService.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookingService.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingService.Presentation.dll"]