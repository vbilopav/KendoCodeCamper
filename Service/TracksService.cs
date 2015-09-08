using Newtonsoft.Json;
using DataAccess;

namespace Service
{
    public class TracksService
    {
        public static string BuildDataScript()
        {
            string result = "define({data:";
            using (var uow = new CodeCamperUnitOfWork())
            {                
                result = string.Concat(result,
                    JsonConvert.SerializeObject(uow.TracksRepository.FindAll()));                
            }
            return string.Concat(result, "});");
        }
    }
}
