using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages.NServiceBus.Commands;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Weather;
using Messages.ServiceBusRequest.Weather.Requests;
using NServiceBus;

namespace AuthenticationService.Communication
{
    /// <summary>
    /// This portion of the class contains client connection functionality pertaining to the weather service.
    /// </summary>
    //TODO: May need to fix this
    partial class ClientConnection
    {
        private ServiceBusResponse weatherRequest(WeatherServiceRequest weatherRequest)
        {
            switch(weatherRequest.requestType)
            {
                case (WeatherRequest.GetWeather):
                    return getWeather((GetWeatherRequest)weatherRequest);

                default:
                    return new ServiceBusResponse(false, "No results could be found pertaining to your search");
            }
        }

        private ServiceBusResponse getWeather(GetWeatherRequest getWeatherRequest)
        {
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("Weather");
            return requestingEndpoint.Request<ServiceBusResponse>(getWeatherRequest, sendOptions).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
