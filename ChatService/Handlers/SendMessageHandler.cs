﻿using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace ChatService.Handlers
{

    /// <summary>
    /// This is the handler class for the getting getting company names. 
    /// This class is created and its methods called by the NServiceBus framework
    /// </summary>
    public class SendMessageHandler : IHandleMessages<SendMessageRequest>
    {
        /// <summary>
        /// This is a class provided by NServiceBus. Its main purpose is to be use log.Info() instead of Messages.Debug.consoleMsg().
        /// When log.Info() is called, it will write to the console as well as to a log file managed by NServiceBus
        /// </summary>
        /// It is important that all logger member variables be static, because NServiceBus tutorials warn that GetLogger<>()
        /// is an expensive call, and there is no need to instantiate a new logger every time a handler is created.
        static ILog log = LogManager.GetLogger<SendMessageRequest>();

        /// <summary>
        /// Searches for companys
        /// </summary>
        /// <param name="message">Information about the company</param>
        /// <param name="context">Used to access information regarding the endpoints used for this handle</param>
        /// <returns>The response to be sent back to the calling process</returns>
        public Task Handle(SendMessageRequest message, IMessageHandlerContext context)
        {
            // TODO: May need to fix
            if (Database.ChatDatabase.getInstance().saveChat(message))
                return context.Reply(new ServiceBusResponse(true, "Successfully saved message."));
            else
                return context.Reply(new ServiceBusResponse(false, "Could not save chat to database."));
        }
    }
}
