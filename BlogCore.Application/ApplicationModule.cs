using Autofac;
using BlogCore.Application.Application;
using BlogCore.Application.IApplication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Application
{
    public class ApplicationModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(AdvertisementServices)).As<IAdvertisementServices>().InstancePerLifetimeScope();
        }
    }
}
