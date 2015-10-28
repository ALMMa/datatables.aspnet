<h1>Datatables.AspNet</h1>
Formely known as <strong>DataTables.Mvc</strong>, this project started over a year ago with small objectives, aiming to provide intermediate and experienced developers a tool to avoid the boring process of handling DataTables parameters.<br />
More than 15 months later, we now undergo a full rewrite with support for Mvc, WebApi and AspNet (<strong>core CLR included!</strong>).<br />
Also, unit-testing is a priority to avoid breaking <i>your</i> app.

<h3>Beta1 has just arrived!</h3>
`Beta1` release now ships with `WebApi2`, core extensions to help mapping and a few more samples to help you get started.
This is the first beta release for `DataTables.AspNet`. As promised, you can now follow a full migration path (link bellow).

<h3>NuGet packages</h3>
- [DataTables.AspNet.Mvc5](https://www.nuget.org/packages/DataTables.AspNet.Mvc5/), with support for Mvc5, registration and automatic binders
- [DataTables.AspNet.WebApi2](https://www.nuget.org/packages/DataTables.AspNet.WebApi2/) with support for WebApi2, registration and automatic binders
- [DataTables.AspNet.AspNet5](https://www.nuget.org/packages/DataTables.AspNet.AspNet5/) with support for AspNet5, dependency injection and automatic binders

You can extend DataTables.AspNet easier through [DataTables.AspNet.Core](https://www.nuget.org/packages/DataTables.AspNet.Core/), which contains core elements including standard interfaces and enums.

<h3>Samples</h3>
Samples are provided on the `samples` folder.<br />
You can also check our wiki [here](https://github.com/ALMMa/datatables.aspnet/wiki) for guidance.

<h3>Migration path</h3>
Check a full migration path from older versions of `DataTables.AspNet` up to the most recent one.<br />
`Beta1` also delivered some breaking changes so if you're running alpha code, check the second migration path bellow:

- Migration path from legacy DataTables.Mvc (TODO - Wiki)
- Migration path from alpha releases (TODO - Wiki)

<h3>Eager for some new code?</h3>
If you are, check out `dev` branch. It has the latest code for DataTables.AspNet, including samples and more.<br />
Be advised that alpha is not production-ready and your should decide carefully before adopting it for your app.

<h3>Still legacy?</h3>
If you can, drop it!<br />
Beta1 is just as stable as legacy DataTables.Mvc and if you find any bug you can easily submit and get it fixed ASAP.