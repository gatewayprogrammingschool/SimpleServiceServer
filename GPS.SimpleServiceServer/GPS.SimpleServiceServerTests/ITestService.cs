using GPS.SimpleServiceServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GPS.SimpleServiceServer.Tests
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITestService : IService
    {
        [OperationContract]
        void DoWork();
    }
}
