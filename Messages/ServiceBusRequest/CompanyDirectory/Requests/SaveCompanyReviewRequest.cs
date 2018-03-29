using Messages.DataTypes.Database.CompanyDirectory;

using System;

namespace Messages.ServiceBusRequest.CompanyDirectory.Requests
{
    [Serializable]
    public class SaveCompanyReviewRequest : CompanyDirectoryServiceRequest
    {
        public SaveCompanyReviewRequest(ReviewInstance companyReview)
            : base(CompanyDirectoryRequest.SaveCompanyReview)
        {
            this.companyReview = companyReview;
        }

        /// <summary>
        /// Contains information needed to locate additional information about the company the client is requesting
        /// </summary>
        public ReviewInstance companyReview;
    }
}
