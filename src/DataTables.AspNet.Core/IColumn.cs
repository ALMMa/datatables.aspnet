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

namespace DataTables.AspNet.Core
{
    /// <summary>
    /// Defines DataTables column members.
    /// </summary>
    public interface IColumn
    {
        /// <summary>
        /// Gets column name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets column field.
        /// </summary>
        string Field { get; }
        /// <summary>
        /// Gets column searchable indicator.
        /// </summary>
        bool IsSearchable { get; }
        /// <summary>
        /// Gets column search definition or null if column is not searchable.
        /// </summary>
        ISearch Search { get; }
        /// <summary>
        /// Gets column sortable indicator.
        /// </summary>
        bool IsSortable { get; }
        /// <summary>
        /// Gets column sort definition or null if column is not sortable.
        /// </summary>
        ISort Sort { get; }



        /// <summary>
        /// Sets column sort definition.
        /// </summary>
        /// <param name="order">Sort order.</param>
        /// <param name="direction">Sort direction.</param>
        /// <returns>True if sort could be set, False otherwise.</returns>
        bool SetSort(int order, string direction);
    }
}
