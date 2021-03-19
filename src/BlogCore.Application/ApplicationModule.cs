using Autofac;
using Autofac.Extras.DynamicProxy;
using BlogCore.Application.Advertisement;
using BlogCore.Application.Services;
using BlogCore.Application.UserInfo;
using Castle.DynamicProxy;
using FluentValidation;

namespace BlogCore.Application
{
    public class ApplicationModule:Module
    {
        public ApplicationModule()
        { 
        
        }

        protected override void Load(ContainerBuilder builder)
        {
            // 要先注册拦截器
            builder.RegisterType<ServiceInterceptor>();
            //注册服务
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.IsAssignableTo<IAppService>())
                //根据类型注册组件IAppService,暴漏其实现的所有服务（接口）
                .AsImplementedInterfaces()
                //每个生命周期作用域一个实例
                .InstancePerLifetimeScope()
                 //动态注入拦截器
                .InterceptedBy(typeof(ServiceInterceptor))
                .EnableClassInterceptors(ProxyGenerationOptions.Default, additionalInterfaces: typeof(IAdvertisementService));
               

            //注册DtoValidators
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
