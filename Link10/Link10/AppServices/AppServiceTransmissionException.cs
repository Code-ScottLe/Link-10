using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.AppService;

namespace Link10.AppServices
{
    public class AppServiceTransmissionException : Exception
    {
        public AppServiceResponseStatus ResponseStatus
        {
            get; private set;
        }

        public AppServiceTransmissionException(AppServiceResponseStatus responseStatus)
            : this(responseStatus, string.Empty)
        {
        }

        public AppServiceTransmissionException(AppServiceResponseStatus responseStatus, string message)
            : this(responseStatus, message, null)
        {

        }

        public AppServiceTransmissionException(AppServiceResponseStatus responseStatus, string message, Exception innerException)
            : base(message, innerException)
        {
            ResponseStatus = responseStatus;
        }
    }
}
