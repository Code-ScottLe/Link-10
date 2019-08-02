using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Link10.AppServices;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;

namespace Link10.AppServices.FullTrust
{
    public abstract class FullTrustAppServiceWin32Endpoint : AppServiceConnectionInitiator
    {
        public FullTrustAppServiceWin32Endpoint(string fullTrustAppServiceName)
            : base (fullTrustAppServiceName, Package.Current.Id.FamilyName)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        protected virtual void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
