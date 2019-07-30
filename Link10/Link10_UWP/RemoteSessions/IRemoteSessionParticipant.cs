using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.RemoteSystems;

namespace Link10.RemoteSessions
{
    public interface IRemoteSessionParticipant
    {
        bool IsConnected { get; }

        RemoteSystemSessionMessageChannel CreateChannel(string channelName, RemoteSystemSessionMessageChannelReliability channelReliability, TypedEventHandler<RemoteSystemSessionMessageChannel, RemoteSystemSessionValueSetReceivedEventArgs> callback);

        void Disconnect();

        Task SendMessageToAllParticipantsAsync(RemoteSystemSessionMessageChannel channel, ValueSet data);

        Task SendMessageToAllParticipantsAsync(string channelName, ValueSet data);

        Task SendMessageAsync(string channelName, RemoteSystemSessionParticipant receiver, ValueSet data);

        Task SendMessageAsync(RemoteSystemSessionMessageChannel channel, RemoteSystemSessionParticipant receiver, ValueSet data);
    }
}
