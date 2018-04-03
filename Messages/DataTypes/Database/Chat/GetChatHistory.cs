using NServiceBus;

using Messages.DataTypes.Database.Chat;

using System;

namespace Messages.DataTypes.Database.Chat
{
    /// <summary>
    /// This class represents a request for the chat history between two users
    /// </summary>
    [Serializable]
    public class GetChatHistory
    {
        /// <summary>
        /// The chat history between these two users.
        /// </summary>
        public ChatHistory history { get; set; }
    }
}
