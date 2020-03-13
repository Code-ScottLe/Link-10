using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Link10.AppServices.Routing;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;

namespace Link10.AppServices
{
    /// <summary>
    /// Responsible for hosting/handling all incoming app services request.
    /// </summary>
    public class AppServiceHost : IRoutedAppServiceEndpointFactory
    {
        private static Dictionary<string, Type> _endpointHandlerTypes;

        private List<AppServiceConnection> _connections;

        private static AppServiceHost _instance;

        public static AppServiceHost Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppServiceHost();
                }

                return _instance;
            }
        }

        static AppServiceHost()
        {
            _endpointHandlerTypes = new Dictionary<string, Type>();

            // Self-reflect.
            IEnumerable<Type> receivers = 
                Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.IsAssignableFrom(typeof(AppServiceReceiver)) 
                        && t.CustomAttributes.Any(c => c.AttributeType == typeof(AppServiceHandlerAttribute)));
            foreach(Type t in receivers)
            {
                // TO DO: Self-parsing here for the case that doesn't override.
                var handlerAttribute = t.GetCustomAttribute(typeof(AppServiceHandlerAttribute)) as AppServiceHandlerAttribute;
                if (!string.IsNullOrWhiteSpace(handlerAttribute.AppServiceName))
                {
                    _endpointHandlerTypes[handlerAttribute.AppServiceName] = t;
                }
            }
        }

        private AppServiceHost()
        {
            _connections = new List<AppServiceConnection>();
        }

        public void HandleIncomingRequest(IBackgroundTaskInstance instance, AppServiceTriggerDetails triggerDetails)
        {
            if (_endpointHandlerTypes.TryGetValue(triggerDetails.Name, out Type handlerType))
            {
                var receiver = Activator.CreateInstance(handlerType, instance, triggerDetails) as IAppServiceEndpoint;
                receiver.AppServiceConnectionTerminated += OnAppServiceConnectionTerminated;
            }
            else
            {
                // doesn't match with anything. Terminate.
                triggerDetails.AppServiceConnection.Dispose();
            }
        }

        private void OnAppServiceConnectionTerminated(IAppServiceEndpoint sender, AppServiceEndpointTerminationReason args)
        {
            throw new NotImplementedException();
        }

        async Task<IAppServiceEndpoint> IRoutedAppServiceEndpointFactory.CreateEndpointAsync(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}
