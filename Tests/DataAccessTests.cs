using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;

namespace Tests
{
    [TestClass]
    public class DataAccessTests : TestBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var uow = new CodeCamperUnitOfWork())
            {
                List<int> tracks = new List<int> { 2, 4 };
                List<string> tags = new List<string> { "Web", "JavaScript" };
                var x = uow.SessionsRepository.SearchAsync("pirate", tracks, tags);
                x.Wait();

                Log.Info(x.Result.Count);
                Log.Info(uow.GetSql());                
            }
        }
    }
}
