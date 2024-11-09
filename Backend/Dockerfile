FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY *.sln ./
COPY Api/*.csproj ./Api/
COPY Model/*.csproj ./Model/

RUN dotnet restore

COPY . ./

RUN dotnet tool install --global dotnet-ef

ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet publish Api/Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

COPY --from=build-env /App/out .

EXPOSE 80

CMD ["dotnet", "Api.dll"]
