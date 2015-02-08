using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
// added...
using AutoMapper;

namespace AssociationsIntro
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Store initializer
            System.Data.Entity.Database.SetInitializer(new Models.StoreInitializer());

            // AutoMapper

            Mapper.CreateMap<Models.Program, Controllers.ProgramBase>();
            Mapper.CreateMap<Models.Program, Controllers.ProgramBaseWithSubjects>();
            Mapper.CreateMap<Controllers.ProgramAdd, Models.Program>();

            Mapper.CreateMap<Models.Subject, Controllers.SubjectBase>();
            Mapper.CreateMap<Models.Subject, Controllers.SubjectBaseWithProgram>();
            Mapper.CreateMap<Controllers.SubjectAdd, Models.Subject>();
            
        }
    }
}
