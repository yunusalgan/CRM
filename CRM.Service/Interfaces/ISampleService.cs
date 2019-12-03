using CRM.Service.Models.Outputs;
using CRM.Service.Models.ReturnModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Interfaces
{
    public interface ISampleService
    {
        GenericResult<List<SampleEntityOutput>> SampleMethod();
    }
}
