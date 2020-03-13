using System;
using System.Collections.Generic;
using System.Text;

namespace Link10.AppServices
{
    public enum AppServiceEndpointTerminationReason
    {
        RequestorTerminate = 0,
        BackgroundTaskCancelled,
        AppSuspending,
        GenericError,
        Stale
    }
}
