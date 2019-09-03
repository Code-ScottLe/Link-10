using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Link10.AppServices;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;

namespace Link10.AppServiceEcho.AppServices
{
    public class EchoAppServiceConnectionEndpoint : AppServiceConnectionReceiver
    {
        public EchoAppServiceConnectionEndpoint(IBackgroundTaskInstance instance, AppServiceTriggerDetails appServiceTriggerDetails)
            : base(instance, appServiceTriggerDetails)
        {
        }

        protected override async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            // Echo service. Send back what we received.
            AppServiceDeferral defferal = args.GetDeferral();

            await SendResponseMessageAsync(args.Request, args.Request.Message);

            defferal.Complete();
        }
    }
}
