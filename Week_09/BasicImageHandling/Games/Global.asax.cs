using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
// added...
using AutoMapper;

namespace Games
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Store intializer
            //System.Data.Entity.Database.SetInitializer(new Models.StoreInitializer());

            // AutoMapper mappings

            Mapper.CreateMap<Controllers.SportAdd, Models.Sport>();
            Mapper.CreateMap<Models.Sport, Controllers.SportBase>();
            Mapper.CreateMap<Models.Sport, Controllers.SportList>();
            Mapper.CreateMap<Models.Sport, Controllers.SportBaseWithImage>();
            Mapper.CreateMap<Models.Sport, Controllers.SportBaseWithVenues>();
            Mapper.CreateMap<Controllers.SportBase, Controllers.SportWithLink>();
            Mapper.CreateMap<Controllers.SportBase, Controllers.SportEditForm>();
            Mapper.CreateMap<Controllers.SportEdit, Controllers.SportEditForm>();

            Mapper.CreateMap<Controllers.VenueAdd, Models.Venue>();
            Mapper.CreateMap<Models.Venue, Controllers.VenueBase>();
            Mapper.CreateMap<Models.Venue, Controllers.VenueList>();
            Mapper.CreateMap<Models.Venue, Controllers.VenueBaseWithImage>();
            Mapper.CreateMap<Models.Venue, Controllers.VenueBaseWithSports>();
            Mapper.CreateMap<Controllers.VenueBase, Controllers.VenueWithLink>();
            Mapper.CreateMap<Controllers.VenueBase, Controllers.VenueBaseWithSports>();
            Mapper.CreateMap<Controllers.VenueBaseWithSports, Controllers.VenueWithSportsWithLink>();
            //Mapper.CreateMap<Controllers.VenueBase, Controllers.VenueEditForm>();
        }

        protected void Application_EndRequest()
        {
            // Handling error conditions...
            // Inspiration: http://stackoverflow.com/a/9026907 

            var code = Context.Response.StatusCode;

            // Add more conditions here as you need them
            if (code == 404) { this.HandleError("NotFound"); }
            if (code >= 500) { this.HandleError("ServerError"); }
        }

        private void HandleError(string action)
        {
            // This method causes the Errors controller to handle the request
            // It creates the controller, then executes the desired action method

            // With some more code, it could also save the error,
            // and notify the web site programmer(s)

            // Clear the accumulated data out of the response object
            Response.Clear();

            // Create the route data configuration
            var rd = new RouteData();
            rd.Values["controller"] = "Errors";
            rd.Values["action"] = action;

            // Create then execute the controller method
            IController c = new Games.Controllers.ErrorsController();
            c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
        }

    }
}
