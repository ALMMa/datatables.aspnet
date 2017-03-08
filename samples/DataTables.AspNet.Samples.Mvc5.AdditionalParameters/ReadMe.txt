Asp.Net MVC5 additional parameters sample
=========================================

This sample should provide you with the instructions to enable both request and response additional parameters for DataTables in a regular ASP.Net MVC 5 project.

What you should know:
---------------------

1) This sample does not guide you through DataTables plugin usage.
Check DataTables website for that: https://www.datatables.net

2) Although this sample shows you the way to receive (parse/request) and provide (response) additional parameters for DataTables.AspNet into your MVC project, it is assumed
that you already understand ASP.NET MVC. Also, consider that this behavior is creating much confusion on users and might be improved (aka: breaking changes) in the future.
For ASP.NET MVC check Microsoft website for that: http://www.asp.net/mvc/overview/getting-started/introduction/getting-started

3) Understand that JsonResult derives from ActionResult and that DataTablesJsonResult derives from JsonResult and that's why
returning is from our action works just fine.



What you won't learn here:
--------------------------

Anything besides the very basic integration. This is meant to keep everything clear.



What you will learn:
--------------------

1) DataTables.AspNet registration with default options.

2) DataTables Request/Response workflow with automatic model binding.
