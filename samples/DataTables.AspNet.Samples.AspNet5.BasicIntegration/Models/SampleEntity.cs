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

namespace DataTables.AspNet.Samples.AspNet5.BasicIntegration.Models
{
    public class SampleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public SampleEntity() { }
        public SampleEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }



        private static IEnumerable<SampleEntity> SampleData;
        public static IEnumerable<SampleEntity> GetSampleData()
        {
            if (SampleEntity.SampleData == null)
            {
                var _data = new List<SampleEntity>(53);

                var random = new Random();
                for(int i = 0; i < 53; i++)
                {
                    var letter1 = random.Next(65, 91);
                    var letter2 = random.Next(65, 91);
                    var letter3 = random.Next(65, 91);
                    var letter4 = random.Next(65, 91);

                    var sampleEntity = new SampleEntity(i + 1, new string(new[] { (char)letter1, (char)letter2, (char)letter3, (char)letter4 }));
                    _data.Add(sampleEntity);
                }

                SampleEntity.SampleData = _data;
            }

            return SampleEntity.SampleData;
        }
    }
}
