using System;
using System.Reflection;

namespace DataTables.AspNet.Samples.WebApi2.BasicIntegration.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}