using System;
using System.Collections.Generic;
using System.Text;

using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;

namespace Link10.AppServices
{
    /// <summary>
    /// Responsible for hosting/handling all incoming app services request.
    /// </summary>
    public class AppServiceHost
    {
        public void HandleIncomingRequest(IBackgroundTaskInstance instance, AppServiceTriggerDetails triggerDetails)
        {
        }
    }

}
