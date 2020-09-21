using System.Web;
using System.Web.Mvc;

namespace VidlyProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new RequireHttpsAttribute()); //endpoints will no longer be available on http channel, only https
        }
    }
}
