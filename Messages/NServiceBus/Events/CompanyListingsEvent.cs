using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Messages.NServiceBus.Events
{
    public class CompanyListingsEvent : IEvent
    {
        public string comp { get; set; }
        //TODO: Add other attributes to this class.
    }
}
