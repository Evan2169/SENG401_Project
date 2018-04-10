using Messages.DataTypes.Database.Weather;

using System;

namespace Messages.ServiceBusRequest.Weather.Requests
{
    [Serializable]
    public class GetWeatherRequest : WeatherServiceRequest
    {
        public GetWeatherRequest(CompanyWeather companyWeather)
            : base(WeatherRequest.GetWeather)
        {
            this.companyWeather = companyWeather;
        }

        /// <summary>
        /// Contains information needed to locate additional information about the company the client is requesting
        /// </summary>
        public CompanyWeather companyWeather;
    }
}
