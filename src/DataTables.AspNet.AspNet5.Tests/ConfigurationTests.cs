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

using System.Linq;
using Microsoft.AspNet.Mvc.OptionDescriptors;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace DataTables.AspNet.AspNet5.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.AspNet5 'Configuration' class.
    /// </summary>
    public class ConfigurationTests
    {
        /// <summary>
        /// This test must be executed alone.
        /// Validates registration with default settings.
        /// </summary>
        [Fact]
        public void DefaultRegistration()
        {
            // Arrange
            var serviceCollection = new Microsoft.Framework.DependencyInjection.ServiceCollection();
            DataTables.AspNet.AspNet5.Configuration.UseDataTables(serviceCollection);
            serviceCollection.AddMvc();
            var provider = serviceCollection.BuildServiceProvider();
            var modelBinder = provider.GetRequiredServices<Microsoft.AspNet.Mvc.ModelBinding.IModelBinderProvider>().ElementAt(0).ModelBinders[0];

            // Assert
            Assert.NotNull(modelBinder);
            Assert.NotNull(modelBinder as ModelBinder);
            Assert.Null((modelBinder as ModelBinder).ParseAditionalParameters);
        }
        /// <summary>
        /// This test must be executed alone.
        /// Validate registration with custom options.
        /// </summary>
        [Fact]
        public void RegistrationWithCustomOptions()
        {
            // Arrange
            var options = TestHelper.MockOptions().UseHungarianNotation();
            var serviceCollection = new Microsoft.Framework.DependencyInjection.ServiceCollection();
            DataTables.AspNet.AspNet5.Configuration.UseDataTables(serviceCollection, options);

            // Assert
            Assert.Equal(options, DataTables.AspNet.AspNet5.Configuration.Options);
        }
        /// <summary>
        /// This test must be executed alone.
        /// Validate registration with custom model binder.
        /// </summary>
        [Fact]
        public void RegistrationWithCustomBinder()
        {
            // Arrange
            var requestBinder = TestHelper.MockModelBinder();
            var serviceCollection = new Microsoft.Framework.DependencyInjection.ServiceCollection();
            DataTables.AspNet.AspNet5.Configuration.UseDataTables(serviceCollection, requestBinder);
            serviceCollection.AddMvc();
            var provider = serviceCollection.BuildServiceProvider();
            var modelBinder = provider.GetRequiredServices<Microsoft.AspNet.Mvc.ModelBinding.IModelBinderProvider>().ElementAt(0).ModelBinders[0];

            // Assert
            Assert.Equal(requestBinder, modelBinder);
        }
        /// <summary>
        /// This test must be executed alone.
        /// Validate registration with custom parser function for aditional parameters.
        /// </summary>
        [Fact]
        public void RegistrationWithParseAditionalParameters()
        {
            // Arrange
            var serviceCollection = new Microsoft.Framework.DependencyInjection.ServiceCollection();
            DataTables.AspNet.AspNet5.Configuration.UseDataTables(serviceCollection, TestHelper.ParseAditionalParameters, true);
            serviceCollection.AddMvc();
            var provider = serviceCollection.BuildServiceProvider();
            var modelBinder = provider.GetRequiredServices<Microsoft.AspNet.Mvc.ModelBinding.IModelBinderProvider>().ElementAt(0).ModelBinders[0];

            // Assert
            Assert.Equal(true, DataTables.AspNet.AspNet5.Configuration.Options.IsRequestAditionalParametersEnabled);
            Assert.Equal(true, DataTables.AspNet.AspNet5.Configuration.Options.IsResponseAditionalParametersEnabled);
            Assert.NotNull((modelBinder as ModelBinder).ParseAditionalParameters);
        }
    }
}
