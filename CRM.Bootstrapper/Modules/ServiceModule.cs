using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Bootstrapper.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            // Servisler buraya gelicek
            //builder.RegisterType<UserService>().As<IUserService>().PropertiesAutowired();

            base.Load(builder);
        }
    }
}
