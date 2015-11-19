#Prerequisites

*Visual Studio 2013 Update 4, run as Administrator (so that the solution can create a web application under the default website)
*NuGet 2.8.6+ (a requirement of xUnit)

#Summary
The solution contains the following projects:
*Justin.1800Contacts.Logic - Interfaces and functionality to sort using a topological search algorithm and a course specific implementation.
*Justin.1800Contacts.Logic.Tests - xUnit tests asserting the facts of the logic layer components.
*Justin.1800Contacts.App - A console base application that lets you load class data from a file and outputs the sorted classes to the console. This app utilizes the same logic layer as the web application.
*Justin.1800Contacts.Api - Exposes functionality in the logic layer through Web API 2.2. Controller is initialized using a Unity dependency resolver.
*Justin.1800Contacts.Web - MVC web application, implementing Angular for MVVM, communicating with the accompanying API to provide a Bootstrap UX for sorting out which courses to take.
