namespace HallRental.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;

    public static class ActiveLinkExtension
    {
        public static string IsLinkActive(this IUrlHelper url, string controller, string action)
        {
            var controller1 = url.ActionContext.RouteData.Values["controller"].ToString();
            var action11 = url.ActionContext.RouteData.Values["action"].ToString();

            if (url.ActionContext.RouteData.Values["controller"].ToString() == controller &&
                url.ActionContext.RouteData.Values["action"].ToString() == action)
            {
                return "w3-grey";
            }

            return "";
        }
    }
}
