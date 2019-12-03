using CRM.Service.Enums;
using CRM.Service.Imp.Helpers;
using CRM.Service.Interfaces;
using CRM.Service.Models.Outputs;
using CRM.Service.Models.ReturnModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Transactions;

namespace CRM.Service.Imp.Services
{
    public class SampleService : ServiceBase, ISampleService
    {

        public GenericResult<List<SampleEntityOutput>> SampleMethod()
        {
            var uowResult = UnitOfWork.Run<GenericResult<List<SampleEntityOutput>>>(result =>
            {
                result = new GenericResult<List<SampleEntityOutput>>() { StatusCode = (int)ResultStatuses.Success, Message = MLValueHelper.GetMultilingualValue("SUCCESS", CultureInfo.CurrentCulture.Name), Data = new List<SampleEntityOutput>() };

                var asd = UnitOfWork.Sample.GetAll();


                result.Data = new List<SampleEntityOutput> { };

                return result;
            }, null, IsolationLevel.ReadUncommitted);

            return uowResult;
        }
    }
}
