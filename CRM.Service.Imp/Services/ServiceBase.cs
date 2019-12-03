using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Imp.Services
{
    public class ServiceBase
    {
        public IUnitOfWork UnitOfWork { get; set; }

    }
}
