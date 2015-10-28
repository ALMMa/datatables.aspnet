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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNet.Core;

namespace DataTables.AspNet.Extensions.AnsiSql
{
    public static class AnsiSql_Extensions
    {
		/// <summary>
		/// Parses filter info from a single column into a partial SQL-ANSI WHERE clause.
		/// </summary>
		/// <param name="column">The column to parse.</param>
		/// <returns>The partial filter string to apply or an empty string.</returns>
        public static string GetFilter(this IColumn column)
        {
            if (column == null) return String.Empty;
            if (!column.IsSearchable || column.Search == null) return String.Empty;
            if (column.Search.IsRegex) return String.Empty;
            
            return String.Format("{0} LIKE {1}",
                column.Field,
                column.Search.Value);
        }
		/// <summary>
		/// Parses filter info from a collection of columns into a SQL-ANSI WHERE clause.
		/// </summary>
		/// <param name="columns">The column collection to parse.</param>
		/// <returns>The filter string to apply or an empty filter.</returns>
		public static string GetFilter(this IEnumerable<IColumn> columns) { return columns.GetFilter(true); }
		/// <summary>
		/// Parses filter info from a collection of columns into a SQL-ANSI WHERE clause.
		/// </summary>
		/// <param name="columns">The column collection to parse.</param>
		/// <param name="includeWhere">Whether to prepend the 'WHERE' clause on the resulting string.</param>
		/// <returns>The filter string to apply or an empty filter.</returns>
		public static string GetFilter(this IEnumerable<IColumn> columns, bool includeWhere)
        {
            if (columns == null || !columns.Any()) return String.Empty;

            var wheres = columns
                .Select(_column => _column.GetFilter())
                .Where(_w => !String.IsNullOrWhiteSpace(_w));

            var result = String.Empty;
            if (wheres.Any())
            {
                if (includeWhere) result += "WHERE ";
                result += String.Join(" AND ", wheres);
            }
            return result;
        }
		/// <summary>
		/// Parses sorting info from a single column into a partial SQL-ANSI ORDER-BY clause.
		/// </summary>
		/// <param name="column">The column to parse.</param>
		/// <returns>The partial sort string to apply or an empty string.</returns>
        public static string GetSort(this IColumn column)
        {
            if (column == null) return String.Empty;
            if (!column.IsSortable || column.Sort == null) return String.Empty;

            return String.Format("{0}{1}",
                column.Field,
                column.Sort.Direction == SortDirection.Ascending
                    ? ""
                    : " DESC");
        }
		/// <summary>
		/// Parses sorting info from a collection of columns into a single SQL-ANSI ORDER-BY clause.
		/// </summary>
		/// <param name="columns">The column collection to parse.</param>
		/// <returns>The sort string to apply or an empty string.</returns>
        public static string GetSort(this IEnumerable<IColumn> columns)
        {
            if (columns == null || !columns.Any()) return String.Empty;

            var sorts = columns
                .Select(_column => _column.GetSort())
                .Where(_s => !String.IsNullOrWhiteSpace(_s));

            var result = String.Empty;
            if (sorts.Any())
            {
                result = String.Join(", ", sorts);
            }
            return result;
        }
    }
}
