using System.Web.Mvc;

namespace UscProject.Areas.KarFarma
{
    public class KarFarmaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KarFarma";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KarFarma_default",
                "KarFarma/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}