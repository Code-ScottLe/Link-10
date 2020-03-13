using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace Link10.AppServices.Routing
{
    public abstract class RoutedAppServiceInitiator : AppServiceInitiator
    {
        public RoutedAppServiceInitiator(string appServiceName, string receiverPackageFamilyName) 
            : base(appServiceName, receiverPackageFamilyName)
        {
        }

        protected override async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            AppServiceDeferral deferral = args.GetDeferral();

            ValueSet set = args.Request.Message;
            Uri uri = new Uri(set["url"] as string);

            try
            {
                await this.RouteCallAsync(uri, set, args.Request);
            }
            catch (Exception e)
            {
                string json = JsonConvert.SerializeObject(e);

                await SendResponseMessageAsync(args.Request, ("result", -1), ("exception", json));
            }
            
            finally
            {
                deferral.Complete();
            } 
        }
    }
}
