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

using System.Collections.Generic;

namespace DataTables.AspNet.AspNetCore
{
    /// <summary>
    /// Provides extension methods for DataTables response creation.
    /// </summary>
    public static class DataTablesExtensions
    {
        /// <summary>
        /// Creates a DataTables response object.
        /// </summary>
        /// <param name="request">The DataTables request object.</param>
        /// <param name="errorMessage">Error message to send back to client-side.</param>
        /// <returns>A DataTables response object.</returns>
        public static Core.IDataTablesResponse CreateResponse(this Core.IDataTablesRequest request, string errorMessage)
        {
            return request.CreateResponse(errorMessage, null);
        }
        /// <summary>
        /// Creates a DataTables response object.
        /// </summary>
        /// <param name="request">The DataTables request object.</param>
        /// <param name="errorMessage">Error message to send back to client-side.</param>
        /// <param name="additionalParameters">Aditional parameters dictionary.</param>
        /// <returns>A DataTables response object.</returns>
        public static Core.IDataTablesResponse CreateResponse(this Core.IDataTablesRequest request, string errorMessage, IDictionary<string, object> additionalParameters)
        {
            return DataTablesResponse.Create(request, errorMessage, additionalParameters);
        }
        /// <summary>
        /// Creates a DataTables response object.
        /// </summary>
        /// <param name="request">The DataTables request object.</param>
        /// <param name="totalRecords">Total records count (total available non-filtered records on database).</param>
        /// <param name="totalRecordsFiltered">Total filtered records (total available records after filtering).</param>
        /// <param name="data">Data object (collection).</param>
        /// <returns>A DataTables response object.</returns>
        public static Core.IDataTablesResponse CreateResponse(this Core.IDataTablesRequest request, int totalRecords, int totalRecordsFiltered, object data)
        {
            return request.CreateResponse(totalRecords, totalRecordsFiltered, data, null);
        }
        /// <summary>
        /// Creates a DataTables response object.
        /// </summary>
        /// <param name="request">The DataTables request object.</param>
        /// <param name="totalRecords">Total records count (total available non-filtered records on database).</param>
        /// <param name="totalRecordsFiltered">Total filtered records (total available records after filtering).</param>
        /// <param name="data">Data object (collection).</param>
        /// <param name="additionalParameters">Adicional parameters dictionary.</param>
        /// <returns>A DataTables response object.</returns>
        public static Core.IDataTablesResponse CreateResponse(this Core.IDataTablesRequest request, int totalRecords, int totalRecordsFiltered, object data, IDictionary<string, object> additionalParameters)
        {
            return DataTablesResponse.Create(request, totalRecords, totalRecordsFiltered, data, additionalParameters);
        }
    }
}
