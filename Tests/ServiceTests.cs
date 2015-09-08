using System;
using System.Linq;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using Service.Model;

namespace Tests
{
    [TestClass]
    public class ServiceTests : TestBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            var result = new SpeakerService().RetreiveAllAsync(new RetreiveAllSpeakerRequest()).Result;
        }

        [TestMethod]
        public void TestMethod2()
        {
            var result = TagsService.BuildDataScript();
            Log.Info(result);
        }
    }
}
