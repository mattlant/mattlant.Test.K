// (c) 2020 mattlant
// See LICENSE file for license information

using System.Collections.Generic;

namespace mattlant.Test.K.Providers.Events
{
    /// <summary>
    /// Loads Event Data from Mongo.
    /// </summary>
    /// <remarks>
    /// This is just a stub to show provider pattern.
    /// </remarks>

    public class MongoDbEventDataProvider : IEventDataProvider
    {
        /// <summary>
        /// Loads a list of events from an Event source
        /// </summary>
        /// <returns><see cref="IList{IEvent}"/> with all the events from the source.</returns>
        public IList<IEvent> LoadAllEvents()
        {
            throw new System.NotImplementedException();
        }
    }
}