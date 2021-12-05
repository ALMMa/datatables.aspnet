using System;
using System.Collections.Generic;
using System.Linq;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace DataTables.AspNet.AspNetCore.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.AspNet5 'Configuration' class.
    /// </summary>
    public class ConfigurationTests
    {
        [Fact]
        public void DefaultRegistration()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.RegisterDataTables();

            // Assert
			var provider = serviceCollection.BuildServiceProvider();
            var mvcOptions = provider.GetRequiredService<IOptions<MvcOptions>>();
            Assert.NotEmpty(mvcOptions.Value.ModelBinderProviders);

            var x = new DefaultModelMetadataProvider(new DefaultCompositeMetadataDetailsProvider(null));
            var metadata = x.GetMetadataForType(typeof(IDataTablesRequest));

            var mockModelBinderProviderContext = new Mock<ModelBinderProviderContext>();
            mockModelBinderProviderContext
                .Setup(x => x.Metadata)
                .Returns(metadata);

            var modelBinder = mvcOptions.Value.ModelBinderProviders[0];
            Assert.NotNull(modelBinder);
            var binder = modelBinder.GetBinder(mockModelBinderProviderContext.Object);
            Assert.NotNull(binder);
            Assert.IsType<ModelBinder>(binder);
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
            var serviceCollection = new ServiceCollection();
            DataTables.AspNet.AspNetCore.Configuration.RegisterDataTables(serviceCollection, options);

            // Assert
            Assert.Equal(options, DataTables.AspNet.AspNetCore.Configuration.Options);
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
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.RegisterDataTables(requestBinder);

            // Assert
            var provider = serviceCollection.BuildServiceProvider();
            var mvcOptions = provider.GetRequiredService<IOptions<MvcOptions>>();
            Assert.NotEmpty(mvcOptions.Value.ModelBinderProviders);

            var x = new DefaultModelMetadataProvider(new DefaultCompositeMetadataDetailsProvider(null));
            var metadata = x.GetMetadataForType(typeof(IDataTablesRequest));

            var mockModelBinderProviderContext = new Mock<ModelBinderProviderContext>();
            mockModelBinderProviderContext
                .Setup(x => x.Metadata)
                .Returns(metadata);

            var modelBinder = mvcOptions.Value.ModelBinderProviders[0];
            Assert.NotNull(modelBinder);
            var binder = modelBinder.GetBinder(mockModelBinderProviderContext.Object);
            Assert.NotNull(binder);
            Assert.Equal(requestBinder, binder);
        }
        /// <summary>
        /// This test must be executed alone.
        /// Validate registration with custom parser function for aditional parameters.
        /// </summary>
        //[Fact]
        //public void RegistrationWithParseAdditionalParameters()
        //{
        //    // Arrange
        //    var serviceCollection = new ServiceCollection();
        //    DataTables.AspNet.AspNetCore.Configuration.RegisterDataTables(serviceCollection, TestHelper.ParseAdditionalParameters, true);
        //    serviceCollection.AddMvc();
        //    var provider = serviceCollection.BuildServiceProvider();
        //    var modelBinder = provider.GetServices<IModelBinder>().FirstOrDefault();

        //    // Assert
        //    Assert.True(DataTables.AspNet.AspNetCore.Configuration.Options.IsRequestAdditionalParametersEnabled);
        //    Assert.True(DataTables.AspNet.AspNetCore.Configuration.Options.IsResponseAdditionalParametersEnabled);
        //    Assert.NotNull((modelBinder as DataTables.AspNet.AspNetCore.ModelBinder).ParseAdditionalParameters);
        //}
    }

    /// <summary>
    /// A default implementation of <see cref="ICompositeMetadataDetailsProvider"/>.
    /// </summary>
    /// <remarks>
    /// https://raw.githubusercontent.com/dotnet/aspnetcore/a450cb69b5e4549f5515cdb057a68771f56cefd7/src/Mvc/Mvc.Core/src/ModelBinding/Metadata/DefaultCompositeMetadataDetailsProvider.cs
    /// </remarks>
    internal class DefaultCompositeMetadataDetailsProvider : ICompositeMetadataDetailsProvider
    {
        private readonly IEnumerable<IMetadataDetailsProvider> _providers;

        /// <summary>
        /// Creates a new <see cref="DefaultCompositeMetadataDetailsProvider"/>.
        /// </summary>
        /// <param name="providers">The set of <see cref="IMetadataDetailsProvider"/> instances.</param>
        public DefaultCompositeMetadataDetailsProvider(IEnumerable<IMetadataDetailsProvider> providers)
        {
            _providers = providers;
        }

        /// <inheritdoc />
        public void CreateBindingMetadata(BindingMetadataProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (var provider in _providers.OfType<IBindingMetadataProvider>())
            {
                provider.CreateBindingMetadata(context);
            }
        }

        /// <inheritdoc />
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (var provider in _providers.OfType<IDisplayMetadataProvider>())
            {
                provider.CreateDisplayMetadata(context);
            }
        }

        /// <inheritdoc />
        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (var provider in _providers.OfType<IValidationMetadataProvider>())
            {
                provider.CreateValidationMetadata(context);
            }
        }
    }
}
