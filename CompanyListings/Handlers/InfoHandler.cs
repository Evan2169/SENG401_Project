using CompanyListingsService.Database;
using Messages.DataTypes.Database.CompanyDirectory;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using NServiceBus;
using NServiceBus.Logging;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EchoService.Handlers
{
    
    /// <summary>
    /// This is the handler class for the getting company info. 
    /// This class is created and its methods called by the NServiceBus framework
    /// </summary>
    public class InfoHandler : IHandleMessages<GetCompanyInfoRequest>
    {
        /// <summary>
        /// This is a class provided by NServiceBus. Its main purpose is to be use log.Info() instead of Messages.Debug.consoleMsg().
        /// When log.Info() is called, it will write to the console as well as to a log file managed by NServiceBus
        /// </summary>
        /// It is important that all logger member variables be static, because NServiceBus tutorials warn that GetLogger<>()
        /// is an expensive call, and there is no need to instantiate a new logger every time a handler is created.
        static ILog log = LogManager.GetLogger<GetCompanyInfoRequest>();

        /// <summary>
        /// Searches for company information
        /// </summary>
        /// <param name="message">Information about the company</param>
        /// <param name="context">Used to access information regarding the endpoints used for this handle</param>
        /// <returns>The response to be sent back to the calling process</returns>
        public Task Handle(GetCompanyInfoRequest message, IMessageHandlerContext context)
        {
            //TODO: May need to edit.
            GetCompanyInfoResponse infoResponse = CompanyListingsDatabase.getInstance().getCompanyInfo(message);

            // Get Reviews
            string result = "";
            if (infoResponse.result)
            {
                try
                {
                    HttpClient getRevs = new HttpClient();
                    string uri = "http://localhost:50151/DBLS/GetCompanyReviews/%7B%22companyName%22:%22" + infoResponse.companyInfo.companyName + "%22%7D";
                    result = getRevs.GetStringAsync(uri).Result;
                    System.Diagnostics.Debug.WriteLine(result);
                    ReviewList r = new JavaScriptSerializer().Deserialize<ReviewList>(result);
                    infoResponse.companyInfo.reviewList = r;
                }
                catch (Exception a)
                {
                    infoResponse.result = false;
                    infoResponse.response = "Issue communicating with review system.";
                }
            }
            
            return context.Reply(infoResponse);
        }
    }
}
