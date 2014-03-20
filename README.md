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
    return new DataTablesResponse(requestModel.Draw, paged, myFilteredData.Count(), myOriginalDataSet.Count());
}

// Or if you'd like to return a JsonResult, try this:

public JsonResult MyActionResult([ModelBinder(typeof(DataTablesBinder)] IDataTablesRequest requestModel)
{
    // do your stuff...
	var paged = myFilteredData.Skip(requestModel.Start).Take(requestModel.Length);
	return Json(new DataTablesResponse(requestModel.Draw, paged, myFilteredData.Count(), myOriginalDataSet.Count()));
}
```
<h3>Any gotchas?</h3>
<p>
	There is one. Simple but tricky.
</p>
<p>
	When sorting on client-side, DataTables send a request to your server to order by the selected columns. It defaults to the first column but you can change that.<br />
</p>
<p>
	The problem comes on the server-side when dealing with IQueryable, IEnumerable and IList elements.
	DataTables provides you with the sorting column indexes and directions but we don't have a method to order an IQueryable, IEnumerable or IList using the index of the field.
</p>
<p>
	Also, consider that you have an unbound column (model: null). You won't find a way to order that column on server-side (but you might want to order through other columns).
</p>
<p>
	So, you'll have work with 3 elements: <code>Column.IsOrdered</code>, <code>Column.OrderNumber</code> and <code>Column.SortDirection</code>.
</p>
<p>
	Tips:
</p>
```C#
var columns = requestParameters.Columns.Where(_column => _column.IsOrdered && !String.IsNullOrWhiteSpace(_column.Data));
if (columns.Any())
{
    var sortedColumns = columns.OrderBy(_column => _column.OrderNumber);
    var isSorted = false;
    foreach(var column in sortedColumns)
    {
        if (!isSorted) { Sort(column.Data, column.SortDirection); isSorted = true; }
        else { SortAgain(column.Data, column.SortDirection); }
    }
}
```