<h1>Datatables.AspNet</h1>
<p>
	Formely known as <strong>DataTables.Mvc</strong>, this project started over a year ago with small objectives, aiming to provide intermediate and experienced developers a tool to avoid the boring process of handling DataTables parameters.
</p>
<p>
	More than 15 months later, we now face new issues: need for a modular system that can be attached to either Mvc or WebApi project, support for AspNet 5, NuGet package, test suite, helper classes and more.
</p>
<h3>New techs, new project, new objectives</h3>
<p>
	Renaming was the first step. Cleaning legacy code (kept now under legacy branch), tagging a final release (1.2) and openning space for a new project is now in place.<br />
	Idea now is to provide a modular system (multiple projects, multiple nuget packages) so that you can use just what you want to use, instead of getting the full stack to use, let's say, just a binder.<br />
	Also, with new techs on the way (Asp.Net 5, just for a start), DataTables.Mvc simply got old. And old code smells.
</p>
<h3>Testing</h3>
<p>
	This was another problem with old DataTables.Mvc.<br />
	Framework was not built with testing in mind and that's why there was no testing at all. No way to simply attach tests (private obscure methods everywhere) and that's not really good for mid-to-large apps either, right?<br />
	With the new DataTables.AspNet code, testing will be a priority and TDD will be used right from the start, to avoid unexpected behavior and code breaking on <strong>your</strong> app.
</p>
<h3>Want to use existing code?</h3>
<p>
	If you do, check legacy branch, along with releases (zip). They won't be "oficially" released under nuget simply because they will be discontinued.<br />
	By the time version 2.0 reaches alpha-1, NuGet package will be fully available.
</p>
<h3>Migration path</h3>
<p>
	Don't worry.<br />
	Although you'll need to change existing code, by the time DataTables.AspNet reaches beta a full migration path will be released to help with existing code.<br />
	I've chosen <strong>beta</strong> as the "due date" to a migration path because during <i>alpha</i> code might change a lot and building a migration path for that would just be hell.
</p>