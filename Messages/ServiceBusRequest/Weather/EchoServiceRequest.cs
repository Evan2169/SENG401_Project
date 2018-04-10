using System;

namespace Messages.ServiceBusRequest.Weather
{
    [Serializable]
    public class WeatherServiceRequest : ServiceBusRequest
    {
        public WeatherServiceRequest(WeatherRequest requestType)
            : base(Service.Weather)
        {
            this.requestType = requestType;
        }

        /// <summary>
        /// Indicates the type of request the client is seeking from the echo service
        /// </summary>
        public WeatherRequest requestType;
    }

    public enum WeatherRequest { GetWeather };
}
