using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace Link10.AppServiceEcho.AppServices
{
    public interface IIncomingAppServiceConnectionHandler
    {
        void OnIncomingAppServiceConnection(IBackgroundTaskInstance taskInstance);
    }
}
