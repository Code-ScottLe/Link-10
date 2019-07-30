using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.System.RemoteSystems;

namespace Link10.AppServices
{
    public abstract class AppServiceConnectionInitiator : AppServiceConnectionEndPoint, IAppServiceConnectionInitiator
    {
        public AppServiceConnectionInitiator(string appServiceName, string receiverPackageFamilyName)
            : base(new AppServiceConnection() { AppServiceName = appServiceName, PackageFamilyName = receiverPackageFamilyName }, false)
        {
            ConnectionAlive = false;
        }

        public virtual async Task OpenConnectionAsync()
        {
            if (!ConnectionAlive)
            {
                AppServiceConnectionStatus status = await Connection.OpenAsync();
                if (status == AppServiceConnectionStatus.Success)
                {
                    ConnectionAlive = true;
                }
            }
        }

        public virtual async Task OpenConnectionRemotelyAsync(RemoteSystem remoteSystem)
        {
            if (!ConnectionAlive)
            {
                var connectionRequest = new RemoteSystemConnectionRequest(remoteSystem);
                AppServiceConnectionStatus status = await Connection.OpenRemoteAsync(connectionRequest);

                if (status == AppServiceConnectionStatus.Success)
                {
                    ConnectionAlive = true;
                }
            }
        }
    }
}
