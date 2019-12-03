using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Models.Inputs.ApplicationErrors
{
    public class InsertErrorInput
    {
        public string ErrorMessage { get; set; }

        public string ErrorStack { get; set; }
    }
}
