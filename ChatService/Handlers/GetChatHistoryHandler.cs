using ChatService.Database;
using Messages.DataTypes.Database.Chat;
using Messages.DataTypes.Database.CompanyDirectory;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.Chat.Responses;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatService.Handlers
{
    
    /// <summary>
    /// This is the handler class for the getting company info. 
    /// This class is created and its methods called by the NServiceBus framework
    /// </summary>
    public class GetChatHistoryHandler : IHandleMessages<GetChatHistoryRequest>
    {
        /// <summary>
        /// This is a class provided by NServiceBus. Its main purpose is to be use log.Info() instead of Messages.Debug.consoleMsg().
        /// When log.Info() is called, it will write to the console as well as to a log file managed by NServiceBus
        /// </summary>
        /// It is important that all logger member variables be static, because NServiceBus tutorials warn that GetLogger<>()
        /// is an expensive call, and there is no need to instantiate a new logger every time a handler is created.
        static ILog log = LogManager.GetLogger<GetChatHistoryRequest>();

        /// <summary>
        /// Searches for company information
        /// </summary>
        /// <param name="message">Information about the company</param>
        /// <param name="context">Used to access information regarding the endpoints used for this handle</param>
        /// <returns>The response to be sent back to the calling process</returns>
        public Task Handle(GetChatHistoryRequest message, IMessageHandlerContext context)
        {
            // TODO: May need to fix
            ChatHistory h = new ChatHistory();
            h.user1 = message.getCommand.history.user1;
            h.user2 = message.getCommand.history.user2;
            h.messages = ChatDatabase.getInstance().getChats(message.getCommand.history);
            GetChatHistory hist = new GetChatHistory();
            hist.history = h;

            if (h.messages.Count > 0)
                return context.Reply(new GetChatHistoryResponse(true, "Successfully retrieved chats from database.", hist));
            else
                return context.Reply(new GetChatHistoryResponse(false, "Could not retrieve chats from database.", hist));
        }
    }
}
