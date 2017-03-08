Basic Asp.Net MVC5 integration sample
=====================================

This sample should provide you with the very basics for DataTables (1.10+) integration in a regular ASP.Net MVC 5 project.

What you should know:
---------------------

1) This sample does not guide you through DataTables plugin usage.
Check DataTables website for that: https://www.datatables.net

2) Although this sample shows you the most simple way to integrate DataTables.AspNet into your MVC project, it is assumed
that you already understand ASP.NET MVC.
Check Microsoft website for that: http://www.asp.net/mvc/overview/getting-started/introduction/getting-started

3) Understand that JsonResult derives from ActionResult and that DataTablesJsonResult derives from JsonResult and that's why
returning is from our action works just fine.



What you won't learn here:
--------------------------

Anything besides the very basic integration. This is meant to keep everything clear.



What you will learn:
--------------------

1) DataTables.AspNet registration with default options.

2) DataTables Request/Response workflow with automatic model binding.
