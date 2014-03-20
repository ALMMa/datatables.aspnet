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

```C#
public ActionResult MyActionResult([ModelBinder(typeof(DataTablesBinder)] IDataTablesRequest requestModel)
{
    // do your stuff...
	var paged = myFilteredData.Skip(requestModel.Start).Take(requestModel.Length);
    return View(new DataTablesResponse(requestModel.Draw, paged, myFilteredData.Count(), myOriginalDataSet.Count()));
}

// Or if you'd like to return a JsonResult, try this:

public JsonResult MyActionResult([ModelBinder(typeof(DataTablesBinder)] IDataTablesRequest requestModel)
{
    // do your stuff...
	var paged = myFilteredData.Skip(requestModel.Start).Take(requestModel.Length);
	return Json(new DataTablesResponse(requestModel.Draw, paged, myFilteredData.Count(), myOriginalDataSet.Count()));
}
```
<h3>What about ordering?</h3>
<p>
	It's a no brainer too.
</p>
<p>
	Filter/sort info from each column is, well, included on each column.
</p>
<p>
	To help you out, get only the columns which were ordered on client-side with <code>IDataTablesRequest.GetSortedColumns()</code>.
	Than, iterate through then and use <code>Column.SortDirection</code> to sort your dataset.
</p>
<p>
	Sample:
</p>
```C#
var filteredColumns = requestParameters.Columns.GetFilteredColumns();
foreach(var column in filteredColumns)
    Filter(column.Data, column.Search.Value, column.Search.IsRegexValue);

var sortedColumns = requestParameters.Columns.GetSortedColumns();
var isSorted = false;
foreach(var column in sortedColumns)
{
    if (!isSorted) { Sort(column.Data, column.SortDirection); isSorted = true; }
    else { SortAgain(column.Data, column.SortDirection); }
}
```