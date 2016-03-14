# About this repository

This repository is aimed to share our ASP.NET MVC architecture, which defines many of the coding standards to achieve clean code. We have enjoyed a lot of benifits from it:

- few methods exeed 50 lines of code;
- few code comments are written, as namings describe the methods themselves;
- few file exeed 300 lines of code;
- every line of code has its right place, one can quickly finish a new feature like configuration.

This repository is a universal ASP.NET MVC architecture solution, including data access, role based authorization, mvc application lifetime, ajax, css, mvc routing, exception handling, Ioc, logging. 


We have been coding all our .NET projects based on this architecture since 2010. And we are keeping upgrading this architecture all the time to make it better and better.

To make it more applicable to most projects and easier to understand, I have been trying to write the minimum code to describe the core value of this architecture. So you can find out it is very light weight.

I also published this architecture online at [http://mvcsolution.985rencai.com](http://mvcsolution.985rencai.com) so that you can get a first look in a few minutes how it looks like. (Everyone can register as an admin by free and look into the admin control panel)

Finally, please remember that this architecture is only a guide for you to archietct your projects, and probably you need to customize it a little for your projects. I don't recommend you just copy the code and take it in use. Instead, I stronly recommend you read the documentation carefully, which is not only a simple doc but also a serias of architecture thinkings and considerations.

# Development Enviroments & References

* IDE:    **VS2015**
* NET Version: **4.5**
* MVC Version: **5.2**
* ORM: **Entity Framework 6.1**
* Json Serialization: **Newtonsoft 8.0.1**
* Unit Tests: **NUnit 2.5.10**
