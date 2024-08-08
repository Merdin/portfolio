# StudyM8

Last updated on 16-05-2021.

## Development environments

### Docker (MacOS/Linux)
1. Install Visual Studio for Mac/Linux and Docker & Make sure you install support for DotNet Core 3.x within the Visual Studio installer.
2. Execute the following command to install Entity Framework globally: `dotnet tool install -g dotnet-ef`.
4. Assure you've prepended the PATH variable as requested in the installer of the previous command.
5. Use the following command: `cd Back-end && dotnet ef database update && cd ../`
6. Execute the following command: `./start.sh dev`.
7. Run the Back-end and Front-end projects.
8. Happy coding!

### Docker (Windows) 
1. Install Visual Studio and Docker for Windows & Make sure you install support for DotNet Core 3.x within Visual Studio installer.
2. Open the Solution and enter the NuGet Package Manager Terminal and enter the following command: `Database-Update`.
3. Execute the following command: `docker-compose up -d`.
4. Run the Back-end and Front-end projects.
5. Happy coding!

## Production and Test-environments

### Front-End

With Traefik, the front-end is available at port 443. Port 80 will redirect to 443.

### Back-End

The back-end is not available directly. In the docker stack, it's available via `http://back-end:80`.

## Quirks

### Front-end

In case you want to change the domain names of test or production, please update the hostnames
in `docker-compose.*.yml` (* being the environment `prod`/`test`.)

### Back-end

If you would like to communicate with the back-end directly, please add the port directive to `docker-compose.yml` in
your local development environment.

### Gecko (only mac users):

- Open any terminal on mac.
- Navigate to the following directory:
  `studym8/TestCases/bin/Debug/netcoreapp3.1`
- Run this command:
  `xattr -r -d com.apple.quarantine geckodriver`