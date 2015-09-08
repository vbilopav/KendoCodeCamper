using CodeFirstConfig;
using Infrastructure.Web.Encoders;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Infrastructure.Web.Controllers
{
    public class LogModel
    {
        public Func<string, string, string, string, int, string> LogFunc { get; set; }
        public string Browser { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public string LineNumber { get; set; }
    }

    public sealed class LogControllerConfig : ConfigManager<LogControllerConfig>
    {
        public string LogEndpoint { get; set; }
        public int PeriodMilliseconds { get; set; }
        public int MaxThreads { get; set; }
        public int MaxJobs { get; set; }

        public LogControllerConfig()
        {
            LogEndpoint = "/Log";
            if (App.IsDebugConfiguration)
            {
                PeriodMilliseconds = 500; 
                MaxThreads = 1;
                MaxJobs = 100;
            }
            else
            {
                PeriodMilliseconds = 1 * 60 * 1000; //1 minutes
                MaxThreads = 1;
                MaxJobs = 10;
            }
        }
    }

    [RoutePrefix("Log")]
    public class LogController : ApiController
    {        
        public static TimedConsumer<LogModel> Consumer { get; private set; }

        static LogController()
        {
            Consumer = new TimedConsumer<LogModel>(LogControllerConfig.Config.MaxThreads, LogControllerConfig.Config.PeriodMilliseconds);
            Consumer.Start(() =>
            {                
                LogModel logView;
                var i = 0;
                while (Consumer.Queue.TryDequeue(out logView))
                {
                    int line;
                    int.TryParse(logView.LineNumber, out line);
                    logView.LogFunc(logView.Message, logView.Browser, null, logView.Url, line);
                    i++;
                    if (i == LogControllerConfig.Config.MaxJobs) break;
                }
            });
            AppFinalizator.CleanupQueue.Enqueue(Consumer);
        }

        public LogController(IEncoder decoder)
        {
            _decoder = decoder;
        }

        public LogController()
            : this(new PlainTextEncoder())
        {
        }

        private readonly IEncoder _decoder;

        private async Task<LogModel> RetreiveModel()
        {
            try
            {
                var ret =
                    JsonConvert.DeserializeObject<LogModel>(
                        _decoder.Decode(await Request.Content.ReadAsStringAsync()));
                if (string.IsNullOrEmpty(ret.Browser) || string.IsNullOrEmpty(ret.Message))
                    return null;
                return ret;
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        [Route("Error")]
        public async void Error()
        {
            LogModel model = await RetreiveModel();
            if (model == null) return;
            model.LogFunc = Log.Error;
            Consumer.Queue.Enqueue(model);
        }

        [HttpPost]
        [Route("Warning")]
        public async void Warning()
        {
            LogModel model = await RetreiveModel();
            if (model == null) return;
            model.LogFunc = Log.Warning;
            Consumer.Queue.Enqueue(model);
        }

        [HttpPost]
        [Route("Info")]
        public async void Info()
        {
            LogModel model = await RetreiveModel();
            if (model == null) return;
            model.LogFunc = Log.Info;
            Consumer.Queue.Enqueue(model);
        }
	}
}