using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Bootstrapper.Interceptors
{
    public class RepositoryInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
