using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Application.Share.Interceptors
{
    public class OpsLogInterceptor : IInterceptor
    {
        private readonly OpsLogAsyncInterceptor _opsLogAsyncInterceptor;

        public OpsLogInterceptor(OpsLogAsyncInterceptor opsLogAsyncInterceptor)
        {
            _opsLogAsyncInterceptor = opsLogAsyncInterceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            this._opsLogAsyncInterceptor.ToInterceptor().Intercept(invocation);
        }
    }
}
