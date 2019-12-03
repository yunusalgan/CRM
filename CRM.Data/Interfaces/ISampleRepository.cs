using CRM.Data.Entities.Netah;
using CRM.Service.Models.Outputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Data.Interfaces
{
    public interface ISampleRepository : IRepositoryBase<SampleEntity>
    {
        List<SampleEntityOutput> GetSampleEntityGrid();
    }
}
