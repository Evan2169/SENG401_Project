using CompanyListingsService.Database;
using Messages.DataTypes.Database.CompanyDirectory;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using NServiceBus;
using NServiceBus.Logging;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CompanyListingsService.Handlers
{
    
    /// <summary>
    /// This is the handler class for the getting company info. 
    /// This class is created and its methods called by the NServiceBus framework
    /// </summary>
    public class SaveReviewHandler : IHandleMessages<SaveCompanyReviewRequest>
    {
        /// <summary>
        /// This is a class provided by NServiceBus. Its main purpose is to be use log.Info() instead of Messages.Debug.consoleMsg().
        /// When log.Info() is called, it will write to the console as well as to a log file managed by NServiceBus
        /// </summary>
        /// It is important that all logger member variables be static, because NServiceBus tutorials warn that GetLogger<>()
        /// is an expensive call, and there is no need to instantiate a new logger every time a handler is created.
        static ILog log = LogManager.GetLogger<SaveCompanyReviewRequest>();

        /// <summary>
        /// Searches for company information
        /// </summary>
        /// <param name="message">Information about the company</param>
        /// <param name="context">Used to access information regarding the endpoints used for this handle</param>
        /// <returns>The response to be sent back to the calling process</returns>
        public Task Handle(SaveCompanyReviewRequest message, IMessageHandlerContext context)
        {
            System.Diagnostics.Debug.WriteLine("CHECK GOT HERE -------------------- ");
            ReviewInstance review = message.companyReview;
            var review_JSON = new JavaScriptSerializer().Serialize(review);
            var client = new HttpClient();
            var content = new StringContent(review_JSON.ToString(), Encoding.UTF8, "application/json");
            //TODO: --ENSURE THIS IS CORRECT BEFORE DEPLOYMENT--
            var result = client.PostAsync("http://localhost:50151/DBLS/SaveCompanyReview/", content).Result;
            SaveCompanyReviewResponse reviewResponse;
            if (result.IsSuccessStatusCode)
            {
                reviewResponse = new SaveCompanyReviewResponse(true, "Review successfully added", review);
            }
            else
            {
                reviewResponse = new SaveCompanyReviewResponse(false, "Review unable to be added", review);
            }
            return context.Reply(reviewResponse);
        }
    }
}
