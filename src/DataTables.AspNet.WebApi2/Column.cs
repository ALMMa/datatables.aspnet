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

using DataTables.AspNet.Core;

namespace DataTables.AspNet.WebApi2
{
    /// <summary>
    /// Represents a DataTables column.
    /// </summary>
    public class Column : IColumn
    {
        public string Field { get; private set; }
        public string Name { get; private set; }
        public ISearch Search { get; private set; }
        public bool IsSearchable { get; private set; }
        public ISort Sort { get; private set; }
        public bool IsSortable { get; private set; }


        public Column(string name, string field, bool searchable, bool sortable, ISearch search)
        {
            Name = name;
            Field = field;
            IsSortable = sortable;
                        
            IsSearchable = searchable;
            if (!IsSearchable) Search = null;
            else Search = search ?? new Search(field);
        }


        public bool SetSort(int order, string direction)
        {
            if (!IsSortable) return false;

            Sort = new Sort(Field, order, direction);
            return true;
        }
    }
}
