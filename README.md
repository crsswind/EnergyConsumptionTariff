# EnergyConsumptionTariff


This project implements a model to build up two tariffs and compare them based on their annual costs. 
The project is implemented in C# using Clean Architecture principles, and it includes two layers: Core and Test.

## Core Layer

The Core layer contains the core business logic of the application, including entities, services, and repositories. 
The entities represent the core business objects and concepts of the application, while the services provide the main functionality of the application, such as calculating the annual costs of different tariffs. 
The repositories are responsible for creating instances of the tariff calculators based on a given consumption value.

The Core layer is designed to be technology-agnostic and independent of any specific user interface or infrastructure concerns. 
This layer is the most important layer in the architecture as it represents the core business logic of the application.

## Test Layer

The Test layer contains unit tests for the Core layer. 
The tests are implemented using the xUnit testing framework, along with the Fluent Assertions and FakeItEasy libraries.

The Test layer ensures that the Core layer is working correctly and provides a safety net for making changes to the code. 
The tests cover edge cases and ensure that the application is functioning as expected.

## Why Only Core and Test Layers?

For this project, I decided to implement only the Core and Test layers. 
The reason for this is that the project is small and straightforward, and adding more layers and libraries could lead to unnecessary complexity and overhead.

Since this project only focuses on calculating the annual costs of the two tariffs and does not involve any user interfaces or external systems, there is no need for implementing any API endpoints or other infrastructure-related concerns.

By keeping the architecture simple, I was able to focus on the core business logic of the application and ensure that it is working correctly through unit tests.

However, it's important to note that in larger or more complex projects that involve user interfaces and external systems, additional layers and infrastructure-related concerns may be required to ensure proper separation of concerns and maintainability.
