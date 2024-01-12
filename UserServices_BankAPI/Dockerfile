#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /UserServices_BankAPI
COPY . .
RUN dotnet restore "./UserServices_BankAPI/UserServices_BankAPI.csproj" --disable-parallel
RUN dotnet publish "./UserServices/UserServices.csproj" -c release -o /app --no-restore

#Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /UserServices_BankAPI
COPY --from=build /app ./


ENTRYPOINT [ "dotnet", "UserServices_BankAPI.dll" ]