using Messages;
using Messages.Database;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.DataTypes.Database.CompanyDirectory;

using MySql.Data.MySqlClient;

using System;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using System.Collections;

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
        public void saveCompany(CompanyListingsEvent compo)
        {
            if(openConnection())
            {
                string query = @"INSERT INTO " + databaseName + @".Companies " +
                    @"VALUES('" + compo.company.companyName +
                    @"', '" + compo.company.email + @"', '" + compo.company.phoneNumber
                    + @"', '" + compo.company.locations[0] + @"');";    //Assumes array of length one (only oone location)

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                closeConnection();
            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
            }
        
        }

        ///<summary>
        ///Retrieves company info based on company name
        ///</summary>
        ///<param name="compo">Information about the company</param>
        public GetCompanyInfoResponse getCompanyInfo(GetCompanyInfoRequest compo)
        {
            if (openConnection())
            {
                string query = @"SELECT * FROM " + databaseName + @".Companies " +
                    @"WHERE companyName='" + compo.companyInfo.companyName + @"';";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = command.ExecuteReader();

                if (dataReader.Read())
                {
                    string[] loc = new string[1];
                    loc[0] = dataReader.GetString("location");
                    CompanyInstance toReturn = new CompanyInstance(dataReader.GetString("companyName"), dataReader.GetString("phoneNumber"), dataReader.GetString("email"), loc);
                    dataReader.Close();
                    return new GetCompanyInfoResponse(true, "Successfully retrieved company information.", toReturn);
                }
                else
                {
                    dataReader.Close();
                    return new GetCompanyInfoResponse(false, "Could not find any company information.", null);
                }
            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
                return new GetCompanyInfoResponse(false, "Could not find any company information.", null);
            }
        }

        ///<summary>
        ///Searches for companies
        ///</summary>
        ///<param name="compo">Information about what to search for</param>
        public CompanySearchResponse searchCompany(CompanySearchRequest compo)
        {
            if (openConnection() == true)
            {
                string query = @"SELECT companyName FROM " + databaseName + @".Companies " +
                    @"WHERE companyName LIKE '%" + compo.searchDeliminator + @"%';";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = command.ExecuteReader();
                CompanyList results = new CompanyList();
                ArrayList res = new ArrayList();
                for (int i = 0; dataReader.Read() == true; i++)
                {
                    res.Add(dataReader.GetString("companyName"));
                }
                dataReader.Close();
                results.companyNames = new string[res.Count];
                res.CopyTo(results.companyNames);

                closeConnection();
                return new CompanySearchResponse(true, "Sucessfully retrieved company information.", results);
            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
                return new CompanySearchResponse(false, "Could not find any company information.", null);
            }
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
