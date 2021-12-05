using System;
using System.Collections.Generic;
using System.Linq;
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
