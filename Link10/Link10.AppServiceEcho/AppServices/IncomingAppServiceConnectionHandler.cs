using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Link10.AppServices;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;

namespace Link10.AppServiceEcho.AppServices
{
    public class IncomingAppServiceConnectionHandler : IIncomingAppServiceConnectionHandler
    {
        private static List<IAppServiceConnectionEndPoint> ConnectionEndPoints
        {
            get; set;
        }

        static IncomingAppServiceConnectionHandler()
        {
            ConnectionEndPoints = new List<IAppServiceConnectionEndPoint>();
        }

        public IncomingAppServiceConnectionHandler()
        {
        }

        public void OnIncomingAppServiceConnection(IBackgroundTaskInstance taskInstance)
        {
            var appService = taskInstance.TriggerDetails as AppServiceTriggerDetails;

            IAppServiceConnectionEndPoint endPoint = CreateEndPoint(taskInstance, appService);
            if (endPoint != null)
            {
                endPoint.AppServiceConnectionTerminated += OnEndPointTerminated;
                ConnectionEndPoints.Add(endPoint);
            }
        }

        public async void HandleAppSuspending(SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

            IEnumerable<Task> cancelingTasks = ConnectionEndPoints.Select(ep => ep.CloseConnectionAsync(AppServiceEndPointTerminationReason.AppSuspending));

            await Task.WhenAll(cancelingTasks);

            ConnectionEndPoints.Clear();

            deferral.Complete();
        }

        private void OnEndPointTerminated(IAppServiceConnectionEndPoint endPoint, AppServiceEndPointTerminationReason reason)
        {
            ConnectionEndPoints.Remove(endPoint);
            endPoint.AppServiceConnectionTerminated -= OnEndPointTerminated;
        }

        private IAppServiceConnectionEndPoint CreateEndPoint(IBackgroundTaskInstance taskInstance, AppServiceTriggerDetails triggerDetails)
        {
            // Switch this to a abstract factory pattern to leverage dependency injection if needed.
            switch (triggerDetails.Name)
            {
                case "com.link10.echoservice":
                    return new EchoAppServiceConnectionEndpoint(taskInstance, triggerDetails);
                default:
                    return null;
            }
        }
    }
}
