using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
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
        public static IActionResult GetActionResult<TDataType>(this IDataTablesRequest request, string errorMessage) =>
            request.GetActionResult<TDataType>(errorMessage, null);

        /// <summary>
        /// Creates a DataTables response object.
        /// </summary>
        /// <param name="request">The DataTables request object.</param>
        /// <param name="errorMessage">Error message to send back to client-side.</param>
        /// <param name="additionalParameters">Aditional parameters dictionary.</param>
        /// <returns>A DataTables response object.</returns>
        public static IActionResult GetActionResult<TDataType>(this IDataTablesRequest request, string errorMessage, IDictionary<string, object> additionalParameters) =>
            new DataTablesJsonResponse<TDataType>(DataTablesResponse<TDataType>.Create(request, errorMessage, additionalParameters));

        /// <summary>
        /// Creates a DataTables response object.
        /// </summary>
        /// <param name="request">The DataTables request object.</param>
        /// <param name="totalRecords">Total records count (total available non-filtered records on database).</param>
        /// <param name="totalRecordsFiltered">Total filtered records (total available records after filtering).</param>
        /// <param name="data">Data object (collection).</param>
        /// <returns>A DataTables response object.</returns>
        public static IActionResult GetActionResult<TDataType>(this IDataTablesRequest request, int totalRecords, int totalRecordsFiltered, IEnumerable<TDataType> data) =>
            request.GetActionResult<TDataType>(totalRecords, totalRecordsFiltered, data, null);

        /// <summary>
        /// Creates a DataTables response object.
        /// </summary>
        /// <param name="request">The DataTables request object.</param>
        /// <param name="totalRecords">Total records count (total available non-filtered records on database).</param>
        /// <param name="totalRecordsFiltered">Total filtered records (total available records after filtering).</param>
        /// <param name="data">Data object (collection).</param>
        /// <param name="additionalParameters">Adicional parameters dictionary.</param>
        /// <returns>A DataTables response object.</returns>
        public static IActionResult GetActionResult<TDataType>(this IDataTablesRequest request, int totalRecords, int totalRecordsFiltered, IEnumerable<TDataType> data, IDictionary<string, object> additionalParameters) =>
            new DataTablesJsonResult<TDataType>(DataTablesResponse<TDataType>.Create(request, totalRecords, totalRecordsFiltered, data, additionalParameters));
    }
}
