# InHouse Restaurant Listing API
 
 [![.NET](https://github.com/akshayjoyinfo/RestaurantListing/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/akshayjoyinfo/RestaurantListing/actions/workflows/dotnet.yml)
 [![Coverage Status](https://coveralls.io/repos/github/akshayjoyinfo/RestaurantListing/badge.svg?branch=main)](https://coveralls.io/github/akshayjoyinfo/RestaurantListing?branch=main)
[![CodeQL](https://github.com/akshayjoyinfo/RestaurantListing/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/akshayjoyinfo/RestaurantListing/actions/workflows/codeql-analysis.yml)


## Live Demo

	Deployed the APP to Heroku using Container Registry
	https://inhouse-restaurants-dev.herokuapp.com/health

	test the APi with 
	
	http://inhouse-restaurants-dev.herokuapp.com/v1/restaurants/listings



## Build & Run Solution

1. Change Directory to \src\InHouse

	>cd \src\InHouse

2. Build the Solution along with restore
	
	>dotnet restore
	>
	>dotnet build

3. Run the api by cd into cd cd InHouse.API

	> dotnet run
    > 
	> launch the https://localhost:5001/health

4. Run the API using VS 2019
	
	F5
	> launch https://localhost:44314/health

5. Run the swagger endpoint with below url

	Note: If you are running this from VS 2019 or higher first time, it might ask you for the Self Signed Certificate Allow option. Please Click on Allow which will register HTTPS IIS DevCertificate Self Signed in the System

	> https://localhost:44314/swagger
	>
	> OR
	>
	> https://localhost:44314/swagger
	>
	>OR
	>
	> https://localhost:44314/swagger/v1/swagger.json
	

Check below screenshots for Build & Restore

![BuldNRestore](docs\images\build_n_restore.png)


## Running Integration Tests

I have used Asp.Net Core Integration WebApplicationFactory with Mvc.Testing.
I have added tests for Couple of Special case 

	dotnet test	

![BuldNRestore](docs\images\test.png)


## Postman Collection

	I have added postman collections for API Samples along with Environemnts
	Use LocalIIS- if you are running from VS IIS Express
	Use LocalKEstrel - use it when u are runiing with Kestrel


## Code Coverage Results : 96%

![CoeCoverage](docs\images\code-coverage.png)

## Logging (Optional , already have Logging in VS Console / Kestrel)
Enabled Logging with Serilog Console and Seq

	1. Create folder in the D:\SeqLogs to keep logs always and querable
  	2. docker run --rm -it -e ACCEPT_EULA=Y -p 5341:80 -v D:\SeqLogs:/data datalust/seq
	3. Once asked just share it 
	3. once all good, check the url here http://localhost:5341/#/events --- note please use the port one which used docker run command

## Deployment

	I will be using AppVeyor for Deploy this using Github PR, but after submitting the
	assignment I will be working on this

## Docker Setup Local
	docker build -t inhouse-api -f DockerfileLocal .
	docker run -it -p 5051:80 --name abc inhouse-api:latest

## Heroku Deployment Using Container Registry

	heroku container:login
	docker build -t inhouse-restaurants-dev .
	heroku container:push -a inhouse-restaurants-dev web
	heroku container:release -a inhouse-restaurants-dev web

