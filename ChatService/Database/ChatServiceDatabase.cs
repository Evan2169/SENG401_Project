using Messages;
using Messages.Database;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.DataTypes.Database.CompanyDirectory;

using MySql.Data.MySqlClient;

using System;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using System.Collections;

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
        
        // TODO: Need to setup chat database table
        protected override Table[] tables { get; } =
        {
            new Table
            (
                dbname,
                "Companies",
                new Column[]
                {
                    new Column
                    (
                        "companyName", "VARCHAR(15)",
                        new string[]
                        {
                            "NOT NULL",
                            "UNIQUE"
                        }, true
                    ),
                     new Column
                    (
                        "email", "VARCHAR(100)",
                        new string[] 
                        {
                            "NOT NULL"
                        },
                        false
                    ),
                    new Column
                    (
                        "phoneNumber", "VARCHAR(10)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    ),
                    new Column
                    (
                        "location", "VARCHAR(50)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    ),
                }
            )
        };
    }
}
