using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using Messages.DataTypes.Database.CompanyDirectory;

namespace Messages.NServiceBus.Events
{
    public class CompanyListingsEvent : IEvent
    {
        public CompanyInstance company { get; set; }
        //TODO: Add other attributes to this class.
    }
}
