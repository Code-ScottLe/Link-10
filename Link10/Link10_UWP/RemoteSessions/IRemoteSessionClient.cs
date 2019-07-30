using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link10.RemoteSessions
{
    public interface IRemoteSessionClient : IRemoteSessionParticipant
    {
        Task ConnectAsync();
    }
}
