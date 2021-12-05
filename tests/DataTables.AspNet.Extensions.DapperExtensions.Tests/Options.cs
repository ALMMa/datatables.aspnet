using DataTables.AspNet.Core;
using DataTables.AspNet.Core.NameConvention;

namespace DataTables.AspNet.Extensions.DapperExtensions.Tests
{
    /// <summary>
    /// Represents a configuration object for DataTables.AspNet.
    /// </summary>
    public class Options : IOptions
    {
        /// <summary>
        /// Gets default page length when parameter is not set.
        /// </summary>
        public int DefaultPageLength { get; private set; }
        /// <summary>
        /// Gets an indicator if draw parameter should be validated.
        /// </summary>
        public bool IsDrawValidationEnabled { get; private set; }
        /// <summary>
        /// Gets an indicator whether request aditional parameters parsing is enabled or not.
        /// </summary>
        public bool IsRequestAdditionalParametersEnabled { get; private set; }
        /// <summary>
        /// Gets an indicator whether response adicional parameters parsing is enabled or not.
        /// </summary>
        public bool IsResponseAdditionalParametersEnabled { get; private set; }



        /// <summary>
        /// Gets the request name convention to be used when resolving request parameters.
        /// </summary>
        public IRequestNameConvention RequestNameConvention { get; private set; }
        /// <summary>
        /// Gets the response name convention to be used when serializing response elements.
        /// </summary>
        public IResponseNameConvention ResponseNameConvention { get; private set; }





        /// <summary>
        /// Sets the default page length to be used when request parameter is not set.
        /// Page length is set to 10 by default.
        /// </summary>
        /// <param name="defaultPageLength">The new default page length to be used.</param>
        /// <returns></returns>
        public IOptions SetDefaultPageLength(int defaultPageLength) { DefaultPageLength = defaultPageLength; return this; }
        /// <summary>
        /// Enables draw validation.
        /// Draw validation is enabled by default.
        /// </summary>
        /// <returns></returns>
        public IOptions EnableDrawValidation() { IsDrawValidationEnabled = true; return this; }
        /// <summary>
        /// Disables draw validation.
        /// As stated by jQuery DataTables, draw validation should not be disabled unless explicitly required.
        /// </summary>
        /// <returns></returns>
        public IOptions DisableDrawValidation() { IsDrawValidationEnabled = false; return this; }
        /// <summary>
        /// Enables parsing request aditional parameters.
        /// You must provide your own function for request adicional parameter resolution on DataTables.AspNet registration.
        /// </summary>
        /// <returns></returns>
        public IOptions EnableRequestAdditionalParameters() { IsRequestAdditionalParametersEnabled = true; return this; }
        /// <summary>
        /// Disables parsing request aditional parameters.
        /// Request aditional parameters are not resolved by default for performance reasons.
        /// </summary>
        /// <returns></returns>
        public IOptions DisableRequestAdditionalParameters() { IsRequestAdditionalParametersEnabled = false; return this; }
        /// <summary>
        /// Enables parsing response aditional parameters.
        /// </summary>
        /// <returns></returns>
        public IOptions EnableResponseAdditionalParameters() { IsResponseAdditionalParametersEnabled = true; return this; }
        /// <summary>
        /// Disables parsing response aditional parameters.
        /// Response aditional parameters are not resolved by default for performance reasons.
        /// </summary>
        /// <returns></returns>
        public IOptions DisableResponseAdditionalParameters() { IsResponseAdditionalParametersEnabled = false; return this; }
        /// <summary>
        /// Forces DataTables to use CamelCase on request/respose parameter names.
        /// CamelCase is enabled by default and is available for DataTables 1.10 and above.
        /// </summary>
        /// <returns></returns>
        public IOptions UseCamelCase() { RequestNameConvention = new CamelCaseRequestNameConvention(); ResponseNameConvention = new CamelCaseResponseNameConvention(); return this; }
        /// <summary>
        /// Forces DataTables to use HungarianNotation on request/response parameter names.
        /// HungarianNotation is available for compatibility with older/legacy DataTables (prior to 1.10).
        /// </summary>
        /// <returns></returns>
        public IOptions UseHungarianNotation() { RequestNameConvention = new HungarianNotationRequestNameConvention(); ResponseNameConvention = new HungarianNotationResponseNameConvention(); return this; }





        /// <summary>
        /// Creates a new 'Option' instance.
        /// </summary>
        public Options()
            : this (10, true, false, false, new CamelCaseRequestNameConvention(), new CamelCaseResponseNameConvention())
        { }
        /// <summary>
        /// Creates a new 'Option' instance.
        /// </summary>
        /// <param name="defaultPageLength">Default page length to be used.</param>
        /// <param name="enableDrawValidation">Indicates if draw validation will be enabled by default or not.</param>
		/// <param name="enableRequestAdditionalParameters">Indicates if additional parameters resolution will be enabled for for request by default.</param>
		/// <param name="enableResponseAdditionalParameters">Indicates if additional parameters will be sent to the response by default.</param>
        /// <param name="requestNameConvention">Request naming convention to be used.</param>
        /// <param name="responseNameConvention">Response naming convention to be used.</param>
        public Options(int defaultPageLength, bool enableDrawValidation, bool enableRequestAdditionalParameters, bool enableResponseAdditionalParameters, IRequestNameConvention requestNameConvention, IResponseNameConvention responseNameConvention)
        {
            DefaultPageLength = defaultPageLength;
            IsDrawValidationEnabled = enableDrawValidation;
            IsRequestAdditionalParametersEnabled = enableRequestAdditionalParameters;
            IsResponseAdditionalParametersEnabled = enableResponseAdditionalParameters;

            RequestNameConvention = requestNameConvention;
            ResponseNameConvention = responseNameConvention;
        }
    }
}
