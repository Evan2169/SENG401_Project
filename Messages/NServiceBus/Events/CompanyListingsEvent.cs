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
        public CompanyListingsEvent(CompanyInstance comp)
        {
            company = comp;
        }

        public CompanyInstance company { get; set; }
    }
}
