using CompanyListingsService.Database;

using Messages.NServiceBus.Events;

using NServiceBus;
using NServiceBus.Logging;

using System.Threading.Tasks;

namespace CompanyListingsService.Handlers
{
    /// <summary>
    /// This is the handler class for a company listings event.
    /// </summary>
    public class CompanyListingsEventHandler : IHandleMessages<CompanyListingsEvent>
    {
        /// <summary>
        /// This is a class provided by NServiceBus. Its main purpose is to be use log.Info() instead of Messages.Debug.consoleMsg().
        /// When log.Info() is called, it will write to the console as well as to a log file managed by NServiceBus
        /// </summary>
        /// It is important that all logger member variables be static, because NServiceBus tutorials warn that GetLogger<>()
        /// is an expensive call, and there is no need to instantiate a new logger every time a handler is created.
        static ILog log = LogManager.GetLogger<AsIsEchoEvent>();

        /// <summary>
        /// Handles company events
        /// This method will be called by the NServiceBus framework when an event of type "CompanyListingsEvent" is published.
        /// </summary>
        /// <param name="message">Information about the company</param>
        /// <param name="context"></param>
        /// <returns>Nothing</returns>
        public Task Handle(CompanyListingsEvent message, IMessageHandlerContext context)
        {
            //TODO: Finish this class
            return Task.CompletedTask;
        }
    }
}
