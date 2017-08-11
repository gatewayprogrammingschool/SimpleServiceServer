using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPS.SimpleServiceServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace GPS.SimpleServiceServer.Tests
{
    [TestClass()]
    public class ServiceContainerBaseTests
    {
        [TestMethod()]
        public void StartTest()
        {
            var container = new Wcf.WcfServiceContainer<TestService>();

            Assert.IsNotNull(container);

            var service = Wcf.WcfServiceContainer<TestService>.CreateService();
            var initialized = container.Initialize(service);

            Assert.IsTrue(initialized);

            Assert.IsTrue(container.Start());

            container.ForceStop();
        }

        [TestMethod()]
        public void TryStopTest()
        {
            var container = new Wcf.WcfServiceContainer<TestService>();

            Assert.IsNotNull(container);

            var service = Wcf.WcfServiceContainer<TestService>.CreateService();
            var initialized = container.Initialize(service);

            Assert.IsTrue(initialized);

            Assert.IsTrue(container.Start());

            Assert.IsTrue(container.TryStop(TimeSpan.FromSeconds(5)));
        }

        [TestMethod()]
        public void ForceStopTest()
        {
            var container = new Wcf.WcfServiceContainer<TestService>();

            Assert.IsNotNull(container);

            var service = Wcf.WcfServiceContainer<TestService>.CreateService();
            var initialized = container.Initialize(service);

            Assert.IsTrue(initialized);

            Assert.IsTrue(container.Start());

            Assert.IsTrue(container.ForceStop());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes =true)]
        public void NullServiceTest()
        {
            var container = new Wcf.WcfServiceContainer<TestService>();

            Assert.IsNotNull(container);

            var initialized = container.Initialize(null);
        }
    }
}