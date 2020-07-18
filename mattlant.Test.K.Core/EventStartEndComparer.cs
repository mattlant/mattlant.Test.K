// (c) 2020 mattlant
// See LICENSE file for license information

using System.Collections.Generic;

namespace mattlant.Test.K
{
    /// <summary>
    /// Implements <see cref="IComparer"/> interface to aid sorting based on Start <see cref="DateTime"/> then End <see cref="DateTime"/>
    /// </summary>
    public sealed class EventStartEndComparer : IComparer<IEvent>
    {
        /// <summary>Compares two <see cref="IEvent"/> instances and returns a value indicating whether one is preceding, same as, or after/longer than the other.</summary>
        /// <param name="event1">The first <see cref="IEvent"/> to compare.</param>
        /// <param name="event2">The second <see cref="IEvent"/> to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="event1" /> and <paramref name="event2" />, as shown in the following:.
        /// <para>
        /// If <paramref name="event1" /> starts before <paramref name="event2" /> returns negative value.
        /// If <paramref name="event1" /> starts after <paramref name="event2" /> returns positive value.
        /// </para>
        /// <para>
        /// If <paramref name="event1" /> and <paramref name="event2" /> start at same time, check which ends first.
        /// If <paramref name="event1" /> ends before <paramref name="event2" /> returns negative value.
        /// If <paramref name="event1" /> ends after <paramref name="event2" /> returns positive value
        /// If end times are also same, the comparer will return 0, meaning they are equal 'value'.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// I wanted to outline the logic for clarity sake for this coding test.
        /// </para>
        /// </remarks>
        public int Compare(IEvent event1, IEvent event2)
        {
            if (ReferenceEquals(event1, event2)) return 0;

            if (ReferenceEquals(null, event2)) return 1;
            if (ReferenceEquals(null, event1)) return -1;

            int startComparison = event1.Start.CompareTo(event2.Start);

            return startComparison != 0 ? 
                startComparison : 
                event1.End.CompareTo(event2.End);
        }
    }
}