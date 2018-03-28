using System;
using System.Collections.Generic;

namespace Messages.DataTypes.Database.CompanyDirectory
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ReviewList
    {
        /// <summary>
        /// A list of the company names
        /// </summary>

            public string response;
            public List<ReviewInstance> reviews;

            public ReviewList()
            {
                reviews = new List<ReviewInstance>();
            }
        
    }
}
