#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /TransactionServices_BankAPI
COPY . .
RUN dotnet restore "./TransactionServices_BankAPI/TransactionServices_BankAPI.csproj" --disable-parallel
RUN dotnet publish "./TransactionServices_BankAPI/TransactionServices_BankAPI.csproj" -c release -o /app --no-restore

#Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /TransactionServices_BankAPI
COPY --from=build /app ./


ENTRYPOINT [ "dotnet", "TransactionServices_BankAPI.dll" ]