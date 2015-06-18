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
using System.Reflection;
using DapperExtensions;

namespace DataTables.AspNet.Extensions.DapperExtensions
{
    /// <summary>
    /// Provides extension methods to simplify DataTables integration with DapperExtensions.
    /// </summary>
    public static class DapperExtensions_Extensions
    {
        /// <summary>
        /// Transforms a DataTables search object into a DapperExtensions field predicate.
        /// Important: regex search is not supported.
        /// </summary>
        /// <typeparam name="TElement">The type of corresponding entity.</typeparam>
        /// <param name="search">The search object to convert.</param>
        /// <returns>The field predicate for the specified type.</returns>
        public static IPredicate GetFilterPredicate<TElement>(this Core.ISearch search) where TElement : class
        {
            return search.GetFilterPredicate<TElement>(false);
        }
        /// <summary>
        /// Transforms a DataTables search object into a DapperExtensions field predicate.
        /// Important: regex search is not supported.
        /// </summary>
        /// <typeparam name="TElement">The type of corresponding entity.</typeparam>
        /// <param name="search">The search object to convert.</param>
        /// <param name="forceEqualsOperator">Forces '==' operator for string properties.</param>
        /// <returns>The field predicate for the specified type.</returns>
        public static IPredicate GetFilterPredicate<TElement>(this Core.ISearch search, bool forceEqualsOperator) where TElement : class
        {
            if (search == null) return null;

            if (search.IsRegex) return null;

            // Scaffolds type and searches for member (field) name.
            var typeSearchResult = TypeSearchResult.Scaffold<TElement>(search.Field);

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

            return new FieldPredicate<TElement>() { PropertyName = search.Field, Operator = _operator, Value = search.Value };
        }
        /// <summary>
        /// Transforms a DataTables sort object into a DapperExtensions sort element.
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static ISort GetSortPredicate<TElement>(this Core.ISort sort)
        {
            if (sort == null) return null;

            // Scaffolds type and searches for member (field) name.
            var typeSearchResult = TypeSearchResult.Scaffold<TElement>(sort.Field);

            // Type does not contains member - returns a null sort to ensure compliance.
            if (!typeSearchResult.ContainsMember) return null;

            return new Sort() { Ascending = sort.Direction == Core.SortDirection.Ascending, PropertyName = sort.Field };
        }
        /// <summary>
        /// Transforms a DataTables sort collection into a DapperExtensions sort list.
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static IList<ISort> GetSortPredicate<TElement>(this IEnumerable<Core.ISort> sort)
        {
            if (sort == null) return null;

            return sort
                .OrderBy(_sort => _sort.Order)
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
                var result = new TypeSearchResult();
                result.ContainsMember = false;
                result.IsStringProperty = false;

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
