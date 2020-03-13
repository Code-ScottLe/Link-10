using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Link10.AppServices.Routing;
using Windows.Foundation.Collections;

namespace Link10.AppServices
{
    public class AppServiceClient
    {
        private IRoutedAppServiceEndpointFactory EndpointFactory
        {
            get; set;
        }

        public AppServiceClient()
            : this(AppServiceHost.Instance)
        {
        }

        public AppServiceClient(IRoutedAppServiceEndpointFactory factory)
        {
            EndpointFactory = factory;
        }

        public Task<ValueSet> SendAsync(string url, params (string key, object value)[] package)
        {
            return SendAsync(new Uri(url), package);
        }

        public Task<ValueSet> SendAsync(Uri uri, params (string key, object value)[] package)
        {
            ValueSet value = new ValueSet();
            foreach (var combo in package)
            {
                value[combo.key] = combo.value;
            }

            return SendAsync(uri, value);
        }

        public Task<ValueSet> SendAsync(string url, IEnumerable<KeyValuePair<string, object>> package)
        {
            return SendAsync(new Uri(url), package);
        }

        public Task<ValueSet> SendAsync(Uri uri, IEnumerable<KeyValuePair<string, object>> package)
        {
            ValueSet value = new ValueSet();
            foreach (var combo in package)
            {
                value[combo.Key] = combo.Value;
            }

            return SendAsync(uri, value);
        }

        public Task<ValueSet> SendAsync(string url, ValueSet package)
        {
            return SendAsync(new Uri(url), package);
        }

        public async Task<ValueSet> SendAsync(Uri uri, ValueSet package)
        {
            IAppServiceEndpoint endpoint = await EndpointFactory.CreateEndpointAsync(uri);

            ValueSet value = await endpoint.SendMessageAsync(package);

            return value;
        }
    }
}
