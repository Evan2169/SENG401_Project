using Messages.DataTypes.Database.CompanyDirectory;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.Chat.Responses;
using Messages.DataTypes.Database.Chat;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChatService.Database;

namespace ChatService.Handlers
{
    
    /// <summary>
    /// This is the handler class for the getting company info. 
    /// This class is created and its methods called by the NServiceBus framework
    /// </summary>
    public class GetChatContactsHandler : IHandleMessages<GetChatContactsRequest>
    {
        /// <summary>
        /// This is a class provided by NServiceBus. Its main purpose is to be use log.Info() instead of Messages.Debug.consoleMsg().
        /// When log.Info() is called, it will write to the console as well as to a log file managed by NServiceBus
        /// </summary>
        /// It is important that all logger member variables be static, because NServiceBus tutorials warn that GetLogger<>()
        /// is an expensive call, and there is no need to instantiate a new logger every time a handler is created.
        static ILog log = LogManager.GetLogger<GetChatContactsRequest>();

        /// <summary>
        /// Searches for company information
        /// </summary>
        /// <param name="message">Information about the company</param>
        /// <param name="context">Used to access information regarding the endpoints used for this handle</param>
        /// <returns>The response to be sent back to the calling process</returns>
        public Task Handle(GetChatContactsRequest message, IMessageHandlerContext context)
        {
            GetChatContactsResponse response;
            // TODO: May need to fix. Currently hardcoded to always retrieve client contacts.
            GetChatContacts conts = new GetChatContacts(message.getCommand.usersname, ChatDatabase.getInstance().getContacts(message.getCommand.usersname));
            
            if(conts.contactNames.Count == 0)
                response = new GetChatContactsResponse(false, "Could not find any contacts.", conts);
            else
                response = new GetChatContactsResponse(true, "Successfully retrieved contacts.", conts);

            return context.Reply(response);
        }
    }
}
