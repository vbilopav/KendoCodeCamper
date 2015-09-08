using System;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    /// <summary>
    /// Summary description for LogTests
    /// </summary>
    [TestClass]
    public class LogTests : TestBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            //LogConfig.Instance.Type = new EnumConfig<LogType>(LogType.RollingFile);
            Log.Info("Hello from TestMethod1");
        }
    }
}
