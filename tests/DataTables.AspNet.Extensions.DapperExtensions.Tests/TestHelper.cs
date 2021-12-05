using DataTables.AspNet.Core;

namespace DataTables.AspNet.Extensions.DapperExtensions.Tests
{
    public static class TestHelper
    {
        public static IColumn MockColumn(string columnName, string columnField, bool searchable, bool sortable, string searchValue, bool searchRegex)
        { return new Column(columnName, columnField, searchable, sortable, TestHelper.MockSearch(searchValue, searchRegex)); }
        public static IOptions MockOptions()
        { return new Options(); }
        public static ISearch MockSearch(string searchValue, bool isRegex)
        { return new Search(searchValue, isRegex); }
        public static ISort MockSort(int order, string direction)
        { return new Sort(order, direction); }
    }



    public class SampleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
