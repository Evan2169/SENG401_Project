using Messages.DataTypes.Database.CompanyDirectory;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.Chat.Responses;
using Messages.DataTypes.Database.Chat;
using Messages.ServiceBusRequest.Weather.Requests;
using Messages.ServiceBusRequest.Weather.Responses;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Messages.DataTypes.Database.Weather;

namespace Weather.Handlers
{
    
    /// <summary>
    /// This is the handler class for the getting company info. 
    /// This class is created and its methods called by the NServiceBus framework
    /// </summary>
    public class GetWeatherHandler : IHandleMessages<GetWeatherRequest>
    {
        /// <summary>
        /// This is a class provided by NServiceBus. Its main purpose is to be use log.Info() instead of Messages.Debug.consoleMsg().
        /// When log.Info() is called, it will write to the console as well as to a log file managed by NServiceBus
        /// </summary>
        /// It is important that all logger member variables be static, because NServiceBus tutorials warn that GetLogger<>()
        /// is an expensive call, and there is no need to instantiate a new logger every time a handler is created.
        static ILog log = LogManager.GetLogger<GetWeatherRequest>();

        /// <summary>
        /// Searches for company information
        /// </summary>
        /// <param name="message">Information about the company</param>
        /// <param name="context">Used to access information regarding the endpoints used for this handle</param>
        /// <returns>The response to be sent back to the calling process</returns>
        public Task Handle(GetWeatherRequest message, IMessageHandlerContext context)
        {
            
            //TODO: Need to do some error checking code. May need to fix.

            string apiKey = "fRA3peleZnVpeYrWfVBwBDcy5g7Li1GV"; //Had to get a new key.
            HttpClient request = new HttpClient();

            //Use a GET request to get city key.
            string uri = @"http://dataservice.accuweather.com/locations/v1/cities/search?apikey=" + apiKey + @"&q=" + message.companyWeather.location + @"&details=false";
            string cityRequestReponse = request.GetStringAsync(uri).Result;
            Console.WriteLine(cityRequestReponse);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            //Index[0].key should give the key of the first city found, as per the api documentation.
            string cityKey = (string)ser.Deserialize<dynamic>(cityRequestReponse)["key"];
            Console.WriteLine(cityKey);

            //Use a GET request to retrieve weather information corresponding to the given key.
            uri = @"http://dataservice.accuweather.com/currentconditions/v1/" + cityKey + @"?apikey=" + apiKey + @"&details=false";
            string weatherRequestResponse = request.GetStringAsync(uri).Result;
            Console.WriteLine(weatherRequestResponse);
            //The indexes/attributes correspond to information given in the api documentation.
            dynamic deserializedObj = ser.Deserialize<dynamic>(weatherRequestResponse);
            string realFeel = deserializedObj["RealFeelTemperature"]["Metric"]["Value"];
            string temperature = deserializedObj["Temperature"]["Metric"]["Value"];
            string weatherText = deserializedObj["WeatherText"];

            CompanyWeather toReturn = new CompanyWeather
            {
                realFeelTemperature = realFeel,
                weatherText = weatherText,
                location = message.companyWeather.location,
                temperature = temperature
            };

            return context.Reply(new GetWeatherResponse(true, "Successfully obtained weather information.", toReturn));
            
            //return context.Reply(new GetWeatherResponse(true, "", null));
        }
    }
}
