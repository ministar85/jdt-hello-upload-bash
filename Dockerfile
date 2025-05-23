# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src
COPY . .
RUN dotnet publish HelloWorldApp/HelloWorldApp.csproj -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime
RUN apt-get update && apt-get install -y bash
WORKDIR /app
COPY --from=build /app .
COPY run.sh .
RUN chmod +x run.sh
RUN mkdir -p /app/analyzed-jdts
ENTRYPOINT ["bash", "-c", "./run.sh && dotnet HelloWorldApp.dll"]
