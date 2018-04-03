using System;

namespace Messages.DataTypes.Database.Chat
{
    
    /// <summary>
    /// This class represents a single chat message sent from one user to another.
    /// </summary>
    [Serializable]
    public partial class ChatMessage : IComparable
    {
        /// <summary>
        /// The username of the person who sent the message
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// The username of the person who the message was sent to
        /// </summary>
        public string receiver { get; set; }

        /// <summary>
        /// A unix timestamp representing when the message was sent, recorded on the clients machine at the time of sending
        /// </summary>
        public int unix_timestamp { get; set; } = -1;

        /// <summary>
        /// The content of the message
        /// </summary>
        public string messageContents { get; set; }

        public int CompareTo(Object obj)
        {
            ChatMessage temp = (ChatMessage)obj;
            if (this.unix_timestamp > temp.unix_timestamp)
                return 1;
            else
                return -1;
        }
    }
}
