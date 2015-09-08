using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Web.ScriptBuilder
{
    public class ScriptBuilderManager
    {               
        static ScriptBuilderManager()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            };
        }
        
        public static void SaveAllFiles(TextWriter @out)
        {
            var config = ScriptBuilderConfigManager.Config;

            if (config.Resources != null && config.Resources.Cultures != null)
            {
                foreach (string culture in config.Resources.Cultures.Split(','))
                {                                               
                    var sb = new StringBuilder();
                    if (config.Resources.Wrap != null)
                    {
                        sb.Append(config.Resources.Wrap.Start);
                    }
                    sb.Append("{\"name\":\"");                    
                    sb.Append(culture);
                    sb.Append("\",\"value\":\"");
                    sb.Append(new CultureInfo(culture).NativeName);
                    sb.Append("\"");  

                    foreach (ResourceItemConfig item in config.Resources.List)
                    {
                        sb.Append(",\"");
                        sb.Append(item.Key);
                        sb.Append("\":");
                        sb.Append(ResourceSerializer.ToJson(item.Type, culture));
                    }
                    sb.Append("}");
                    if (config.Resources.Wrap != null)
                    {
                        sb.Append(config.Resources.Wrap.End);
                    }
                    string fileName = Path.Combine(config.Resources.Folder, 
                        string.Concat(culture, ".js"));
                    File.WriteAllText(fileName, sb.ToString());
                    @out.WriteLine("Successfully written resource script file: '{0}'", fileName);
                }            
            }

            if (config.Types == null) return;
            foreach (TypeItemConfig item in config.Types)
            {
                string fileName = ScriptBuilderConfigManager.ParsePath(item.Script);
                string[] split = item.Type.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                Assembly assembly = ScriptBuilderConfigManager.LoadAssembly(split[1]);
                Type type = assembly.GetType(split[0].Trim());

                object[] parameters = null;
                if (item.Parameters != null)
                    parameters = item.Parameters;
                MethodInfo method = type.GetMethod(item.Method, BindingFlags.Public | BindingFlags.Static);
                object result = method.Invoke(null, parameters);

                string content;
                switch (item.Format)
                {                          
                    case TypeFormat.Raw:
                        content = result as string;
                        break;
                    case TypeFormat.Reformat:
                        dynamic parsedJson = JsonConvert.DeserializeObject(result as string);
                        content = JsonConvert.SerializeObject(parsedJson, config.Formatting); 
                        break;
                    case TypeFormat.Object:
                        content = JsonConvert.SerializeObject(result, config.Formatting);
                        break;
                    default:
                        content = result as string;
                        break;
                }
                if (item.Wrap != null)
                {
                    content = string.Concat(item.Wrap.Start, content, item.Wrap.End);
                }
                File.WriteAllText(fileName, content);
                @out.WriteLine("Successfully written script: '{0}'", fileName);
            }
        }
    }
}
