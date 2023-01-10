﻿#region Copyright

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

using DataTables.AspNet.Core.NameConvention;

namespace DataTables.AspNet.Mvc5.NameConvention
{
    /// <summary>
    /// Represents CamelCase request naming convention for DataTables.AspNet.Mvc5.
    /// </summary>
    public class CamelCaseRequestNameConvention : IRequestNameConvention
    {
        public string Draw
        { get { return "draw"; } }

        public string Start
        { get { return "start"; } }

        public string Length
        { get { return "length"; } }

        public string SearchValue
        { get { return "search[value]"; } }

        public string IsSearchRegex
        { get { return "search[regex]"; } }

        public string SortField
        { get { return "order[{0}][column]"; } }

        public string SortDirection
        { get { return "order[{0}][dir]"; } }

        public string ColumnField
        { get { return "columns[{0}][data]"; } }

        public string ColumnName
        { get { return "columns[{0}][name]"; } }

        public string IsColumnSearchable
        { get { return "columns[{0}][searchable]"; } }

        public string IsColumnSortable
        { get { return "columns[{0}][orderable]"; } }

        public string ColumnSearchValue
        { get { return "columns[{0}][search][value]"; } }

        public string IsColumnSearchRegex
        { get { return "columns[{0}][search][regex]"; } }

        public string SortAscending
        { get { return "asc"; } }

        public string SortDescending
        { get { return "desc"; } }
    }
}