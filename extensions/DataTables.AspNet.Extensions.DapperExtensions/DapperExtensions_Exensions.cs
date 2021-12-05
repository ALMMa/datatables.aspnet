using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DapperExtensions;
using DapperExtensions.Predicate;

namespace DataTables.AspNet.Extensions.DapperExtensions
{
    /// <summary>
    /// Provides extension methods to simplify DataTables integration with DapperExtensions.
    /// </summary>
    public static class DapperExtensions_Extensions
    {
        public static IPredicate GetFilterPredicate<TElement>(this Core.IDataTablesRequest request)
            where TElement : class
        {
            return request.Columns.GetFilterPredicate<TElement>(request.Search.Value);
        }

        public static IPredicate GetFilterPredicate<TElement>(this IEnumerable<Core.IColumn> columns, string searchValue)
             where TElement : class
        {
            var searchableColumns = columns.Where(x => x.IsSearchable).ToArray();
            var searchPredicates = new List<IPredicate>();

            foreach (var searchableColumn in searchableColumns)
            {
                var columnPredicate = searchableColumn.GetFilterPredicate<TElement>(searchValue);
                if (columnPredicate != null) searchPredicates.Add(columnPredicate);
            }

            if (searchPredicates.Any())
            {
                return Predicates.Group(
                    GroupOperator.Or,
                    searchPredicates.ToArray());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the DapperExtensions filter predicate for a given column, if any search is set.
        /// Important: regex search is not supported.
        /// </summary>
        /// <typeparam name="TElement">The type of corresponding entity.</typeparam>
        /// <param name="column">The column to get search information.</param>
        /// <returns>The field predicate for the specified type or null.</returns>
        public static IPredicate GetFilterPredicate<TElement>(this Core.IColumn column)
            where TElement : class
        {
            return column.GetFilterPredicate<TElement>(column.Search?.Value, false);
        }

        /// <summary>
        /// Gets the DapperExtensions filter predicate for a given column, if any search is set.
        /// Important: regex search is not supported.
        /// </summary>
        /// <typeparam name="TElement">The type of corresponding entity.</typeparam>
        /// <param name="column">The column to get search information.</param>
        /// <param name="searchValue">The value to be searched.</param>
        /// <returns>The field predicate for the specified type or null.</returns>
        public static IPredicate GetFilterPredicate<TElement>(this Core.IColumn column, string searchValue)
            where TElement : class
        {
            return column.GetFilterPredicate<TElement>(searchValue, false);
        }

        /// <summary>
        /// Gets the DapperExtensions filter predicate for a given column, if any search is set.
        /// Important: regex search is not supported.
        /// </summary>
        /// <typeparam name="TElement">The type of corresponding entity.</typeparam>
        /// <param name="column">The column to get search information.</param>
        /// <param name="forceEqualsOperator">Forces '==' operator for string properties.</param>
        /// <returns>The field predicate for the specified type or null.</returns>
        public static IPredicate GetFilterPredicate<TElement>(this Core.IColumn column, string searchValue, bool forceEqualsOperator)
            where TElement : class
        {
            if (column == null) return null;
            if (!column.IsSearchable) return null;
            if (column.Search == null) return null;

            if (column.Search.IsRegex) return null;

            // Scaffolds type and searches for member (field) name.
            var typeSearchResult = TypeSearchResult.Scaffold<TElement>(column.Field);

            // Type does not contains member - returns a null predicate to ensure compliance.
            if (!typeSearchResult.ContainsMember)
                return null;

            // By default, 'LIKE' should be used when searching string content on database.
            // You can, however, force usage of '==' operator if desired.
            var _operator = forceEqualsOperator 
                ? Operator.Eq 
                : typeSearchResult.IsStringProperty
                    ? Operator.Like
                    : Operator.Eq;

            return new FieldPredicate<TElement>() { PropertyName = column.Field, Operator = _operator, Value = searchValue };
        }
        /// <summary>
        /// Transforms a DataTables sort object into a DapperExtensions sort element.
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static ISort GetSortPredicate<TElement>(this Core.IColumn column)
        {
            if (column == null) return null;
            if (column.Sort == null) return null;

            // Scaffolds type and searches for member (field) name.
            var typeSearchResult = TypeSearchResult.Scaffold<TElement>(column.Field);

            // Type does not contains member - returns a null sort to ensure compliance.
            if (!typeSearchResult.ContainsMember) return null;

            return new Sort() { Ascending = column.Sort.Direction == Core.SortDirection.Ascending, PropertyName = column.Field };
        }
        /// <summary>
        /// Transforms a DataTables sort collection into a DapperExtensions sort list.
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static IList<ISort> GetSortPredicate<TElement>(this IEnumerable<Core.IColumn> columns)
        {
            if (columns == null) return null;
            if (!columns.Any() || !columns.Any(_c => _c.Sort != null)) return null;

            var sortColumns = columns.Where(_c => _c.Sort != null).OrderBy(_c => _c.Sort.Order);

            return sortColumns
                .Select(_sort => _sort.GetSortPredicate<TElement>())
                .Where(_sort => _sort != null)
                .ToList();
        }



        internal class TypeSearchResult
        {
            public bool ContainsMember { get; private set; }
            public bool IsStringProperty { get; private set; }



            public static TypeSearchResult Scaffold<TElement>(string propertyName)
            {
                var result = new TypeSearchResult
                {
                    ContainsMember = false,
                    IsStringProperty = false
                };

                try
                {
                    var type = typeof(TElement);
                    var memberInfo = type.GetMember(propertyName).FirstOrDefault();

                    if (memberInfo != null)
                    {
                        result.ContainsMember = true;

                        Type memberType = null;
                        if (memberInfo is FieldInfo)
                        {
                            var fieldInfo = memberInfo as FieldInfo;
                            memberType = fieldInfo.FieldType;
                        }
                        else if (memberInfo is PropertyInfo)
                        {
                            var propertyInfo = memberInfo as PropertyInfo;
                            memberType = propertyInfo.PropertyType;
                        }

                        if (memberType != null && memberType.IsEquivalentTo(typeof(string)))
                            result.IsStringProperty = true;
                    }

                    return result;
                }
                catch { return result; }
            }
        }
    }
}
