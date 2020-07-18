// (c) 2020 mattlant
// See LICENSE file for license information

using System.Collections.Generic;

namespace mattlant.Test.K
{
    /// <summary>
    /// Provides an interface to build custom sorting algorithms for Events.
    /// </summary>
    public interface IEventSorter
    {
        /// <summary>
        /// Sorts a list of events.
        /// </summary>
        /// <param name="events">The <see cref="IEnumerable{IEvent}"/> list of events to sort.</param>
        /// <returns>An <see cref="IEnumerable{IEvent}"/> list of events that are sorted according to the implemented sorting algorithm.</returns>
        public IEnumerable<IEvent> Sort(IEnumerable<IEvent> events);
    }
}