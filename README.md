# JDT Analyzer - Bash Entrypoint Demo

This sample demonstrates a .NET 9 container with a custom `bash` entrypoint script for pre-start tasks like listing analyzed JDTs.

## Files

- `Dockerfile` - Multi-stage Docker build with bash support
- `run.sh` - Script to list `/app/analyzed-jdts` contents before running the app
- `HelloWorldApp/` - Minimal ASP.NET Core app

## Usage

1. Build the image:
   ```bash
   docker build -t jdt-analyzer-demo .
   ```

2. Run the container:
   ```bash
   docker run -v $(pwd)/sample-data:/app/analyzed-jdts jdt-analyzer-demo
   ```

3. The app will:
   - List the contents of the `analyzed-jdts` folder
   - Start the ASP.NET Core app