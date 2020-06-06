# _SweetCakes Bakery Menu Page_

#### _Epicodus Project June 5, 2020_

#### By _**Julia Seidman**_


## Description

_A project to introduce many-to-many relationships with MySQL in integration with the .NET Core framework with ASP.NET CORE MVC and MSBuild.  The project, a mock menu page for a bakery, also uses user authentication with Identity to distinguish between patrons who can view the menu and employees who may edit it.  This lesson serves as a reference for configuring, building, and launching web applications in C# with a SQL database backend. Dynamic sites using forms and views are explored with a web utility allowing users to add new bakery items in different flavor categories and to view bakery items by type or flavor._

## Setup/Installation Requirements

1. Clone this repository from GitHub.
2. Install MySQL on your computer.
3. Open the downloaded directory in a text editor of your choice. (VSCode, Atom, etc.)
4. In your terminal, navigate to the project directory and run the commands dotnet restore and dotnet build to download dependencies and build the configuration.
5. Currently, the mySQL database is set up with an appsettings.json file that was not uploaded to Github.  Once you have saved the SQL database on your computer, create an appsettings.json file in the project root directory, and update it with your MySQL installation information in this format:
  ```{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=YOUR PORT NUMBER HERE;database=DATABASE NAME AS YOU SAVED IT;uid=YOUR USER ID HERE;pwd=YOUR PASSWORD HERE;"
  }
}```
6. To run MySQL migrations and create a database in your MySQL installation, enter the following command in your terminal: ```dotnet ef database update```.
7. To launch the application in your browser, from the project directory in your terminal, enter ```dotnet run``` and open a browser page at localhost:5000.
8. For demo purposes, user accounts may be created with a mockup email and a single-character password, or you may use the account "pete@pete.com" with password "p".


## Known Bugs

There are no known bugs at the time of this update.

## Support and contact details

_Have a bug or an issue with this application? [Open a new issue] here on GitHub._

## Technologies Used

* C#
* .NET Core
* ASP.NET Core MVC
* MySQL
* Identity
* MSBuild
* Git and GitHub

### Specs
| Spec | Input | Output |
| :------------- | :------------- | :------------- |
| **Site allows bakery patron to see a list of all menu items** | User Input: "View Menu" | Output: "crumblepuff, gooeybun, sweetiepie" |
| **Site allows bakery patron to see a list of all menu flavors** | User Input: "View Menu By Flavor" | Output: "brambleberry, cheddar chive, cinnamon" |
| **Site allows bakery patron to see details of menu items** | User Input: "Crumblepuff" | Output: "Crumblepuff, $3, brambleberry, cheddar chive |
| **Site allows bakery patron to see all menu items in a certain flavor** | User Input: "Brambleberry" | Output: "Crumblepuff, $3, See other flavors" |
| **Site allows user to create an employee account** | User Input: "Joe", "joe@joejoe.com", "j" | "Welcome, Joe!" |
| **Site allows user to log in to access editing functionality** | User Input: "joe@joejoe.com", "j" | "Welcome, Joe! Create new items." |
| **Site allows bakery employee to create new menu items** | User Input: "honeycake, 4" | Output: "honeycake, $4, Flavors: Plain" |
| **Site allows bakery employee to create new flavors** | User Input: "Raisin Nut" | Output: "brambleberry, cheddar chive, cinnamon, Raisin Nut" |
| **Site allows bakery employee to edit details of menu items** | User Input: "3" | Output: "Honeycake, $3, Flavors: Plain|
| **Site allows bakery employee to edit details of flavors** | User Input: "nutty raisin" | Output: "brambleberry, cheddar chive, cinnamon, nutty raisin" |
| **Site allows bakery employee to add flavors to an existing menu item** | User Input: "nutty raisin" | Output: "Honeycake, $3, Flavors: nutty raisin" |
| **Site allows bakery employee to add menu items to an existing flavor** | User Input: "Crumblepuff" | Output: "Crumblepuff, $3, Flavors: brambleberry, cheddar chive, nutty raisin" |
| **Site allows bakery employee to delete menu items** | User Input: "mushybuns" | Output: "Are you sure you want to delete mushybuns?"  |
| **Site allows bakery employee to delete flavors** | User Input: "durian" | Output: "Are you sure you want to delete durian?" |


### License
This software is licensed under the MIT license.

Copyright (c) 2020 **_Julia Seidman_**