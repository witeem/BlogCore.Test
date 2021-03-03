using System.Reflection;
using Autofac;
using BlogCore.Application.Application;
using BlogCore.Application.IApplication;

namespace BlogCore.Application
{
    public class ApplicationModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(AdvertisementServices)).As<IAdvertisementServices>().InstancePerLifetimeScope();
        } 
    }
}
