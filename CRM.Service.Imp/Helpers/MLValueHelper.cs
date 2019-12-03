using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CRM.Service.Imp.Helpers
{
    public static class MLValueHelper
    {
        private static ConcurrentDictionary<string, string> _mlValues;

        private static string _beforeCulture;

        public static string GetMultilingualValue(string code, string culture = "en")
        {
            var fileName = string.Empty;

            switch (culture.ToLowerInvariant())
            {
                case "en":
                case "en-gb":
                case "en-us":
                    fileName = "en";
                    break;
                case "tr":
                case "tr-tr":
                    fileName = "tr";
                    break;
            }

            if (_beforeCulture != culture)
            {
                _mlValues = new ConcurrentDictionary<string, string>();

                using (StreamReader r = new StreamReader(fileName + ".json"))
                {
                    var value = JsonConvert.DeserializeObject<JObject>(r.ReadToEnd());

                    foreach (var item in value)
                    {
                        _mlValues.TryAdd(item.Key.ToString(), item.Value.ToString());
                    }
                }
            }

            _beforeCulture = culture;

            string mlValue = string.Empty;

            if (_mlValues.TryGetValue(code, out mlValue))
            {
                return mlValue;
            }

            return "NOT_FOUND - " + code;
        }
    }
}
