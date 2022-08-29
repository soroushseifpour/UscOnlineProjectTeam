using System.Web.Mvc;

namespace UscProject.Areas.KarJo
{
    public class KarJoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KarJo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KarJo_default",
                "KarJo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}