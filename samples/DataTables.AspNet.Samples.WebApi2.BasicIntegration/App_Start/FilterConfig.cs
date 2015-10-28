using System.Web;
using System.Web.Mvc;

namespace DataTables.AspNet.Samples.WebApi2.BasicIntegration
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
