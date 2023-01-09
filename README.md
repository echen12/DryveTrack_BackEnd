
# Dryvetrack Backend

This is a revised backend for my full-stack application Dryvetrak. This time, I've written the API using ASP.Net 6 instead of Node.js. All endpoints are secured using JWT.

At this time, the backend will have to be run locally because I'm still learning how to set up CI/CD pipelines for my application.



## Installation

Make sure you have Visual Studio and Microsoft SQL Server Management Studio installed and running.

In the appsettings.json file, change the database connection string to the credentials of your SQL database.

After building the solution, in the console for package manager, run these two commands and the tables should build in the database.

```bash
  Add-Migration InitialMigration
  Update-Database
```

If a Swagger page appears when you start the solution, the API is running correctly.
## API Reference

The Swagger page contains all the api references.

