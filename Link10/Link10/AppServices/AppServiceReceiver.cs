using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;

namespace Link10.AppServices
{
    public abstract class AppServiceReceiver : AppServiceEndpoint
    {
        protected BackgroundTaskDeferral ServiceDeferral
        {
            get; set;
        }

        public AppServiceReceiver(IBackgroundTaskInstance instance, AppServiceTriggerDetails appServiceTriggerDetails)
            : base(appServiceTriggerDetails.AppServiceConnection, true)
        {
            ConnectionAlive = true;
            ServiceDeferral = instance.GetDeferral();
            instance.Canceled += OnAppServiceCancelled;
        }

        /// <summary>
        /// Fired when the background task responsible for the app service connection has been cancelled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        protected virtual void OnAppServiceCancelled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            var ignored = CloseConnectionAsync(AppServiceEndpointTerminationReason.BackgroundTaskCancelled);
        }

        public override async Task CloseConnectionAsync(AppServiceEndpointTerminationReason reason)
        {
            await base.CloseConnectionAsync(reason);

            ServiceDeferral?.Complete();
            ServiceDeferral = null;
        }
    }
}
