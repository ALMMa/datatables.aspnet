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
        /// <returns></returns>
        IOptions UseCamelCase();
        /// <summary>
        /// Forces DataTables to use HungarianNotation naming convention.
        /// HungarianNotation is available for compatibility with older DataTables (prior to 1.10).
        /// </summary>
        /// <returns></returns>
        IOptions UseHungarianNotation();
    }
}
