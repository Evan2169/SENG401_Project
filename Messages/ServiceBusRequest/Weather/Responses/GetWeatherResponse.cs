using Messages.DataTypes.Database.Weather;

using System;

namespace Messages.ServiceBusRequest.Weather.Responses
{
    [Serializable]
    public class GetWeatherResponse : ServiceBusResponse
    {
        public GetWeatherResponse(bool result, string response, CompanyWeather companyWeather)
            : base(result, response)
        {
            this.companyWeather = companyWeather;
        }

        /// <summary>
        /// Contains information about the company the client requested
        /// </summary>
        public CompanyWeather companyWeather;
    }
}
