# InHouse Restaurant Listing API


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