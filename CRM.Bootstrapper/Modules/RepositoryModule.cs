using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Bootstrapper.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repolar buraya gelicek (DB Context Dahil)

            //builder.RegisterType<DBContext>().AsSelf().PropertiesAutowired().SingleInstance().InstancePerLifetimeScope();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().PropertiesAutowired();
            base.Load(builder);
        }
    }
}
