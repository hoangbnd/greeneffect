using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using GreenEffect.Repository;
using GreenEffect.Web.Framework.EmbeddedViews;
using GreenEffect.Web.Framework.Mvc;
using GreenEffect.Web.Framework.Mvc.Routes;
using GreenEffect.Web.Framework.UI.Editor;
using MVCCore;
using MVCCore.Caching;
using MVCCore.Data;
using MVCCore.Fakes;
using MVCCore.Plugins;
using GreenEffect.Services.Implement;
using GreenEffect.Services.Interface;

namespace GreenEffect.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {

            
            var dependencyResolver = new MVCCoreDependencyResolver();
            DependencyResolver.SetResolver(dependencyResolver);
            ModelBinders.Binders.Add(typeof(BaseNopModel), new MVCCoreModelBinder());
            ModelMetadataProviders.Current = new MVCCoreMetadataProvider();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            // Create the container builder.
            var builder = new ContainerBuilder();
            
            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //HTTP context and other related stuff
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerHttpRequest().InstancePerApiRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerHttpRequest().InstancePerApiRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerHttpRequest().InstancePerApiRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerHttpRequest().InstancePerApiRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerHttpRequest().InstancePerApiRequest();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerHttpRequest().InstancePerApiRequest();

            //controllers
            //builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //data layer
            builder.RegisterType<DataSettings>();
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency().InstancePerApiRequest();


            builder.Register(x => (IEfDataProvider)x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency().InstancePerApiRequest();
            builder.Register(x => (IEfDataProvider)x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IEfDataProvider>().InstancePerDependency().InstancePerApiRequest();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dataSettingsManager.LoadSettings());
                var dataProvider = (IEfDataProvider)efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();

                builder.Register<IDbContext>(c => new DbObjectContext(dataProviderSettings.DataConnectionString)).InstancePerHttpRequest().InstancePerApiRequest();
            }
            else
            {
                builder.Register<IDbContext>(c => new DbObjectContext(dataSettingsManager.LoadSettings().DataConnectionString)).InstancePerHttpRequest().InstancePerApiRequest();
            }


            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest().InstancePerApiRequest();

            //plugins
            builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerHttpRequest().InstancePerApiRequest();

            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("webmvc_cache_static").SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("webmvc_cache_per_request").InstancePerHttpRequest().InstancePerApiRequest();

            //work context
            //builder.RegisterType<WorkContext>().As<IWorkContext>().InstancePerHttpRequest().InstancePerApiRequest();
                
            //register file and email
            builder.RegisterType<FileWrapper>().As<IFile>().InstancePerHttpRequest().InstancePerApiRequest();
            //services
            builder.RegisterType<UserServices>().As<IUserServices>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<CustomersSevices>().As<ICustomersServices>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<RouteSevices>().As<IRouteSevice>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<CustomerRouteServices>().As<ICustomersRoutesServices>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<ProductsGroupServices>().As<IProductsGroupServices>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<ProductsServices>().As<IProductsServices>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<AuthorityObjectServices>().As<IAuthorityObjectServices>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<OrderServices>().As<IOrderServices>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<OrderDataServices>().As<IOrderDataServices>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<CustomersLocationServices>().As<ICustomersLocationServices>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<CustomersImagesServices>().As<ICustomersImagesServices>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<MessagerServices>().As<IMessagerServices>().InstancePerHttpRequest().InstancePerApiRequest();
            
            //register webhelper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerHttpRequest().InstancePerApiRequest();

            builder.RegisterType<EmbeddedViewResolver>().As<IEmbeddedViewResolver>().SingleInstance().InstancePerApiRequest();
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance().InstancePerApiRequest();

            //HTML Editor services
            builder.RegisterType<NetAdvDirectoryService>().As<INetAdvDirectoryService>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<NetAdvImageService>().As<INetAdvImageService>().InstancePerHttpRequest().InstancePerApiRequest();

            // Build the container.
            var container = builder.Build();

            // Create the depenedency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            // Set the dependency resolver for MVC.
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }
    }
}
