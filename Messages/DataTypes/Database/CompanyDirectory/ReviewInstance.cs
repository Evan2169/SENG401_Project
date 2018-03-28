using NServiceBus;

using System;

namespace Messages.DataTypes.Database.CompanyDirectory
{
    /// <summary>
    /// This class represents
    /// </summary>
    [Serializable]
    public class ReviewInstance
    {
        public string companyName { get; set; }
        public string review { get; set; }
        public int stars { get; set; }
        public double timestamp { get; set; }
        public string userName { get; set; }

        public ReviewInstance()
        {

        }

        /// <summary>
        /// Creates a ReviewInstance object
        /// </summary>
        /// <param name="companyName">The name of the company</param>
        /// <param name="review"></param>
        /// <param name="stars"></param>
        /// <param name="timestamp"></param>
        /// <param name="userName"></param>
        public ReviewInstance(string companyName, string review, int stars, double timestamp, string userName)
        {
            this.companyName = companyName;
            this.review = review;
            this.stars = stars;
            this.timestamp= timestamp;
            this.userName = userName;
        }
        
    }
}
