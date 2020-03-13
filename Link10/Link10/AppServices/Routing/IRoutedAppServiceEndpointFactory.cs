using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Link10.AppServices.Routing
{
    public interface IRoutedAppServiceEndpointFactory
    {
        Task<IAppServiceEndpoint> CreateEndpointAsync(Uri uri);
    }
}
