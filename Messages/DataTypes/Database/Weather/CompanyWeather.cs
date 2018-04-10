using System;

namespace Messages.DataTypes.Database.Weather
{
    
    /// <summary>
    /// This class represents a weather information datatype sent from one user to another.
    /// </summary>
    [Serializable]
    public partial class CompanyWeather
    {
        /// <summary>
        /// 
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string weatherText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string temperature { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string realFeelTemperature { get; set; }

    }
}
