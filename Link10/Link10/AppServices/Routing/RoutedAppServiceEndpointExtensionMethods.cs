using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace Link10.AppServices.Routing
{
    internal static class RoutedAppServiceEndpointExtensionMethods
    {
        public static async Task RouteCallAsync(this AppServiceEndpoint endpoint, Uri uri, ValueSet data, AppServiceRequest request)
        {
            if (!uri.IsValidAppServiceUri())
            {
                await endpoint.SendResponseMessageAsync(request, ("error", $"Invalid Uri"));
                return;
            }

            // Verify if right one.
            var attribute = endpoint.GetType().GetCustomAttribute(typeof(AppServiceHandlerAttribute)) as AppServiceHandlerAttribute;
            string appServiceName = uri.GetAppServiceName();
            if (attribute.AppServiceName != appServiceName)
            {
                // wrong app service.
                await endpoint.SendResponseMessageAsync(request, ("error", $"Wrong app service routed. Request app service: {uri.GetAppServiceName()} | Current connection: {attribute.AppServiceName}"));
                return;
            }

            // Check if we have method.
            string methodName = uri.GetRoutedCall();

            if (string.IsNullOrWhiteSpace(methodName))
            {
                // Default.
                methodName = "IndexAsync";
            }


            MethodInfo methodInfo = endpoint.GetType().GetMethods().FirstOrDefault(m => m.Name == methodName);

            if (methodInfo == null)
            {
                await endpoint.SendResponseMessageAsync(request, ("error", $"Can't find the method. requesting: {methodName} not found in appservice {appServiceName}"));
                return;
            }

            // check args.
            ParameterInfo[] paramInfos = methodInfo.GetParameters();
            object[] parameters = new object[paramInfos?.Length ?? 0];
            for (int i = 0; i <= paramInfos.Length; i++)
            {
                ParameterInfo paramInfo = paramInfos[i];

                if (paramInfo.ParameterType == typeof(ValueSet))
                {
                    parameters[i] = data;
                }

                // try to look in payload
                else if (data.TryGetValue(paramInfo.Name, out object value))
                {
                    // check type to see if we need to deseralize
                    if (paramInfo.ParameterType.IsPrimitive)
                    {
                        parameters[i] = value;
                    }
                    else
                    {
                        parameters[i] = JsonConvert.DeserializeObject((string)value, paramInfo.ParameterType);
                    }
                }

                else if (paramInfo.HasDefaultValue)
                {
                    // has default.
                    parameters[i] = paramInfo.DefaultValue;
                }
                else
                {
                    // missing.
                    await endpoint.SendResponseMessageAsync(request, ("error", $"Missing args name: {paramInfo.Name}"));
                    return;
                }
            }

            // make it this far? Call it. 
            object returnValue = methodInfo.Invoke(endpoint, parameters.Length > 0 ? parameters : null);

            if (methodInfo.ReturnType != typeof(void) && methodInfo.ReturnType != Type.GetType("System.Threading.Tasks.VoidTaskResult"))
            {
                // Check if we have to pull from Task.
                object actualReturnValue;
                if (typeof(Task).IsAssignableFrom(methodInfo.ReturnType))
                {
                    // Task type.
                    actualReturnValue = await ((Task)returnValue).AsObjectTaskResult();
                }
                else
                {
                    actualReturnValue = returnValue;
                }

                if (actualReturnValue == null)
                {
                    await endpoint.SendResponseMessageAsync(request, ("status", 0));
                }
                else if (actualReturnValue is ValueSet fixedSet)
                {
                    await endpoint.SendResponseMessageAsync(request, fixedSet);
                }
                else if (actualReturnValue.GetType().IsPrimitive)
                {
                    // just go.
                    await endpoint.SendResponseMessageAsync(request, ("status", 0), ("returns", returnValue));
                }
                else
                {
                    // serialize.
                    string serialized = JsonConvert.SerializeObject(actualReturnValue);
                    await endpoint.SendResponseMessageAsync(request, ("status", 0), ("returns", serialized));
                }
            }

        }
    }
}
