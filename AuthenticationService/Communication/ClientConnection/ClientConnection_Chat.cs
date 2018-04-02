using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages.NServiceBus.Commands;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Chat;
using Messages.ServiceBusRequest.Chat.Requests;
using NServiceBus;

namespace AuthenticationService.Communication
{
    /// <summary>
    /// This portion of the class contains client connection functionality pertaining to the company listings service.
    /// </summary>
    //TODO: May need to fix this
    partial class ClientConnection
    {
        private ServiceBusResponse chatRequest(ChatServiceRequest chatRequest)
        {
            switch(chatRequest.requestType)
            {
                case (ChatRequest.GetChatContacts):
                    return getChatContacts((GetChatContactsRequest)chatRequest);
                case (ChatRequest.GetChatHistory):
                    return getChatHistory((GetChatHistoryRequest)chatRequest);
                case (ChatRequest.SendMessage):
                    return sendChatMessage((SendMessageRequest)chatRequest);
                default:
                    return new ServiceBusResponse(false, "No results could be found pertaining to your search");
            }
        }

        private ServiceBusResponse getChatContacts(GetChatContactsRequest getChatContactsRequest)
        {
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("Chat");
            return requestingEndpoint.Request<ServiceBusResponse>(getChatContactsRequest, sendOptions).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private ServiceBusResponse getChatHistory(GetChatHistoryRequest getChatHistoryRequest)
        {
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("Chat");
            return requestingEndpoint.Request<ServiceBusResponse>(getChatHistoryRequest, sendOptions).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private ServiceBusResponse sendChatMessage(SendMessageRequest sendMessageRequest)
        {
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("Chat");
            return requestingEndpoint.Request<ServiceBusResponse>(sendMessageRequest, sendOptions).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
