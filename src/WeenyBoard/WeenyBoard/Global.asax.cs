using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeenyBoard.CommandHandlers;
using WeenyBoard.Data;
using WeenyBoard.Infrastructure;

namespace WeenyBoard
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterTypes();
            RegisterCommandHandlers();
        }

        private void RegisterCommandHandlers()
        {
            var commandDispatcher = ObjectRegistry.Instance.Resolve<ICommandDispatcher>();
            var persistentStoreCommandHandler = new PersistentStoreCommandHandler();
            commandDispatcher.RegisterHandler(persistentStoreCommandHandler);
        }

        private void RegisterTypes()
        {
            ObjectRegistry.Instance.Register<ICommandDispatcher>(new CommandDispatcher());
            ObjectRegistry.Instance.Register<IBoardRepository>(new InMemoryBoardRepository());
        }
    }
}