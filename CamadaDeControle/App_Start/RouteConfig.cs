using System.Web.Mvc;
using System.Web.Routing;

namespace CamadaDeControle
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProdutoListaPorPreco",
                url: "Produto/Lista/{PrecoMinimo}/{PrecoMaximo}",
                defaults: new
                {
                    controller = "Produto",
                    action = "Lista"
                });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {
                    controller = "Produto",
                    action = "Lista",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
