using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages.NServiceBus.Commands;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.CompanyDirectory;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using NServiceBus;

namespace AuthenticationService.Communication
{
    /// <summary>
    /// This portion of the class contains client connection functionality pertaining to the company listings service.
    /// </summary>
    partial class ClientConnection
    {
        private ServiceBusResponse companyListingsRequest(CompanyDirectoryServiceRequest companyListingsRequest)
        {
            switch(companyListingsRequest.requestType)
            {
                case (CompanyDirectoryRequest.CompanySearch):
                    return companySearch((CompanySearchRequest)companyListingsRequest);
                case (CompanyDirectoryRequest.GetCompanyInfo):
                    return infoSearch((GetCompanyInfoRequest)companyListingsRequest);
                case (CompanyDirectoryRequest.SaveCompanyReview):
                    return saveReview((SaveCompanyReviewRequest)companyListingsRequest);
                default:
                    return new ServiceBusResponse(false, "No results could be found pertaining to your search");
            }
        }

        private ServiceBusResponse companySearch(CompanySearchRequest searchRequest)
        {
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("CompanyListings");
            return requestingEndpoint.Request<ServiceBusResponse>(searchRequest, sendOptions).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private ServiceBusResponse infoSearch(GetCompanyInfoRequest infoRequest)
        {
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("CompanyListings");
            return requestingEndpoint.Request<ServiceBusResponse>(infoRequest, sendOptions).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private ServiceBusResponse saveReview(SaveCompanyReviewRequest saveRequest)
        {
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("CompanyListings");
            return requestingEndpoint.Request<ServiceBusResponse>(saveRequest, sendOptions).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
