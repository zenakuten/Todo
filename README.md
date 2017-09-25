# Todo Web API and Spa Client


This is a demonstration 'Todo' app consisting of a basic n-tier architecture in the backend, RESTful api, and Spa client.  


## Technologies

- ASP.NET Core 2.0
- VueJS 2
- Typescript


## Building

Requirements

 - Visual Studio 2017 (Community Edition)
 - Working NodeJS environment


 When first opening the solution in Visual Studio 2017, make sure to restore the nuget packages for the solution, or enable restore by default if it's not already.

 In addition to nuget packages, npm packages will also download upon first opening the solution.  This can take a while.  To see the output switch to 'bower/npm' output in the output window.

If npm packages do not automatically download upon first build, you can manually download them by opening a cmd prompt, navigate to the Todo.Client folder, and type 'npm install'

## Launching

The solution should by default have multiple projects selected to start when hitting F5.  If this does not work for you, click on the Todo solution in soluion explorer, then in the project menu select 'Set startup projects...'.  In the window, select 'multiple startup projects' and select 'Todo.Api' -> 'Start' and 'Todo.Client' -> 'Start'.

