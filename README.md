DROP TABLE

# Launching frontend
0. Have NodeJS installed (https://nodejs.org/en/).
1. Navigate to the `FrontendApartmentReservation` project
2. In the console, run `npm install`
3. Once all the modules are installed, run `npm run`.

# Launching backend from Visual Studio
0. Make sure you are running project with `local` settings. You can check it in `launchSettings.json`. Out-of-the-box file should be configured as `local` environment.
1. Simply run the solution as IIS Server or as standalone console application

# Launching backend as Docker container
0. Make sure you have docker installed
    * Windows https://docs.docker.com/docker-for-windows/install/
    * Linux https://docs.docker.com/install/linux/docker-ce/ubuntu/
1. Navigate to the `BackendApartmentReservation/BackendApartmentReservation` folder (the one that has `Dockerfile` in it)
2. From the console, build project using `docker build . -t whatever_name`
3. Run docker container using `docker run -p PORT:PORT whatever_name` where `PORT` is the port in `launchSettings.json`. Default is `5000`.

# Backend and frontend integration

## IIS Server
1. Routes should already be configured to the correct port. You are good to go.

## Standalone console application
1. Select `BackendApartmentReservation` as run option in the run choices dropdown.
2. If no settings were changed, application will run on port `5000`. There are two options to configure it to work
    * Edit `launchSettings.json` `BackendApartmentReservation` target to have the same port as IIS server/Docker image
    * Edit FE application `src/actions/index.jsx` file to use port `5000` 
