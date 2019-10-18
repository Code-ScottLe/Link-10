using System;
using System.Collections.Generic;
using System.Text;

namespace Link10.AppServices
{
    public enum AppServiceEndPointTerminationReason
    {
        RequestorTerminate = 0,
        BackgroundTaskCancelled,
        AppSuspending,
        GenericError,
        Stale
    }
}
