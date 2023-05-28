# Payroll Solution

This document briefly explains the codebase structure and provides instructions on how to run it and execute any included tests

## Solution Components

The code is pretty much self explanatory and includes additional comments in critical areas. The solution is based on Clean Architecture and Domain Driven Design. It contains the following projects,

* PayrollSolution.Domain - Contains all domain objects that encapsulate domain specific attributes and behaviors (entities, value objects, factory methods, domain validation etc.);
* PayrollSolution.Application - Contains any business logic / application services and provides a reusable library that can be referenced by any front end applications. In process CQRS via Mediatr provides necessary decoupling between these front end applications and the business logic
* PayrollSolution.Infrastructure - Contains implementations that address infrastructure concerns such as persistance. e.g. repository implementations
* PayrollSolution.RestApi - Provides a lightweight REST API which exposes the business functions in the application layer to the outside (ultimately consumed by the web ui)
* PayrollSolution.Ui - This user facing web ui project based on Blazor provides the necessary user interface for accessing the underlying domain functionality

## Running the Solution

You will need docker and docker-compose installed with support for linux containers on the host
To build and run the solution;

* Clone this repository
* From within the solution folder run `docker-compose up -d --build`. This should build the solution inside intermediate docker containers before spinning up seperate containers for hosting the rest api and the web ui applications. Note that host ports 3080 and 4080 are used by the web ui and rest api respectively

## Accessing the UI

The Web UI should be available via [http://localhost:3080/](http://localhost:3080/) on your browser

## Accessing the REST API

The web ui itself accesses the rest api [via docker's internal dns resolution](https://github.com/harindaka/payroll-solution/blob/a0d8ea5e92efcfa86d55093399729a39cd071b9d/src/PayrollSolution.Ui/appsettings.json#L4). However it should also be accessible to the host via [http://localhost:4080/](http://localhost:4080/) and API documentation via [http://localhost:4080/swagger/index.html](http://localhost:4080/swagger/index.html)

## Running Tests

You can build and run the tests directly on the host. You will need the .Net v7 SDK installed before proceeding.

* Navigate to the solution folder
* Restore dependencies with `dotnet restore PayrollSolution.sln`
* Build with `dotnet build PayrollSolution.sln`
* Run tests with `dotnet test PayrollSolution.sln`

## Further Considerations

Additional tests may be introduced to cover domain object creation and validation, alternative api responses etc. which I've excluded in the interest of time.
