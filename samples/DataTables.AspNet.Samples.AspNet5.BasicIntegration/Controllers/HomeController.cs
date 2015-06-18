#region Copyright
/* The MIT License (MIT)

Copyright (c) 2014 Anderson Luiz Mendes Matos (Brazil)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion Copyright

using System.Linq;
using DataTables.AspNet.AspNet5;
using Microsoft.AspNet.Mvc;

namespace DataTables.AspNet.Samples.AspNet5.BasicIntegration.Controllers
{
    public class HomeController : Controller
    {
        // This is your basic view method.
        // Nothing is done here besides view returning.
        public IActionResult Index()
        {
            return View();
        }



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
        public IActionResult PageData(Core.IDataTablesRequest request)
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
            return new DataTablesJsonResult(response, true);
        }
    }
}
