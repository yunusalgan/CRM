using CRM.Data.EF.Contexts;
using CRM.Data.Entities.Netah;
using CRM.Data.Interfaces;
using CRM.Service.Models.Outputs;
using System;
using System.Collections.Generic;

namespace CRM.Data.EF.Repositories
{
    public class SampleRepository : RepositoryBase<SampleEntity>, ISampleRepository
    {
        public SampleRepository(CrmContext crmContext) : base(crmContext)
        {

        }

        public List<SampleEntityOutput> GetSampleEntityGrid()
        {
            //var result = (from ap in _crmContext.SampleEntities
            //              select ap).FirstOrDefault();
            //return result;
            throw new NotImplementedException();

        }
    }
}
