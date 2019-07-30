using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.RemoteSystems;

namespace Link10.RemoteSessions
{
    public interface IRemoteSessionHost : IRemoteSessionParticipant
    {
        Task SendInvitationAsync(RemoteSystem remoteDevice);

        Task StartSessionAsync();
    }
}
