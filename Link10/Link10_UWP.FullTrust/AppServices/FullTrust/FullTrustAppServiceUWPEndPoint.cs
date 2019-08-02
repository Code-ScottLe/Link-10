using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Metadata;

namespace Link10.AppServices.FullTrust
{
    public abstract class FullTrustAppServiceUWPEndPoint : AppServiceConnectionEndPoint
    {
        protected FullTrustAppServiceUWPEndPoint(AppServiceConnection connection) 
            : base(connection, true) // UWP always listen to active connection from Win32. 
        {
        }

        public static async Task InvokeWin32EndpointAsync()
        {
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            }
            else
            {
                throw new InvalidOperationException("FullTrustAppContract is not present.");
            }
        }
    }
}
