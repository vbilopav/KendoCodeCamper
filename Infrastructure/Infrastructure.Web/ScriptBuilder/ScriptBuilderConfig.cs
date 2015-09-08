using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Infrastructure.Web.ScriptBuilder
{
    public enum TypeFormat
    {
        Raw, 
        Reformat, 
        Object
    }

    public class WrapConfig
    {
        public string Start { get; set; }
        public string End { get; set; }
    }

    public class ResourceItemConfig
    {
        public string Key { get; set; }
        public string Type { get; set; }
    }

    public class ResourcesConfig
    {
        public string Folder { get; set; }
        public string Cultures { get; set; }
        public WrapConfig Wrap { get; set; }
        public IEnumerable<ResourceItemConfig> List { get; set; }
    }

    public class TypeItemConfig
    {
        public string Type { get; set; }
        public string Script { get; set; }
        public string Method { get; set; }
        public WrapConfig Wrap { get; set; }
        public TypeFormat Format { get; set; }
        public object[] Parameters { get; set; }
        public TypeItemConfig()
        {
            Format = TypeFormat.Raw;
            Parameters = null;
        }
    }

    public class ScriptBuilderConfig
    {
        public string BaseFolder { get; set; }
        public string BinFolder { get; set; }
        public Formatting Formatting { get; set; }
        public ResourcesConfig Resources { get; set; }
        public IEnumerable<TypeItemConfig> Types { get; set; }

        public ScriptBuilderConfig()
        {
            BaseFolder = App.Config.Folder;
            BinFolder = App.Config.BinFolder;
            Formatting = Formatting.Indented;
            Resources = new ResourcesConfig
            {
                Folder = @"App\resources",
                Cultures = string.Join(",", SpaApp.Config.Cultures), //!?
                List = new[]         
                { 
                    new ResourceItemConfig 
                    { 
                        Key = "core", 
                        Type = "Infrastructure._resx.Core, Infrastructure.Core"
                    } 
                }
            };
            Types = new[]         
            {             
                new TypeItemConfig
                {
                    Wrap = new WrapConfig
                    {
                        Start = "var _ = _ || ",
                        End = ""
                    },
                    Script = @"Scripts\_app.js",
                    Type = "Infrastructure.Web.AppScriptBuilder.BuildIndentedRawFileScript, Infrastructure.Web",
                    Method = "BuildIndentedRawFileScript",
                    Format = TypeFormat.Raw
                }
            };
        }
    }
}
