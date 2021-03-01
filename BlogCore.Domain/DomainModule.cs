using Autofac;
using BlogCore.Domain.DomainServices.Advertisement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Domain
{
    public class DomainModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(AdvertisementDomainServices)).As<IAdvertisementDomainServices>().InstancePerLifetimeScope();
        }
    }
}
