using Messages;
using Messages.Database;
using Messages.DataTypes;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;

using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;

namespace CompanyListingsService.Database
{
    /// <summary>
    /// This portion of the class contains methods and functions
    /// </summary>
    public partial class CompanyListingsDatabase : AbstractDatabase
    {
        /// <summary>
        /// Private default constructor to enforce the use of the singleton design pattern
        /// </summary>
        private CompanyListingsDatabase() { }

        /// <summary>
        /// Gets the singleton instance of the database
        /// </summary>
        /// <returns>The singleton instance of the database</returns>
        public static CompanyListingsDatabase getInstance()
        {
            if (instance == null)
            {
                instance = new CompanyListingsDatabase();
            }
            return instance;
        }

        /// <summary>
        /// Saves the company info to the database
        /// </summary>
        /// <param name="compo">Information about the company</param>
        //TODO: Create company saving functionality
        public void saveCompany(CompanyListingsEvent compo)
        {
        /*
            if(openConnection() == true)
            {
                string query = @"INSERT INTO echoforward(timestamp, username, datain)" +
                    @"VALUES('" + DateTimeOffset.Now.ToUnixTimeSeconds().ToString() +
                    @"', '" + echo.username + @"', '" + echo.data + @"');";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                closeConnection();
            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
            }
        */
        }

        ///<summary>
        ///Retrieves company info based on company name
        ///</summary>
        ///<param name="compo">Information about the company</param>
        //TODO: Create company retreiving functionality
        public GetCompanyInfoResponse getCompanyInfo(GetCompanyInfoRequest compo)
        {
            string query = @"SELECT * FROM " + databaseName + @".Companies " +
                @"WHERE username='" + username + @"' " +
                @"AND password='" + password + @"';";
        }

        ///<summary>
        ///Searches for companies
        ///</summary>
        ///<param name="compo">Information about what to search for</param>
        //TODO: Create company retreiving functionality
        public CompanySearchResponse searchCompany(CompanySearchRequest compo)
        {

        }
    }

    /// <summary>
    /// This portion of the class contains the properties and variables 
    /// </summary>
    public partial class CompanyListingsDatabase : AbstractDatabase
    {
        /// <summary>
        /// The name of the database.
        /// Both of these properties are required in order for both the base class and the
        /// table definitions below to have access to the variable.
        /// </summary>
        private const String dbname = "CompanyDB";
        public override string databaseName { get; } = dbname;

        /// <summary>
        /// The singleton isntance of the database
        /// </summary>
        protected static CompanyListingsDatabase instance = null;

        /// <summary>
        /// This property represents the database schema, and will be used by the base class
        /// to create and delete the database.
        /// </summary>
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
