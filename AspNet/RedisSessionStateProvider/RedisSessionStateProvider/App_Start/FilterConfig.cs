using System.Web;
using System.Web.Mvc;

namespace RedisSessionStateProvider
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
