using System;
using System.Globalization;
using System.Threading;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{ 
    public class TestBase
    {
        public TestBase()
        {            
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("hr-HR");
        }
    }
}
