using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Infrastructure.Web.ScriptBuilder
{
    public class ScriptBuilderConfigManager : ConfigManager<ScriptBuilderConfig>
    {
        private static readonly object LoadLock = new object();

        public static string ParsePath(string value, string combine = null)
        {
            if (combine == null)
                combine = Config.BaseFolder;
            return value.Contains("../") ? Path.Combine(combine, value.Replace("../", "").Replace("/", @"\")) : value.Replace("/", @"\");
        }

        public static Assembly LoadAssembly(string file)
        {
            file = file.EndsWith(".dll") ? file.Trim() : string.Concat(file, ".dll").Trim();
            if (!file.Contains(":\\"))
            {
                file = Path.Combine(Config.BinFolder, file);
            }
            return Assembly.LoadFrom(file);
        }
        
        public static void LoadFromJsonFile(string file, params string[] formatParameters)
        {
            lock (LoadLock)
            {
                string configString = File.ReadAllText(file);

                for (int i = 0; i < formatParameters.Length; i++)
                {
                    configString =
                        configString.Replace(string.Concat("%", (i + 1).ToString()), formatParameters[i]);
                }

                configString = configString
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace(@"\", "/");

                dynamic config = JsonConvert.DeserializeObject(configString);
                if (config.baseFolder != null)
                    Config.BaseFolder = config.baseFolder.Value.Replace("/", @"\");

                if (config.binFolder != null)
                {
                    Config.BinFolder = ParsePath(config.binFolder.Value);                     
                }
                else
                {
                    Config.BinFolder = string.Concat(Config.BaseFolder, "bin");
                    if (!Directory.Exists(Config.BinFolder))
                    {
                        Config.BinFolder = Path.Combine(Config.BinFolder, formatParameters[0]);
                    }
                }
                if (!Directory.Exists(Config.BinFolder))
                    throw new ArgumentException("ScriptBuilder could not find bin folder: {0}", Config.BinFolder);

                if (config.formatting != null)
                {
                    Formatting f;
                    Enum.TryParse(config.formatting.Value.ToString(), out f);
                    Config.Formatting = f;
                }

                if (config.resources != null)
                {
                    Config.Resources = new ResourcesConfig();
                    if (config.resources.folder != null)
                    {
                        Config.Resources.Folder = ParsePath(config.resources.folder.Value);
                    }
                    if (config.resources.cultures != null)
                    {
                        Config.Resources.Cultures = config.resources.cultures;
                    }                    
                    if (config.resources.wrap != null)
                    {
                        Config.Resources.Wrap = new WrapConfig
                        {
                            Start = config.resources.wrap.start,
                            End = config.resources.wrap.end
                        };
                    }
                    if (config.resources.list != null)
                    {
                        var list = new List<ResourceItemConfig>();
                        foreach (var l in config.resources.list)
                        {
                            list.Add(new ResourceItemConfig {Key = l.key.Value, Type = l.type.Value});
                        }
                        Config.Resources.List = list.ToArray();
                    }
                }

                if (config.types != null)
                {
                    var typeList = new List<TypeItemConfig>();
                    foreach (var t in config.types)
                    {
                        var item = new TypeItemConfig();
                        if (t.format != null)
                        {
                            TypeFormat f;
                            Enum.TryParse(t.format.Value, out f);
                            item.Format = f;
                        }
                        item.Method = t.method.Value;
                        item.Type = t.type.Value;
                        item.Script = t.script.Value;
                        if (t.wrap != null)
                        {
                            item.Wrap = new WrapConfig
                            {
                                Start = t.wrap.start.Value,
                                End = t.wrap.end
                            };
                        }
                        typeList.Add(item);
                    }
                    Config.Types = typeList.ToArray();
                }
            }
        }
    }
}
