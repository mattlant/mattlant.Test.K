// (c) 2020 mattlant
// See LICENSE file for license information

using System.Collections.Generic;
using System.Linq;

namespace mattlant.Test.K.Analysis
{
    /// <summary>
    /// Analyzes a list of events for conflicting times (overlapping)
    /// </summary>
    /// <remarks>
    ///
    /// </remarks>
    public class EventConflictsAnalyzer : IEventsAnalyzer<IEnumerable<EventConflicts>>
    {
        private readonly IEventStartEndSorter _eventSorter;

        /// <summary>
        /// Creates and instance of <see cref="EventConflictsAnalyzer"/>.
        /// </summary>
        /// <param name="eventSorter">The <see cref="IEventStartEndSorter"/> to use.</param>
        public EventConflictsAnalyzer(IEventStartEndSorter eventSorter)
        {
            _eventSorter = eventSorter;
        }


        /// <summary>
        /// Processes an <see cref="IEnumerable{IEvent}"/> list of events to find conflicts.
        /// </summary>
        /// <param name="events">An <see cref="IEnumerable{IEvent}"/> list of events to analyze.</param>
        /// <returns>An <seealso cref="IEnumerable{T}"/> list of <seealso cref="EventConflicts"/></returns>
        public IEnumerable<EventConflicts> Process(IEnumerable<IEvent> events)
        {
            IEvent[] sortedEvents = _eventSorter.Sort(events).ToArray();

            List<EventConflicts> overlappingEventsList = new List<EventConflicts>();

            for (int index = 0; index < sortedEvents.Count(); index++)
            {

                EventConflicts conflicts = new EventConflicts(sortedEvents[index]);

                foreach (var @event in GetFutureConflicts(index, sortedEvents, conflicts.Event))
                    conflicts.FutureConflicts.Add(@event);

                foreach (var @event in GetPastConflicts(index, sortedEvents, conflicts.Event))
                    conflicts.PastConflicts.Add(@event);

                if (conflicts.HasConflicts)
                    overlappingEventsList.Add(conflicts);
            }

            return overlappingEventsList.AsEnumerable();

        }

        private static IEnumerable<IEvent> GetPastConflicts(int currentItemIndex, IEvent[] sortedEvents, 
            IEvent currentItem)
        {
            List<IEvent> results = new List<IEvent>();

            for (int lookBehind = currentItemIndex - 1; lookBehind >= 0; lookBehind--)
            {
                IEvent compareEvent = sortedEvents[lookBehind];

                // > 0 means they overlap
                // < 0 means they do not overlap
                // = 0 means the start and end time are same (back to back events)
                if (compareEvent.End.CompareTo(currentItem.Start) <= 0)
                    break;

                results.Add(compareEvent);
            }

            return results;
        }

        private static IEnumerable<IEvent> GetFutureConflicts(int currentItemIndex, IEvent[] sortedEvents, 
            IEvent currentItem)
        {
            List<IEvent> results = new List<IEvent>();

            for (int lookAhead = currentItemIndex + 1; lookAhead < sortedEvents.Length; lookAhead++)
            {
                IEvent compareEvent = sortedEvents[lookAhead];

                // > 0 means they overlap
                // < 0 means they do not overlap
                // = 0 means the start and end time are same (back to back events)
                if (currentItem.End.CompareTo(compareEvent.Start) <= 0)
                    break;
                results.Add(compareEvent);
            }

            return results;
        }
    }
}