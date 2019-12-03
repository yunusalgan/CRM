using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Bootstrapper.Interceptors
{
    class ServiceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
