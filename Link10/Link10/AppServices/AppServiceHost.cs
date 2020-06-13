using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Subjects;
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
        private static AppServiceHost _instance;

        public static AppServiceHost Instance
        {
            get
            {
                if (_instance != null)
                {
                    _instance = new AppServiceHost();
                }

                return _instance;
            }
        }

        private Subject<AppServiceConnection> _connectionObservable;

        public IObservable<AppServiceConnection> ConnectionObservable
        {
            get => _connectionObservable;
        }

        private AppServiceHost()
        {
            _connectionObservable = new Subject<AppServiceConnection>();
        }

        public void HandleIncomingRequest(IBackgroundTaskInstance instance, AppServiceTriggerDetails triggerDetails)
        {
            if(_connectionObservable.HasObservers)
            {
                _connectionObservable.OnNext(triggerDetails.AppServiceConnection);
            }
            else
            {
                // WARNING: Data lost. No observer/handler. Quit it.
                Debug.WriteLine($"FATAL: Dropping connection inbound for {triggerDetails.Name} from pfn: {triggerDetails.CallerPackageFamilyName}.");
                triggerDetails.AppServiceConnection.Dispose();
            }
        }
    }

}
