using System.Web;
using System.Web.Mvc;

namespace BTL_Mang_may_tinh
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
