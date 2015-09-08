using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using Newtonsoft.Json;

namespace Infrastructure.Web.ScriptBuilder
{
    public class ResourceSerializer
    {
        public static string ToJson(string resx, string culture = null)
        {
            CultureInfo ci = culture == null ? CultureInfo.CurrentUICulture : new CultureInfo(culture);
            ResourceSet rs;
            string[] split = resx.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length == 1)
            {
                rs = new ResourceManager(split[0], AppAssembly.Assembly)
                    .GetResourceSet(ci, true, true);
            }
            else
            {
                rs = new ResourceManager(split[0], ScriptBuilderConfigManager.LoadAssembly(split[1]))
                    .GetResourceSet(ci, true, true);
            }
            Dictionary<string, string> dict = 
                rs.Cast<DictionaryEntry>()
                .ToDictionary(x => x.Key.ToString(), x => x.Value.ToString());
            return JsonConvert.SerializeObject(dict, ScriptBuilderConfigManager.Config.Formatting);
        }
    }
}