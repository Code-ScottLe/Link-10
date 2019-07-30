using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Link10.AppServices
{
    public abstract class AppServiceConnectionEndPoint : IAppServiceConnectionEndPoint
    {
        public string AppServiceName
        {
            get; private set;
        }

        public string RequestorPackageFamilyName
        {
            get; private set;
        }

        protected AppServiceConnection Connection
        {
            get; set;
        }

        public bool ConnectionAlive
        {
            get; protected set;
        }

        public event TypedEventHandler<IAppServiceConnectionEndPoint, AppServiceEndPointTerminationReason> AppServiceConnectionTeminated;

        protected AppServiceConnectionEndPoint(AppServiceConnection connection, bool isConnectionAlreadyAlive)
        {
            ConnectionAlive = isConnectionAlreadyAlive;
            Connection = connection;
            Connection.RequestReceived += OnRequestReceived;
            Connection.ServiceClosed += OnConnectionClosed;
            AppServiceName = Connection.AppServiceName;
            RequestorPackageFamilyName = Connection.PackageFamilyName;
        }

        public Task<ValueSet> SendMessageAsync(params (string key, object value)[] quickMessage)
        {
            ValueSet valueSet = new ValueSet();
            foreach (var t in quickMessage)
            {
                valueSet.Add(t.key, t.value);
            }

            return SendMessageAsync(valueSet);
        }

        public virtual async Task<ValueSet> SendMessageAsync(ValueSet message)
        {
            if (ConnectionAlive)
            {
                AppServiceResponse response = await Connection.SendMessageAsync(message);
                if (response.Status == AppServiceResponseStatus.Success)
                {
                    return response.Message;
                }
                else
                {
                    throw new AppServiceTransmissionException(response.Status, $"Failed to send message. Status: {response.Status}");
                }
            }
            else
            {
                throw new InvalidOperationException("Connection is not live! Re-establish connection before sending message.");
            }
        }

        public Task SendResponseMessageAsync(AppServiceRequest request, params (string key, object value)[] quickResponse)
        {
            ValueSet valueSet = new ValueSet();
            foreach (var t in quickResponse)
            {
                valueSet.Add(t.key, t.value);
            }

            return SendResponseMessageAsync(request, valueSet);
        }

        public virtual async Task SendResponseMessageAsync(AppServiceRequest request, ValueSet response)
        {
            if (ConnectionAlive)
            {
                AppServiceResponseStatus status = await request.SendResponseAsync(response);

                switch (status)
                {
                    case AppServiceResponseStatus.Success:
                        break;
                    default:
                        throw new AppServiceTransmissionException(status, $"Failed to transmit response. Reason: {status}");
                }
            }
            else
            {
                throw new InvalidOperationException("Connection is not live! Re-establish connection before sending message.");
            }
        }

        protected virtual void OnConnectionClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            CloseConnectionAsync(AppServiceEndPointTerminationReason.RequestorTerminate);
        }

        protected abstract void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args);

        public virtual Task CloseConnectionAsync(AppServiceEndPointTerminationReason reason)
        {
            Connection?.Dispose();
            Connection = null;

            ConnectionAlive = false;

            AppServiceConnectionTeminated?.Invoke(this, reason);

            return Task.CompletedTask;
        }
    }
}