using Xunit;

namespace DataTables.AspNet.AspNetCore.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.AspNet5 'Options' class.
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
            Assert.Equal("asc", options.RequestNameConvention.SortAscending);
            Assert.Equal("desc", options.RequestNameConvention.SortDescending);
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
