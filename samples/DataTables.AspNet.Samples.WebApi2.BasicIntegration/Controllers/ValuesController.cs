using DataTables.AspNet.Core;
using DataTables.AspNet.WebApi2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace DataTables.AspNet.Samples.WebApi2.BasicIntegration.Controllers
{
    public class ValuesController : ApiController
    {
		/// <summary>
		/// This is your data method.
		/// DataTables will query this (HTTP GET) to fetch data to display.
		/// </summary>
		/// <param name="request">
		/// This represents your DataTables request.
		/// It's automatically binded using the default binder and settings.
		/// 
		/// You should use IDataTablesRequest as your model, to avoid unexpected behavior and allow
		/// custom binders to be attached whenever necessary.
		/// </param>
		/// <returns>
		/// Return data here, with a json-compatible result.
		/// </returns>
		public JsonResult<IDataTablesResponse> Get(IDataTablesRequest request)
		{
			// Nothing important here. Just creates some mock data.
			var data = Models.SampleEntity.GetSampleData();

			// Global filtering.
			// Filter is being manually applied due to in-memmory (IEnumerable) data.
			// If you want something rather easier, check IEnumerableExtensions Sample.
			var filteredData = data.Where(_item => _item.Name.Contains(request.Search.Value));

			// Paging filtered data.
			// Paging is rather manual due to in-memmory (IEnumerable) data.
			var dataPage = filteredData.Skip(request.Start).Take(request.Length);

			// Response creation. To create your response you need to reference your request, to avoid
			// request/response tampering and to ensure response will be correctly created.
			var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

			// Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
			// response to a json-compatible content, so DataTables can read it when received.
			return new DataTablesJsonResult(response, Request);
		}
    }
}
