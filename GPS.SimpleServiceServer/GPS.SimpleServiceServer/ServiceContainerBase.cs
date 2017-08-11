using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.SimpleServiceServer
{
    public abstract class ServiceContainerBase<TServiceType> where TServiceType: class, IService
    {
        public TServiceType _service { get; set; }

        public virtual bool Initialize(TServiceType service)
        {
            _service = service;

            return true;
        }

        public abstract bool Start();

        public abstract bool Pause(TimeSpan timeout);

        public abstract bool Resume();

        public abstract bool TryStop(TimeSpan timeout);

        public abstract bool ForceStop();
    }
}
