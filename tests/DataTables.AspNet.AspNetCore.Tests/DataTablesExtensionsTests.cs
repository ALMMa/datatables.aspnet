using DataTables.AspNet.AspNetCore.Tests.Mocks;
using Xunit;

namespace DataTables.AspNet.AspNetCore.Tests
{
    public class DataTablesExtensionsTests
    {
        /// <summary>
        /// Validates error response creation without aditional parameters.
        /// </summary>
        [Fact]
        public void ErrorResponseWithoutAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);

            // Act
            var response = request.CreateResponse<MockData>("just_some_error_message");

            // Assert
            Assert.NotNull(response);
        }
        /// <summary>
        /// Validates error response creation with aditional parameters.
        /// </summary>
        [Fact]
        public void ErrorResponseWithAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var aditionalParameters = TestHelper.MockAdditionalParameters();

            // Act
            var response = request.CreateResponse<MockData>("just_some_error_message", aditionalParameters);

            // Assert
            Assert.NotNull(response);
        }
        /// <summary>
        /// Validates response creation without aditional parameters.
        /// </summary>
        [Fact]
        public void ResponseWithoutAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();

            // Act
            var response = request.CreateResponse<MockData>(2000, 1000, data);

            // Assert
            Assert.NotNull(response);
        }
        /// <summary>
        /// Validates response creation with aditional parameters dictionary.
        /// </summary>
        [Fact]
        public void ResponseWithAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();
            var aditionalParameters = TestHelper.MockAdditionalParameters();

            // Act
            var response = request.CreateResponse<MockData>(2000, 1000, data, aditionalParameters);

            // Assert
            Assert.NotNull(response);
        }
        /// <summary>
        /// Validates response response creation for invalid (null) request.
        /// </summary>
        [Fact]
        public void NullRequestResponseCreation()
        {
            // Arrange
            Core.IDataTablesRequest request = null;

            // Act
            var response = request.CreateResponse<MockData>("just_some_error_message");

            // Assert
            Assert.Null(response);
        }
        /// <summary>
        /// This test must be executed alone.
        /// Validates response creation for request with invalid draw value and draw validtion enabled.
        /// </summary>
        [Fact]
        public void InvalidDrawResponseCreationWithDrawValidation()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(0, 13, 99, null, null);
            var data = TestHelper.MockData();
            Configuration.Options.EnableDrawValidation();

            // Act
            var response = request.CreateResponse<MockData>(2000, 1000, data);

            // Assert
            Assert.Null(response);
        }
        /// <summary>
        /// This test must be executed alone.
        /// Validates response creation for request with invalid draw value and without draw validtion enabled.
        /// </summary>
        [Fact]
        public void InvalidDrawResponseCreationWithoutDrawValidation()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(0, 13, 99, null, null);
            var data = TestHelper.MockData();
            Configuration.Options.DisableDrawValidation();

            // Act
            var response = request.CreateResponse<MockData>(2000, 1000, data);

            // Assert
            Assert.NotNull(response);
        }
    }
}
