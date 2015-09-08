using System;
using Newtonsoft.Json;
using DataAccess;

namespace Service
{
    public class RoomsService
    {
        public static string BuildDataScript()
        {
            string result = "define({data:";
            using (var uow = new CodeCamperUnitOfWork())
            {
                result = string.Concat(result,
                    JsonConvert.SerializeObject(uow.RoomsRepository.FindAll()));
            }
            return string.Concat(result, "});");
        }
    }
}
