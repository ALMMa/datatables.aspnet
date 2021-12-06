# Datatables.AspNet

Simplified opinionated model binding and mapping from jQuery DataTables into .NET typed models with support for `AspNet Core 3.1` and `.NET 6.0`.

## Compatibility

This library is compatible with the following stacks:

- `AspNet Core 3.1`
- `.NET 6.0`

Moving forward, support will be added only for LTS versions of `.NET`.

## Standard NuGet packages

- [DataTables.AspNet.AspNetCore](https://www.nuget.org/packages/DataTables.AspNet.AspNetCore/) with support for AspNetCore, dependency injection and automatic binders

### IMPORTANT: Deprecated (unlisted) package

- [DataTables.AspNet.Mvc5](https://www.nuget.org/packages/DataTables.AspNet.Mvc5/) with support for Mvc5, registration and automatic binders

- [DataTables.AspNet.WebApi2](https://www.nuget.org/packages/DataTables.AspNet.WebApi2/) with support for WebApi2, registration and automatic binders

- [DataTables.AspNet.AspNet5](https://www.nuget.org/packages/DataTables.AspNet.AspNet5/)

All those packages are now considered deprecated and won't be updated anymore, as per latest Microsoft SDLC and framework versioning.

### Write your own code!

DataTables.AspNet ships with a core project called [DataTables.AspNet.Core](https://www.nuget.org/packages/DataTables.AspNet.Core/), which contains basic interfaces and core elements just the way DataTables needs.<br />
Feel free to use it and implement your own classes, methods and extend DataTables.AspNet in <i>your</i> very own way.

### Helpers and extensions

- [DataTables.AspNet.Extensions.AnsiSql](https://www.nuget.org/packages/DataTables.AspNet.Extensions.AnsiSql/) enables basic translation from sort and filter into ANSI-SQL `WHERE` and `ORDER BY`

- [DataTables.AspNet.Extensions.DapperExtensions](https://www.nuget.org/packages/DataTables.AspNet.Extensions.DapperExtensions/) transforms filters into `IPredicate` and sort into `ISort`

Those are still alpha1 releases but with nuget packages available. There are no tests yet, they are in a very initial phase and might change a bit in the near future.
After they become stable I'll accept pull requests for other extensions (eg: NHibernate, EntityFramework, etc). For now, keep in mind that these two are supposed to set the basic extension standard for DataTables.AspNet.Extensions.

## Samples

Samples are provided on the `samples` folder.<br />

## Eager for some new code?

If you are, check out [dev](https://github.com/ALMMa/datatables.aspnet/tree/dev) branch. It has the latest code for DataTables.AspNet, including samples and more.<br />
For every release (even unstable ones) there should be a nuget package.
