<h1>Datatables.Mvc</h1>
==============
<p>
	ASP.NET MVC 5 classes to bind your DataTables (1.10 and above with the new camelCaseApi) with your controllers.
</p>
<h3>But why another project for this?</h3>
<p>
	First, it's not just another project. This one provides binding for ASP.NET MVC with DataTables 1.10.
</p>
<p>
	Second, it's all about the new DataTables interface (camelCase).<br />
	So far, DataTables was using the HungarianNotation and that was, for me, a major pain. It all comes down to developer's choice and that wasn't mine at all.
</p>
<p>
	Finally, there was no binder, so far, for this cenario. Of course we could simple get the request parameters but this should help you avoid type-casting parameters, detecting, filtering and more.
</p>
<h3>What about demo?</h3>
<p>
	It's as simple as it gets:
</p>
<blockquote>
	<pre>
public ActionResult MyActionResult([Bind(typeof(DataTables.Mvc.DataTablesBinder)] DataTables.Mvc.IDataTablesRequest requestModel) { ... }
	</pre>
</blockquote>

<blockquote>
	<pre>
return new DataTables.Mvc.DataTablesResponse(requestModel.Draw, myFilteredData.Skip(requestModel.Start).Take(requestModel.Length), myFilteredData.Count(), myOriginalDataSet.Count());
	</pre>
</blockquote>