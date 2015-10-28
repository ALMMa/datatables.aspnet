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

using Xunit;

namespace DataTables.AspNet.Mvc5.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.Mvc5 'Options' class.
    /// </summary>
    public class OptionsTests
    {
        /// <summary>
        /// Validates default configuration options.
        /// </summary>
        [Fact]
        public void DefaultConfiguration()
        {
            // Arrange
            var options = TestHelper.MockOptions();

            // Assert
            Assert.NotNull(options);
            Assert.Equal(10, options.DefaultPageLength);
            Assert.Equal(false, options.IsRequestAdditionalParametersEnabled);
            Assert.Equal(false, options.IsResponseAdditionalParametersEnabled);
            Assert.Equal(true, options.IsDrawValidationEnabled);
            Assert.Equal(new NameConvention.CamelCaseRequestNameConvention().Draw, options.RequestNameConvention.Draw);
            Assert.Equal(new NameConvention.CamelCaseResponseNameConvention().Draw, options.ResponseNameConvention.Draw);
        }
        /// <summary>
        /// Validates switching name convention to HungarianNotation.
        /// </summary>
        [Fact]
        public void SwitchHungarianNotation()
        {
            // Arrange
            var options = TestHelper.MockOptions();

            // Act
            options.UseHungarianNotation();

            // Assert
            Assert.Equal(new NameConvention.HungarianNotationRequestNameConvention().Draw, options.RequestNameConvention.Draw);
            Assert.Equal(new NameConvention.HungarianNotationResponseNameConvention().Draw, options.ResponseNameConvention.Draw);
        }
        /// <summary>
        /// Validates switching name convention to CamelCase.
        /// </summary>
        [Fact]
        public void SwitchCamelCase()
        {
            // Arrange
            var options = TestHelper.MockOptions();

            // Act
            options.UseHungarianNotation().UseCamelCase();

            // Assert
            Assert.Equal(new NameConvention.CamelCaseRequestNameConvention().Draw, options.RequestNameConvention.Draw);
            Assert.Equal(new NameConvention.CamelCaseResponseNameConvention().Draw, options.ResponseNameConvention.Draw);
        }
        /// <summary>
        /// Validates disabling request draw validation.
        /// </summary>
        [Fact]
        public void DisableDrawValidation()
        {
            // Arrange
            var options = TestHelper.MockOptions();

            // Act
            options.DisableDrawValidation();

            // Assert
            Assert.Equal(false, options.IsDrawValidationEnabled);
        }
        /// <summary>
        /// Validates enabling request draw validation.
        /// </summary>
        [Fact]
        public void EnableDrawValidation()
        {
            // Arrange
            var options = TestHelper.MockOptions();

            // Act
            options.DisableDrawValidation().EnableDrawValidation();

            // Assert
            Assert.Equal(true, options.IsDrawValidationEnabled);
        }
        /// <summary>
        /// Validates enabling aditional parameters verification.
        /// </summary>
        [Fact]
        public void EnableAditionalParameters()
        {
            // Arrange
            var options = TestHelper.MockOptions();

            // Act
            options.EnableRequestAdditionalParameters();
            options.EnableResponseAdditionalParameters();

            // Assert
            Assert.Equal(true, options.IsRequestAdditionalParametersEnabled);
            Assert.Equal(true, options.IsResponseAdditionalParametersEnabled);
        }
        /// <summary>
        /// Validates disabling aditional parameters verification.
        /// </summary>
        [Fact]
        public void DisableAditionalParameters()
        {
            // Arrange
            var options = TestHelper.MockOptions();

            // Act
            options.EnableRequestAdditionalParameters().DisableRequestAdditionalParameters();
            options.EnableResponseAdditionalParameters().DisableResponseAdditionalParameters();

            // Assert
            Assert.Equal(false, options.IsRequestAdditionalParametersEnabled);
            Assert.Equal(false, options.IsResponseAdditionalParametersEnabled);
        }
        /// <summary>
        /// Validates changing default page length.
        /// </summary>
        [Fact]
        public void ChangeDefaultPageLength()
        {
            // Arrange
            var options = TestHelper.MockOptions();

            // Act
            options.SetDefaultPageLength(123);

            // Assert
            Assert.Equal(123, options.DefaultPageLength);
        }
    }
}
