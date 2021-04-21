Getting Started

### Prerequisites
Install
- .NET Core SDK 5.0: https://dotnet.microsoft.com/download/dotnet/5.0
- SQL Server Express 2019: https://www.microsoft.com/en-us/sql-server/sql-server-downloads

### Run web
- Change SQL connection string in appsetting.json
> "Demo": "Server=127.0.0.1;Database=Demo;User Id=sa;Password=Admin@123"

- At the project root, run cmd 

`cd src/Web`  
`dotnet run -c Release`

 - Access https://localhost:5001 in the browser

 ### Run API

 - Change SQL connection string in appsetting.json
> "Demo": "Server=127.0.0.1;Database=Demo;User Id=sa;Password=Admin@123"

- At the project root, run cmd 

`cd src/PublicApi `  
`dotnet run -c Release`

 - Open another cmd session, run
 ```
curl --location --request GET 'https://localhost:5001/api/reports/roomtypesummary'
 ```
or run by Postman or your favourite API testing tool.