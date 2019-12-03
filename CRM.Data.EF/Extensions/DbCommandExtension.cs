using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace CRM.Data.EF.Extensions
{
    public static class DbCommandExtension
    {
        internal static int AddParameterWithValue<T>(this DbCommand cmd, string name, T value)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            return cmd.Parameters.Add(p);
        }
    }
}
