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
public ActionResult MyActionResult([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
{
    // do your stuff...
	var paged = myFilteredData.Skip(requestModel.Start).Take(requestModel.Length);
    return View(new DataTablesResponse(requestModel.Draw, paged, myFilteredData.Count(), myOriginalDataSet.Count()));
}

// Or if you'd like to return a JsonResult, try this:

public JsonResult MyActionResult([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
{
    // do your stuff...
	var paged = myFilteredData.Skip(requestModel.Start).Take(requestModel.Length);
	return Json(new DataTablesResponse(requestModel.Draw, paged, myFilteredData.Count(), myOriginalDataSet.Count()));
}
```
<h3>What about filtering/ordering?</h3>
<p>
	It's a no brainer too.
</p>
<p>
	Filter/sort info from each column is, well, included on each column.
</p>
<p>
	To help you out, there are two methods on <code>ColumnCollection</code>:<br />
	<code>IDataTablesRequest.Columns.GetSortedColumns()</code> will return an ordered enumeration of sorted columns.<br />
	<code>IDataTablesRequest.Columns.GetFilteredColumns()</code> will return an enumeration of columns which were actually filtered on client-side.
</p>
<p>
	Sample:
</p>
```C#
// Apply filter to your dataset based only on the columns that actually have a search value.
var filteredColumns = requestParameters.Columns.GetFilteredColumns();
foreach(var column in filteredColumns)
    Filter(column.Data, column.Search.Value, column.Search.IsRegexValue);

	
// Set your dataset on the same order as requested from client-side either directly on your SQL code or easily
// into any type or enumeration.
var sortedColumns = requestParameters.Columns.GetSortedColumns();
var isSorted = false;
foreach(var column in sortedColumns)
{
    if (!isSorted) { Sort(column.Data, column.SortDirection); isSorted = true; }
    else { SortAgain(column.Data, column.SortDirection); }
}
```
<h3>Is it possible to add custom parameters sent with my request?</h3>
<p>
	Sure! It's a piece of cake now. Override <code>BindModel</code> and make it call <code>Bind</code> with your custom implementation of <code>IDataTablesRequest</code>.<br />
	Than, override the <code>MapAditionalProperties</code> to map your extra info into your custom type.<br />
	Here's a sample:
</p>
```C#
// Create a custom type from DataTablesBinder.
public class MyBinder : DataTablesBinder
{
    // Override the default BindModel called by ASP.NET and make it call Bind passing the type of your
    // implementation of IDataTablesRequest:
    public override object BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
    {
        return Bind(controllerContext, bindingContext, typeof(MyCustomRequest));
    }
	
	// Override MapAditionalProperties so you can set your aditional data into the model:
    protected override void MapAditionalColumns(IDataTablesRequest requestModel, System.Collections.Specialized.NameValueCollection requestParameters)
    {
            var myModel = (MyCustomRequest)requestModel;
            myModel.MyCustomProp = Get<string>(requestParameters, "myCustomProp");
    }
}

// You'll need a custom request model, of course.
// Just derive from DefaultDataTablesRequest and you're fine :)
// You can choose to implement IDataTablesRequest too, if you like.
public class MyCustomRequest : DefaultDataTablesRequest
{
    public string MyCustomProp { get; set; }
}

// Than, on your controller/action, decorate with:
public ActionResult MyActionResult([ModelBinder(typeof(MyBinder))] MyCustomRequest requestModel)
```
<h3>Any issues?</h3>
<p>
	If you do find any issues, please, submit then and I'll fix it ASAP.
</p>