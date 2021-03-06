﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.System.RemoteSystems;

namespace Link10.AppServices
{
    public interface IAppServiceConnectionInitiator : IAppServiceConnectionEndPoint
    {
        Task OpenConnectionAsync();

        Task OpenConnectionRemotelyAsync(RemoteSystem remoteSystem);
    }
}
