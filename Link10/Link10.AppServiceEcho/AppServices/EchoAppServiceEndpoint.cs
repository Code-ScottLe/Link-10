using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Link10.AppServices;
using Link10.AppServices.Routing;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;

namespace Link10.AppServiceEcho.AppServices
{
    [AppServiceHandler(AppServiceName = "com.link10.echoservice")]
    public class EchoAppServiceEndpoint : RoutedAppServiceReceiver
    {
        public EchoAppServiceEndpoint(IBackgroundTaskInstance instance, AppServiceTriggerDetails appServiceTriggerDetails)
            : base(instance, appServiceTriggerDetails)
        {
        }

        internal Task<object> IndexAsync(ValueSet data)
        {
            return Task.FromResult<object>(data);
        }
    }
}
