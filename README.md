# About this repository

This repository is aimed to share our ASP.NET MVC architecture, which defines many of the coding standards to achieve clean code. We have enjoyed a lot of benifits from it:

- few methods exceed 50 lines of code;
- few code comments are written, as naming describes the methods themselves;
- few file exceed 300 lines of code;
- every line of code has its right place, one can quickly finish a new feature like configuration.

This repository is a universal ASP.NET MVC architecture solution, including data access, role based authorization, mvc application lifetime, ajax, css, mvc routing, exception handling, Ioc, logging. 


We have been coding all our .NET projects based on this architecture since 2010. And we are keeping upgrading this architecture all the time to make it better and better.

To make it more applicable to most projects and easier to understand, I have been trying to write the minimum code to describe the core value of this architecture. So you can find out it is very light weight.

I also published this architecture online at [http://mvcsolution.985rencai.com](http://mvcsolution.985rencai.com) so that you can get a first look in a few minutes how it looks like. (Everyone can register as an admin by free and look into the admin control panel)

Finally, please remember that this architecture is only a guide for you to architect your projects, and probably you need to customize it a little for your projects. I don't recommend you just copy the code and take it in use. Instead, I strongly recommend you read the documentation carefully, which is not only a simple doc but also a serials of architecture thinkings and considerations.

# Development Environments & References

* IDE:    **VS2015**
* NET Version: **4.5**
* MVC Version: **5.2**
* ORM: **Entity Framework 6.1**
* Json Serialization: **Newtonsoft 8.0.1**
* Unit Tests: **NUnit 2.5.10**

# Projects References

First, take a look at the projects and their reference relationships.

![references](https://raw.githubusercontent.com/leotsai/mvcsolution/master/_doc/images/project%20references.jpg)

# Documentation Index

### [01.Get Started](https://github.com/leotsai/mvcsolution/blob/master/_doc/01.get-started.md)
* Compile
* Initialize Database
* Run the Web Application
* Publish
* Conclusion

### [02. Architecture Overview](https://github.com/leotsai/mvcsolution/blob/master/_doc/02.architecture-overview.md)
* Projects References
*	Application Start
*	MVC Areas & Routes
*	AppContext & SettingsContext
*	Exception Handling
*	Error Logging 
*	IOC
*	Manage External DLLs

### [03. Data Access](https://github.com/leotsai/mvcsolution/blob/master/_doc/03.data-access.md)
* Database & Tables Definition
* Access to Database CRUD
* Data Migration

### [04. Role Based Authorization](https://github.com/leotsai/mvcsolution/blob/master/_doc/04.role-based-authorization.md)
* Login 
* Logout
* Session
* Cookies

### [05. Grunt & JS Architecture](https://github.com/leotsai/mvcsolution/blob/master/_doc/05.grunt-js-architecture.md)
* Install Grunt
* Grunt Config & JS File Structore

### [06. CSS Architecture](https://github.com/leotsai/mvcsolution/blob/master/_doc/06.css-architecture.md)

### [07. MISC](https://github.com/leotsai/mvcsolution/blob/master/_doc/07.misc.md)
* Unit Tests
* Grid





# License
MIT. Feel free to use and ask questions. I'm open to any discussion.
