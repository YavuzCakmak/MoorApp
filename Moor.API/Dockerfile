FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnet-build
WORKDIR /src 
EXPOSE 80
EXPOSE 443

COPY *.sln .
COPY Moor.API/*.csproj ./Moor.API/
COPY Moor.Service/*.csproj ./Moor.Service/
COPY Moor.Repository/*.csproj ./Moor.Repository/
COPY Moor.Core/*.csproj ./Moor.Core/
COPY Moor.Model/*.csproj ./Moor.Model/
RUN dotnet restore "Moor.API/Moor.API.csproj"
#
COPY Moor.API/. ./Moor.API/
COPY Moor.Service/. ./Moor.Service/
COPY Moor.Repository/. ./Moor.Repository/
COPY Moor.Core/. ./Moor.Core/
COPY Moor.Model/. ./Moor.Model/
WORKDIR /src/Moor.API
RUN dotnet build "Moor.API.csproj" -c Release -o /app/build

FROM dotnet-build AS dotnet-publish
RUN dotnet publish "Moor.API.csproj" -c Release -o /app/publish 

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
WORKDIR /app
RUN mkdir /app/wwwroot
RUN mkdir /app/files
COPY --from=dotnet-publish /app/publish .
ENTRYPOINT ["dotnet", "Moor.API.dll"]
