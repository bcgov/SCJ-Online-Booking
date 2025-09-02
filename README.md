# SCJ-Online-Booking
Superior Courts Judiciary Online Booking

![img](https://img.shields.io/badge/Lifecycle-Stable-97ca00)


## Dev environment setup

### Visual Studio Code setup
(assuming you have an ARM macbook)

- Install the .NET 8 SDK Installer for ARM
- Install C# extension for Visual Studio Code
- Install C# Dev Kit for Visual Studio Code
- Install csharpier https://csharpier.com/
- Install CSharpier plugin for VS Code

### Clone the project 
https://github.com/bcgov/SCJ-Online-Booking

check out the `develop` branch

### Copy the .env file

`cp .env.example .env`

Get the `KEYCLOAK_CLIENT_SECRET` from someone else on the team

For `SENDGRID_FROM_EMAIL` and `SENDGRID_API_KEY`, you can either create your own Sendgrid account or get someone else's credentials.  

### Do a test build from the terminal

make sure `dotnet run` works from the /app folder

you should be able to connect on http://localhost:5002/

### Build the css & vue

Make sure you are using Node 18 by running `node -v`

`cd app`

`npm install`

`npm run build` or  `npm run watch`

### Running tests

`cd tests`

`dotnet test`

\tests\bin\Debug\net8.0\scj-booking.sqlite will be generated.  You can use https://sqlitestudio.pl/ to inspect this file and look at test results. 

### Debugging

Find "Solution Explorer" in the VS Code explorer panel.  Right click on SCJ.Booking.MVC and select "Debug => Start New Instance"

you should be able to connect on https://localhost:5002/

### Running the app
 
You will need to register for a BCeID account on the BCeID dev environment. 
https://www.development.bceid.ca/register/


## Test cases

### Supreme Court test cases

- Vancouver / E (Family Law) / 23222
- Kelowna / (leave the class blank)  / 111


### Court of Appeal test cases

- CA39029 - Civil Case
- CA42024 - Criminal case
- CA39000 - Civil case with a child case number
- CA39001 - Civil case with a parent case number
- CA39002 - Civil case with a parent case number


## License

    Copyright 2019 British Columbia Superior Courts

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
