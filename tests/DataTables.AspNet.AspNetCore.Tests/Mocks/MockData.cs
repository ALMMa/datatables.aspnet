using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace DataTables.AspNet.AspNetCore.Tests.Mocks
{
    public class MockData
    {
        [JsonPropertyName("field")]
        [JsonProperty("field")]
        public string Field { get; set; }

        public MockData() : this(Guid.NewGuid().ToString()) { }
        public MockData(string field) { Field = field; }
    }
}
