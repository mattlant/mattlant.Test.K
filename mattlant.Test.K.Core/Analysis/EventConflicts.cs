// (c) 2020 mattlant
// See LICENSE file for license information

using System.Collections.Generic;

namespace mattlant.Test.K.Analysis
{
    /// <summary>
    /// A class that contains information on an event and any future and past overlapping events.
    /// </summary>
    public readonly struct EventConflicts
    {
        /// <summary>
        /// Creates a new <see cref="EventConflicts"/> instance with the <see cref="IEvent"/> this instance refers to.
        /// </summary>
        /// <param name="event">The <see cref="IEvent"/> this instance refers to.</param>
        public EventConflicts(IEvent @event)
        {
            Event = @event;
            FutureConflicts = new List<IEvent>();
            PastConflicts = new List<IEvent>();
        }

        /// <summary>
        /// Gets the <see cref="IEvent"/> this <see cref="EventConflicts"/> refers to.
        /// </summary>
        public IEvent Event { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IEvent}"/> list of events that conflict in the future.
        /// </summary>
        public IList<IEvent> FutureConflicts { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IEvent}"/> list of events that conflict in the past.
        /// </summary>
        public IList<IEvent> PastConflicts { get; }

        /// <summary>
        /// Gets a value indicating if there are any past or future conflicts.
        /// </summary>
        public bool HasConflicts => FutureConflicts.Count > 0 || PastConflicts.Count > 0;
    }
}