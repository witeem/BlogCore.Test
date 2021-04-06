using Autofac;
using BlogCore.Application;
using BlogCore.Core;
using BlogCore.Domain;
using BlogCore.EFCore;

namespace BlogCore
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new EFCoreModule());
        }
    }
}
