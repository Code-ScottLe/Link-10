using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;

using Windows.ApplicationModel.AppService;

namespace Link10.AppServices
{
    public class CallRoutingService
    {
        private Dictionary<string, Type> _endpointHandlerTypes;

        public CallRoutingService()
        {
            _endpointHandlerTypes = new Dictionary<string, Type>();

            // Self-reflect.
            IEnumerable<Type> receivers =
                Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.IsAssignableFrom(typeof(ControllerBase))
                        && t.CustomAttributes.Any(c => c.AttributeType == typeof(RouteAttribute)));
            foreach (Type t in receivers)
            {
                // TO DO: Self-parsing here for the case that doesn't override.
                var routeAttribute = t.GetCustomAttribute(typeof(RouteAttribute)) as RouteAttribute;
                if (!string.IsNullOrWhiteSpace(routeAttribute.AppServiceName))
                {
                    _endpointHandlerTypes[routeAttribute.AppServiceName] = t;
                }
            }

            // Register for the handler
            AppServiceHost.Instance.ConnectionObservable.Where(c => _endpointHandlerTypes.ContainsKey(c.AppServiceName)).Subscribe(RouteIncomingConnection);
        }

        private void RouteIncomingConnection(AppServiceConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
