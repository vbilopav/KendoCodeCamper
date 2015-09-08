using System;

namespace ScriptBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new[]
            //{
            //    "Release",
            //    @"C:\Dev\KendoCodeCamper\Src\Web\",
            //    @"C:\Dev\KendoCodeCamper\Src\",
            //    @"C:\Dev\KendoCodeCamper\Src\Web\App\build-system\script-builder.js"
            //};
            //args = new[] 
            //{  
            //    "Release", 
            //    @"C:\Dev\KendoCodeCamper\KendoCodeCamper\Web\", 
            //    @"C:\Dev\KendoCodeCamper\KendoCodeCamper\", 
            //    @"C:\Dev\KendoCodeCamper\KendoCodeCamper\Web\App\build-system\script-builder.js"
            //};

            Console.WriteLine(@"Running script builder console using following arguments {0} ... ", string.Join(", ", args));            
            string configFile = args[3];
            Infrastructure.Web.ScriptBuilder.ScriptBuilderConfigManager.LoadFromJsonFile(configFile, args);           
            Console.WriteLine(@"Configuration '{0}' loaded.", configFile);
            Infrastructure.Web.ScriptBuilder.ScriptBuilderManager.SaveAllFiles(Console.Out);
            Console.WriteLine(@"Script builder console finished.");
        }
    }
}
