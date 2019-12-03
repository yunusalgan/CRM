using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Models.ReturnModels
{
    public class GenericResult<T> : Result
    {
        public T Data { get; set; }
    }
}
