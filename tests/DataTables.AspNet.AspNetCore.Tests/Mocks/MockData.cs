using System;

namespace DataTables.AspNet.AspNetCore.Tests.Mocks
{
    public class MockData
    {
        public string Field { get; set; }

        public MockData() : this(Guid.NewGuid().ToString()) { }
        public MockData(string field) { Field = field; }
    }
}
