using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Application
{
    /// <summary>
    ///  AOP 功能
    /// </summary>
    public class ServiceInterceptor : IInterceptor
    {
        public virtual void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"{DateTime.Now}: 方法执行前");
            invocation.Proceed();
            Console.WriteLine($"{DateTime.Now}: 方法执行后");
        }
    }
}
