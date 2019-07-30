using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Link10.AppServices
{
    public interface IAppServiceConnectionEndPoint
    {
        string AppServiceName { get; }

        bool ConnectionAlive { get; }

        string RequestorPackageFamilyName { get; }

        event TypedEventHandler<IAppServiceConnectionEndPoint, AppServiceEndPointTerminationReason> AppServiceConnectionTeminated;

        Task CloseConnectionAsync(AppServiceEndPointTerminationReason reason);

        Task<ValueSet> SendMessageAsync(params (string key, object value)[] quickMessage);

        Task<ValueSet> SendMessageAsync(ValueSet message);

        Task SendResponseMessageAsync(AppServiceRequest request, params (string key, object value)[] quickResponse);

        Task SendResponseMessageAsync(AppServiceRequest request, ValueSet response);
    }
}
