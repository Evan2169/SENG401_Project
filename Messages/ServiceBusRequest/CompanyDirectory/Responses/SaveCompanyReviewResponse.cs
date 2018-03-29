using Messages.DataTypes.Database.CompanyDirectory;

using System;

namespace Messages.ServiceBusRequest.CompanyDirectory.Responses
{
    [Serializable]
    public class SaveCompanyReviewResponse : ServiceBusResponse
    {
        public SaveCompanyReviewResponse(bool result, string response, ReviewInstance companyReview)
            : base(result, response)
        {
            this.companyReview = companyReview;
        }

        /// <summary>
        /// Contains information about the company the client requested
        /// </summary>
        public ReviewInstance companyReview;
    }
}
