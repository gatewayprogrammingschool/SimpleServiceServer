using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GPS.SimpleExtensions;

namespace GPS.SimpleServiceServer.Wcf
{
    public class WcfServiceContainer<TServiceType> : ServiceContainerBase<TServiceType> 
        where TServiceType: class, IService
    {
        protected ServiceHost _host;

        private bool HasAttribute<TAttribute>(TServiceType service) where TAttribute: Attribute
        {
            var result = false;
            var direct = typeof(TServiceType).GetCustomAttribute(typeof(TAttribute));

            if(direct != null)
            {
                result = true;
            }
            else
            {
                foreach(var interf in service.GetType().GetInterfaces())
                {
                    var interAttribute = interf.GetCustomAttribute(typeof(TAttribute));

                    result |= interAttribute != null;
                }
            }

            return result;
        }

        public override bool Initialize(TServiceType service)
        {
            service.AssertParameterNotNull(nameof(service), "A service must be supplied.");

            base.Initialize(service);
            var attribute = HasAttribute<ServiceContractAttribute>(service);
            var behaviorAttribute = HasAttribute<ServiceBehaviorAttribute>(service);

            if (attribute && (service == null || !behaviorAttribute))
            {
                _host = new ServiceHost(typeof(TServiceType));
            }
            else if (attribute && behaviorAttribute)
            {
                _host = new ServiceHost(service);
            }
            else
            {
                return false;
            }

            return true;
        }

        public override bool ForceStop()
        {
            _host?.Abort();

            return _host != null;
        }

        public override bool Pause(TimeSpan timeout)
        {
            if ((_host?.State ?? CommunicationState.Opened) == CommunicationState.Opened ||
               (_host?.State ?? CommunicationState.Opening) == CommunicationState.Opening)
            {
                _host?.Close(timeout);
            }

            return _host != null;
        }

        public override bool Resume()
        {
            if((_host?.State ?? CommunicationState.Closed) == CommunicationState.Closed ||
               (_host?.State ?? CommunicationState.Created) == CommunicationState.Created)
            {
                _host?.Open();
            }

            return _host != null;
        }

        public override bool Start()
        {
            if ((_host?.State ?? CommunicationState.Closed) == CommunicationState.Closed ||
               (_host?.State ?? CommunicationState.Created) == CommunicationState.Created)
            {
                _host?.Open();
            }

            return _host != null;
        }

        public override bool TryStop(TimeSpan timeout)
        {
            if ((_host?.State ?? CommunicationState.Opened) == CommunicationState.Opened ||
               (_host?.State ?? CommunicationState.Opening) == CommunicationState.Opening)
            {
                _host?.Close(timeout);
            }

            return _host != null;
        }

        public static TServiceType CreateService()
        {
            return Activator.CreateInstance<TServiceType>();
        }
    }
}
