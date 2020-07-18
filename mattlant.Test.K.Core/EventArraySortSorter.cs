// (c) 2020 mattlant
// See LICENSE file for license information

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace mattlant.Test.K
{
    /// <summary>
    /// A sorter that sorts events based on the <see cref="EventStartEndComparer"/>.
    /// </summary>
    public class EventArraySortSorter : IEventStartEndSorter
    {
        /// <summary>
        /// Sorts a list of events by Start time then End time, in ascending order.
        /// </summary>
        /// <param name="events">An <see cref="IEnumerable{IEvent}"/> list of events to be sorted.</param>
        /// <returns>An <see cref="IEnumerable{IEvent}"/> list of events that is sorted by Start time then End time, in ascending order.</returns>
        public IEnumerable<IEvent> Sort(IEnumerable<IEvent> events)
        {
            var eventsArray = events.ToArray();

            Array.Sort(eventsArray, comparer: new EventStartEndComparer());

            return eventsArray;
        }
    }
}