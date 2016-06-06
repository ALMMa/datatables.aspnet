<h1>Datatables.AspNet</h1>
Formely known as <strong>DataTables.Mvc</strong>, this project started over a year ago with small objectives, aiming to provide intermediate and experienced developers a tool to avoid the boring process of handling DataTables parameters.<br />
More than 15 months later, we now undergo a full rewrite with support for Mvc, WebApi and AspNet (<strong>core CLR included!</strong>).<br />
Also, unit-testing is a priority to avoid breaking <i>your</i> app.

<h3>Beta4 is here with DotNet Core support!</h3>
`Beta4` release now ships with support for DotNet Core RC2, along with extension and a complete rewrite of tests.
This is the fourth beta release for `DataTables.AspNet`. A full migration path (for all, including MVC 5 and WebApi) is now being released.

<h3>Standard NuGet packages</h3>
- [DataTables.AspNet.Mvc5](https://www.nuget.org/packages/DataTables.AspNet.Mvc5/) with support for Mvc5, registration and automatic binders
- [DataTables.AspNet.WebApi2](https://www.nuget.org/packages/DataTables.AspNet.WebApi2/) with support for WebApi2, registration and automatic binders
- [DataTables.AspNet.AspNet5](https://www.nuget.org/packages/DataTables.AspNet.AspNet5/) >> DEPRECATED (Renamed to AspNetCore as bellow)
- [DataTables.AspNet.AspNetCore](https://www.nuget.org/packages/DataTables.AspNet.AspNetCore/) with support for AspNetCore, dependency injection and automatic binders

You can extend DataTables.AspNet easier through [DataTables.AspNet.Core](https://www.nuget.org/packages/DataTables.AspNet.Core/), which contains core elements including standard interfaces and enums.

<h3>Extensions</h3>
- [DataTables.AspNet.Extensions.AnsiSql](https://www.nuget.org/packages/DataTables.AspNet.Extensions.AnsiSql/) enables basic translation from sort and filter into ANSI-SQL `WHERE` and `ORDER BY`
- [DataTables.AspNet.Extensions.DapperExtensions](https://www.nuget.org/packages/DataTables.AspNet.Extensions.DapperExtensions/) transforms filters into `IPredicate` and sort into `ISort`

Those were the first ones because I currently use them everywhere (either plain SQL or DapperExtensions). If you'd like to see other extensions, open an issue for discussion on that. You're welcome to submit a pull request too.

<h3>Samples</h3>
Samples are provided on the `samples` folder.<br />
You can also check our wiki for guidance.

<h3>Migration path</h3>
Check a full migration path from older versions of `DataTables.AspNet` up to the most recent one.<br />
`Beta1` also delivered some breaking changes so if you're running alpha code, check the second migration path bellow:

- [Migration path from legacy DataTables.Mvc]()
- [Migration path from previous releases]()

<h3>Eager for some new code?</h3>
If you are, check out [dev](https://github.com/ALMMa/datatables.aspnet/tree/dev) branch. It has the latest code for DataTables.AspNet, including samples and more.<br />

<h3>Stable code?</h3>
We're still on beta so there is no stable code/release on `master` branch yet.<br />
As soon as we're stable, `master` branch will be populated, with full sem-ver compatibility and all.

<h3>Still legacy?</h3>
If you can, drop it!<br />
Beta1 is just as stable as legacy DataTables.Mvc and if you find any bug you can easily submit and get it fixed ASAP.

<h3>Known issues with Beta2</h3>
I has been a nightmare to make tests run properly. Since beta2 I simply can't make them run correctly so I'll rely on updated sample projects to make sure everything works fine and will rewrite tests and try to make them run again.