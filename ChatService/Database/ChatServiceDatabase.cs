using Messages;
using Messages.Database;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.DataTypes.Database.CompanyDirectory;

using MySql.Data.MySqlClient;

using System;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using System.Collections;
using System.Collections.Generic;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.DataTypes.Database.Chat;

namespace ChatService.Database
{
    /// <summary>
    /// This portion of the class contains methods and functions
    /// </summary>
    public partial class ChatDatabase : AbstractDatabase
    {
        /// <summary>
        /// Private default constructor to enforce the use of the singleton design pattern
        /// </summary>
        private ChatDatabase() { }

        /// <summary>
        /// Gets the singleton instance of the database
        /// </summary>
        /// <returns>The singleton instance of the database</returns>
        public static ChatDatabase getInstance()
        {
            if (instance == null)
            {
                instance = new ChatDatabase();
            }
            return instance;
        }

        // TODO: Complete necessary functions for chat database

        /// <summary>
        /// This function contacts the database and returns a list of all recipients of messages sent by 'user'
        /// </summary>
        /// <param name="user">Name of the user to retrieve sent messages of.</param>
        /// <returns>List of contacts.</returns>
        public List<string> getContacts(string user)
        {
            List<string> toReturn = new List<string>();
            if (openConnection())
            {
                try
                {
                    string query = @"SELECT DISTINCT RECEIVER FROM " + dbname + @".CHAT WHERE SENDER = '" + user + @"';";

                    MySqlCommand com = new MySqlCommand(query, connection);
                    MySqlDataReader red = com.ExecuteReader();

                    while (red.Read())
                    {
                        toReturn.Add(red.GetString(0));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Issue retrieving contacts from database.");
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    closeConnection();
                }

                return toReturn;
            }
            else
            {
                throw new Exception("Could not connect to database.");
            }
        }

        /// <summary>
        /// Saves a chat message to the chat database
        /// </summary>
        /// <param name="chat">Contains the information to save.</param>
        /// <returns>returns true if chat successfully saved to database, false otherwise.</returns>
        public bool saveChat(SendMessageRequest chat)
        {
            if (openConnection())
            {
                try
                {
                    string query = @"INSERT INTO CHAT(SENDER, RECEIVER, MESSAGE, TIMESTAMP) VALUES('" + chat.message.sender
                        + @"', '" + chat.message.receiver + @"', '" + chat.message.messageContents + @"', '" + chat.message.unix_timestamp + @"');";
                    Console.WriteLine(query);
                    MySqlCommand com = new MySqlCommand(query, connection);
                    if (com.ExecuteNonQuery() >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Issue saving chat to database.");
                }
                finally
                {
                    closeConnection();
                }
            }
            else
            {
                throw new Exception("Could not connect to database.");
            }
            return false;
        }

        public List<ChatMessage> getChats(ChatHistory hist)
        {
            List<ChatMessage> toReturn = new List<ChatMessage>();

            if (openConnection())
            {
                try
                {
                    string query = @"SELECT MESSAGE FROM " + dbname + @".CHAT WHERE SENDER = '" + hist.user1 + @"' AND RECEIVER = '" + hist.user2 + "';";

                    Console.WriteLine(query);

                    MySqlCommand com = new MySqlCommand(query, connection);
                    MySqlDataReader red = com.ExecuteReader();

                    while (red.Read())
                    {
                        ChatMessage temp = new ChatMessage();
                        temp.sender = hist.user1;
                        temp.receiver = hist.user2;
                        temp.messageContents = red.GetString(0);
                        toReturn.Add(temp);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Issue retrieving messages from database.");
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    closeConnection();
                }
            }
            else
            {
                throw new Exception("Could not connect to database.");
            }

            return toReturn;
        }
    }

    /// <summary>
    /// This portion of the class contains the properties and variables 
    /// </summary>
    public partial class ChatDatabase : AbstractDatabase
    {
        /// <summary>
        /// The name of the database.
        /// Both of these properties are required in order for both the base class and the
        /// table definitions below to have access to the variable.
        /// </summary>
        private const String dbname = "ChatDB";
        public override string databaseName { get; } = dbname;

        /// <summary>
        /// The singleton isntance of the database
        /// </summary>
        protected static ChatDatabase instance = null;

        /// <summary>
        /// This property represents the database schema, and will be used by the base class
        /// to create and delete the database.
        /// </summary>
        
        // TODO: May need to fix
        protected override Table[] tables { get; } =
        {
            new Table
            (
                dbname,
                "CHAT",
                new Column[]
                {
                    new Column
                    (
                        "SENDER", "VARCHAR(50)",
                        new string[]
                        {
                            "NOT NULL"
                        }, 
                        true
                    ),
                     new Column
                    (
                        "RECEIVER", "VARCHAR(50)",
                        new string[] 
                        {
                            "NOT NULL"
                        },
                        true
                    ),
                    new Column
                    (
                        "MESSAGE", "VARCHAR(500)",
                        new string[]
                        {
                            "NOT NULL"
                        }, 
                        false
                    ),
                    new Column
                    (
                        "TIMESTAMP", "INT(64)",
                        new string[]
                        {
                            "NOT NULL"
                        }, 
                        true
                    )
                }
            )
        };
    }
}
