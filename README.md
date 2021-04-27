# CallCenter-Agent-System

## Get Code
	1. Clone repository in your local system
	2. Download the code file
	
## Applicaiton Version
	1. Application is developed on .net core 3.1 - https://dotnet.microsoft.com/download/dotnet/3.1
	2. Database used - MS SQL Server
	

## Set up the application locally

In CallCenter.Agent.Server project - use your local database source 

	Update connection string in appsettings.json

## Setup database locally 
	verify <CallCenter.Agent.Server> application should be selected as startup applicaiton
	open <package manager console> and run the below EF code first command

Create Identity Database for User -
	
	add-migration CallCenterUserDatabase -Context ApplicationDBContext
	update-database -Context ApplicationDBContext


Create Agent System Database -
	
	add-migration CallCenterDatabase -Context CallCenterDbContext
	update-database -Context CallCenterDbContext
	
Verify the database in sql server studio

## Run Application

	verify - CallCenter.Agent.Server application should be selected as startup applicaiton
	
	run application 
	
## Check Swagger Page

	go to https://localhost:44399/swagger/index.html
	verify the endpoints

## Agent UI Application
	
	go to https://localhost:44399/login
	if first time: you have to create the user click on <register user>
	
	once the user is created and logged-in, user can only see the home page as he don't have any roles currently.
	
## Add User Role

	Go swagger page - https://localhost:44399/swagger/index.html
	use: ./api/Auth/AddUserRole enpoint to create a user role

	For demo purpose create the following roles - 
		1. Admin
		2. OperationEngineer
		3. TeamLead
		4. SystemAdministrator
		
	

	

  
  


