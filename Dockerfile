FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

# Copy everything else and build
COPY ./ ./
RUN dotnet test
RUN dotnet publish -c Release -o out src/AidanTwomey.PaymentsGateway.API/AidanTwomey.PaymentsGateway.API.csproj
#list files copied to output
RUN find
RUN du -ch out/


# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
EXPOSE 80
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "AidanTwomey.PaymentsGateway.API.dll"]
