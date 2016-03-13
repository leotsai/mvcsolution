MVC Solution.com-2
===========

This repository is to build a complete solution template for ASP.NET MVC, so that you can quickly finish the architecture of an ASP.NET MVC website.

## How to Use This Repository

1. Download all the source code and open the solution file(.sln) in Visual Studio.
2. On the Visual Studio menu bar, click "File" > "Export Template", and then export EVERY project in this solution to a template.
3. Close the solution.
4. Create your website from the exported templates. In Visual Studio, "File" > "New" > "Project", you will the exported templates. If you cannot see, you can search "MvcSolution." on the top-right search box. You don't have to create projects for "Admin" or "Api" if your website doesn't include them, but you should create projects for other templates.
5. Fix DLL references. Copy the "Libs" folder from the downloaded source code to your newly created solution (remember to match the relative path).
6. Replace text "MvcSolution" to yours. In the new whole solution, relace "MvcSolution" to {YourSolution} (tick match case). Other possible relaces may be "mvcsolution", "mvcSolution", "MVCSolution", etc.
7. Rename files. Replace file names "MvcSolution" with {YourSolution}. 
8. Build the new solution and fix errors. Try running.

## Understanding The Layers
There are mainly 4 layers: infrastructure, data, services, web. It's easy to understand, isn't it?

### 1. MvcSolution.Infrastructure
This is foundation project of the solution, and it should be referenced by all other projects.

It contains extenstions to .NET Framework, MVC, third party libraries and some helper methods. A few other features are included by default, such as IOC, Logging, Captcha. 

### 2. MvcSolution.Data
This project uses Entity Framework Code First mode to achieve data access. It defines the database and tables(entities). This project should also contain the minimum unit of business logic. In our practices, we put the logics to entity definitions, and IQueryable<TEntity> extensions. 

### 3. MvcSolution.Services
This layer is used to access the database and return results to the Web layer. Any database access should stop at the services layer, which means the Web layer must use services to do data access. 

The DbContext instances should be disposed in every service methods. 

### 4. MvcSolution.Web
This layer handle user requests and renders the data in front of the end users. 

In this template solution, there are projects in the Web layer. They are:

1. **MvcSolution.Web**: shared project among child websites (if any). You can it WebBase project.
1. **MvcSolution.Web.Main**: the real website that contains global.asax (defining application entrance) and web.config. 
1. **MvcSolution.Web.Admin**: actually an MVC area for admin. It’s useful to plug in an area to your website without influencing the existing website.
1. **MvcSolution.Web.Api**: an MVC area for API access.

## Understanding The UnitTests
We used NDB unit test for this solution. It’s useful to test your data access code in a real database.

There are actually two projects in the UnitTests solution folder. 

### MvcSolution.UnitTestBase
The main unit test architecture, and your every other unit test project should reference it. 

I plan to write a separate wiki page to document how to use it, but for now you can refer to the MVCSolution.UnitTests.Services project.

### MVCSolution.UnitTests.Services
A sample unit test project demonstrating how to use MvcSolution.UnitTestBase.

## Grunt
Grunt is used for professional websites. I don’t recommend you use it if your project doesn’t have too many users. 

Please refer to Gruntfile.js to see how grunt is gracefully configured.
