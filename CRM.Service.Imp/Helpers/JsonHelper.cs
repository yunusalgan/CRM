using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Imp.Helpers
{
    public static class JsonHelper
    {
        public static string GetJsonString(string enValue, string trValue)
        {
            return $@"{{""en"":""{enValue.Trim()}"",""tr"":""{trValue.Trim()}""}}";
        }
        public static string GetJsonParseString(string value, string culture)
        {
            string result = string.Empty;
            switch (culture)
            {
                case "en":
                    result = value != null ? JObject.Parse(value)[culture].ToString() : string.Empty;
                    break;
                case "tr":
                    result = value != null ? JObject.Parse(value)[culture].ToString() : string.Empty;
                    break;
            }
            return result;
        }
    }
}
