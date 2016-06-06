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

using DataTables.AspNet.Core.NameConvention;

namespace DataTables.AspNet.AspNetCore.NameConvention
{
    /// <summary>
    /// Represents HungarianNotation request naming convention for DataTables.AspNet.AspNetCore.
    /// </summary>
    public class HungarianNotationRequestNameConvention : IRequestNameConvention
    {
        public string ColumnField { get { return "mDataProp_{0}"; } }
        public string ColumnName { get { return "{0}"; } }
        public string IsColumnSortable { get { return "bSortable_{0}"; } }
        public string IsColumnSearchable { get { return "bSearchable_{0}"; } }
        public string IsColumnSearchRegex { get { return "bRegex_{0}"; } }
        public string ColumnSearchValue { get { return "sSearch_{0}"; } }
        public string Draw { get { return "sEcho"; } }
        public string Length { get { return "iDisplayLength"; } }
        public string IsSearchRegex { get { return "bRegex"; } }
        public string SearchValue { get { return "sSearch"; } }
        public string SortDirection { get { return "sSortDir_{0}"; } }
        public string SortField { get { return "sSortCol_{0}"; } }
        public string Start { get { return "iDisplayStart"; } }
        public string SortAscending { get { return "asc"; } }
        public string SortDescending { get { return "desc"; } }
    }
}
