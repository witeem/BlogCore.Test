﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Core
{
    public class CoreModule : Module
    {
        public CoreModule()
        {

        }
        protected override void Load(ContainerBuilder builder)
        {
            //注册服务
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.IsAssignableTo<IAppCore>())
                //根据类型注册组件IAppService,暴漏其实现的所有服务（接口）
                .AsImplementedInterfaces()
                //每个生命周期作用域一个实例
                .InstancePerLifetimeScope();
            ////动态注入拦截器
            //.EnableInterfaceInterceptors()
            //.InterceptedBy(typeof(OpsLogInterceptor), typeof(EasyCachingInterceptor));

            //builder.RegisterType(typeof(AdvertisementDomainServices)).As<IAdvertisementDomainServices>().InstancePerLifetimeScope();
        }
    }
}
