// (c) 2020 mattlant
// See LICENSE file for license information

using System.Collections.Generic;

namespace mattlant.Test.K.Providers.Events
{
    /// <summary>
    /// A simplified<see cref="IEvent"/> Data Provider.
    /// </summary>
    /// <remarks>For this coding test, the data provider is read only with no filtering.</remarks>
    public interface IEventDataProvider
    {
        /// <summary>
        /// Loads a list of events from an Event source.
        /// </summary>
        /// <returns><see cref="IList{IEvent}"/> with all the events from the source.</returns>
        IList<IEvent> LoadAllEvents();
    }
}