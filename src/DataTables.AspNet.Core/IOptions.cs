namespace DataTables.AspNet.Core
{
    /// <summary>
    /// Defines DataTables option members and methods.
    /// </summary>
    public interface IOptions
    {
        /// <summary>
        /// Gets the default page length.
        /// </summary>
        int DefaultPageLength { get; }

        /// <summary>
        /// Gets the indicator whether draw validation is enabled or not.
        /// </summary>
        bool IsDrawValidationEnabled { get; }

        /// <summary>
        /// Gets the indicator whether aditional request parameters parsing is enabled or not.
        /// </summary>
        bool IsRequestAdditionalParametersEnabled { get; }

        /// <summary>
        /// Gets the indicator whether aditional response parameters parsing is enabled or not.
        /// </summary>
        bool IsResponseAdditionalParametersEnabled { get; }

        /// <summary>
        /// Gets request name conventions.
        /// </summary>
        NameConvention.IRequestNameConvention RequestNameConvention { get; }

        /// <summary>
        /// Gets response name conventions.
        /// </summary>
        NameConvention.IResponseNameConvention ResponseNameConvention { get; }

        /// <summary>
        /// Sets default page length.
        /// </summary>
        /// <param name="defaultPageLength">Default page length to use.</param>
        /// <returns></returns>
        IOptions SetDefaultPageLength(int defaultPageLength);

        /// <summary>
        /// Enables draw validation.
        /// Draw validation is enabled by default.
        /// </summary>
        /// <returns></returns>
        IOptions EnableDrawValidation();

        /// <summary>
        /// Disables draw validation.
        /// </summary>
        /// <returns></returns>
        IOptions DisableDrawValidation();

        /// <summary>
        /// Enables request aditional parameter parsing.
        /// You must also provide your own custom parsing function on registration.
        /// </summary>
        /// <returns></returns>
        IOptions EnableRequestAdditionalParameters();

        /// <summary>
        /// Disables request aditional parameter parsing.
        /// </summary>
        /// <returns></returns>
        IOptions DisableRequestAdditionalParameters();

        /// <summary>
        /// Enables response aditional parameter parsing.
        /// </summary>
        /// <returns></returns>
        IOptions EnableResponseAdditionalParameters();

        /// <summary>
        /// Disables response aditional parameter parsing.
        /// </summary>
        /// <returns></returns>
        IOptions DisableResponseAdditionalParameters();

        /// <summary>
        /// Forces DataTables to use CamelCase naming convention.
        /// CamelCase is enabled by default.
        /// </summary>
        /// <returns>A configured instance implementing the<see cref="IOptions"/> interface.</returns>
        IOptions UseCamelCase();

        /// <summary>
        /// Forces DataTables to use HungarianNotation naming convention.
        /// HungarianNotation is available for compatibility with older DataTables (prior to 1.10).
        /// </summary>
        /// <returns>A configured instance implementing the<see cref="IOptions"/> interface.</returns>
        IOptions UseHungarianNotation();
    }
}
