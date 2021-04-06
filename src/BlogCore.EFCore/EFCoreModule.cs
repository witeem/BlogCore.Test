using Autofac;
using BlogCore.Core.ModulesInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.EFCore
{
    public class EFCoreModule : Module
    {

        public EFCoreModule()
        {

        }
        protected override void Load(ContainerBuilder builder)
        {
            //注册服务
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.IsAssignableTo<IEFCore>())
                //根据类型注册组件IAppService,暴漏其实现的所有服务（接口）
                .AsImplementedInterfaces()
                //每个生命周期作用域一个实例
                .InstancePerLifetimeScope();
            ////动态注入拦截器
            //.EnableInterfaceInterceptors()
            //.InterceptedBy(typeof(OpsLogInterceptor), typeof(EasyCachingInterceptor));
        }
    }
}
