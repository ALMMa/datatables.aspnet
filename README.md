<h1>Datatables.AspNet</h1>

Simplified opinionated model binding and mapping from jQuery DataTables into .NET typed models with support for `NETStandard 2.0`, `.NET 5.0` and `.NET 6.0`.

<h3>Standard NuGet packages</h3>

- [DataTables.AspNet.AspNetCore](https://www.nuget.org/packages/DataTables.AspNet.AspNetCore/) with support for AspNetCore, dependency injection and automatic binders

<h3>IMPORTANT: Deprecated (unlisted) package</h3>

- [DataTables.AspNet.Mvc5](https://www.nuget.org/packages/DataTables.AspNet.Mvc5/) with support for Mvc5, registration and automatic binders

- [DataTables.AspNet.WebApi2](https://www.nuget.org/packages/DataTables.AspNet.WebApi2/) with support for WebApi2, registration and automatic binders

- [DataTables.AspNet.AspNet5](https://www.nuget.org/packages/DataTables.AspNet.AspNet5/)

All those packages are now considered deprecated and won't be updated anymore, as per latest Microsoft SDLC and framework versioning.

<h3>Write your own code!</h3>

DataTables.AspNet ships with a core project called [DataTables.AspNet.Core](https://www.nuget.org/packages/DataTables.AspNet.Core/), which contains basic interfaces and core elements just the way DataTables needs.<br />
Feel free to use it and implement your own classes, methods and extend DataTables.AspNet in <i>your</i> very own way.

<h3>Helpers and extensions</h3>

- [DataTables.AspNet.Extensions.AnsiSql](https://www.nuget.org/packages/DataTables.AspNet.Extensions.AnsiSql/) enables basic translation from sort and filter into ANSI-SQL `WHERE` and `ORDER BY`

- [DataTables.AspNet.Extensions.DapperExtensions](https://www.nuget.org/packages/DataTables.AspNet.Extensions.DapperExtensions/) transforms filters into `IPredicate` and sort into `ISort`

Those are still alpha1 releases but with nuget packages available. There are no tests yet, they are in a very initial phase and might change a bit in the near future.
After they become stable I'll accept pull requests for other extensions (eg: NHibernate, EntityFramework, etc). For now, keep in mind that these two are supposed to set the basic extension standard for DataTables.AspNet.Extensions.

<h3>Samples</h3>
Samples are provided on the `samples` folder.<br />

<h3>Eager for some new code?</h3>
If you are, check out [dev](https://github.com/ALMMa/datatables.aspnet/tree/dev) branch. It has the latest code for DataTables.AspNet, including samples and more.<br />
For every release (even unstable ones) there should be a nuget package.
