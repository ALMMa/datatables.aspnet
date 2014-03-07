<h1>Datatables.Mvc</h1>
<p>
	ASP.NET MVC 5 classes to bind DataTables (1.10 and above with the new camelCase API) with your controllers.
</p>
<h3>But why another project for this?</h3>
<p>
	First, it's not just another project. This one provides binding for ASP.NET MVC with DataTables 1.10.
</p>
<p>
	Second, it's all about the new DataTables API (camelCase).<br />
	So far, DataTables was using the <a href='http://en.wikipedia.org/wiki/Hungarian_notation'>Hungarian Notation</a> and that was, for me, a major pain. It all comes down to a developer's choice.
</p>
<p>
	Finally, there was no binder, so far, for this scenario. 
	Of course we could simply get the request parameters manually (after all, that's what the DataTables.Mvc is doing, right?) but this should help you avoid type-casting parameters, detecting, filtering and more. 
	Also, this should help your focus on business rules instead of HTTP parameter checking.
</p>
<h3>What about demo?</h3>
<p>
	It's as simple as it gets:
</p>

<pre>
public ActionResult MyActionResult([Bind(typeof(DataTables.Mvc.DataTablesBinder)] DataTables.Mvc.IDataTablesRequest requestModel) { ... }
</pre>

<pre>
return new DataTables.Mvc.DataTablesResponse(requestModel.Draw, myFilteredData.Skip(requestModel.Start).Take(requestModel.Length), myFilteredData.Count(), myOriginalDataSet.Count());
</pre>