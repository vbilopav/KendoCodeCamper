using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using DataAccess;

namespace Service
{
    public class TagsService
    {
        public static string BuildDataScript()
        {
            string result = "define({data:";
            using (var uow = new CodeCamperUnitOfWork())
            {
                result = string.Concat(result,
                    JsonConvert.SerializeObject(uow.SessionsRepository.GetTagCounts()));
            }
            return string.Concat(result, "});");
        }
    }
}
