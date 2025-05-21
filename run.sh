#!/bin/bash
echo "Listing analyzed JDTs:"
ls /app/analyzed-jdts || echo "/app/analyzed-jdts does not exist or is empty."

# Start the application
dotnet HelloWorldApp.dll