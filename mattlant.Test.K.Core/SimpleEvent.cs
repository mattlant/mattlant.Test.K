// (c) 2020 mattlant
// See LICENSE file for license information

using System;

#nullable enable  

namespace mattlant.Test.K
{
    /// <summary>
    /// Represents a simple event with a start, end and title only.
    /// </summary>
    public class SimpleEvent : IEvent
    {
        /// <summary>Returns a string that represents this <see cref="IEvent"/></summary>
        /// <returns>A string that represents the this <see cref="SimpleEvent"/>.</returns>
        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, {nameof(Title)}: {Title}";
        }

        /// <inheritdoc />
        public DateTime Start { get; set; }


        /// <inheritdoc />
        public DateTime End { get; set; }

        /// <inheritdoc />
        public string Title { get; set; }

        /// <summary>
        /// Creates a <see cref="SimpleEvent"/>.
        /// </summary>
        /// <param name="start">The start <see cref="DateTime"/> of the event as a date and time <see cref="String"/>.</param>
        /// <param name="end">The end <see cref="DateTime"/> of the event as a date and time <see cref="String"/>.</param>
        /// <param name="title">The title of the event. If the value is null, the default title will be used based on configuration settings.</param>
        /// <seealso cref="Config"/>
        public SimpleEvent(string start, string? end = null, string? title = null)
        {
            if (!DateTime.TryParse(new ReadOnlySpan<char>(start.ToCharArray()), out var startDateTime))
                throw new ArgumentException($"Value of parameter {nameof(start)} is not a valid DateTime string.");
            Start = startDateTime;


            if (end == null)
                End = Start.AddMinutes(Config.Events.DefaultDurationInMinutes);
            else if (DateTime.TryParse(new ReadOnlySpan<char>(end.ToCharArray()), out var endDateTime))
                End = endDateTime;
            else    
                throw new ArgumentException($"Value of parameter {nameof(end)} is not a valid DateTime string.");

            Title = title ?? Config.Events.DefaultTitle;

        }

        /// <summary>
        /// Creates a <see cref="SimpleEvent"/>.
        /// </summary>
        /// <param name="start">The start <see cref="DateTime"/> of the event.</param>
        /// <param name="end">The end <see cref="DateTime"/> of the event. If the value is null, the default duration will be used based on configuration settings.</param>
        /// <param name="title">The title of the event. If the value is null, the default title will be used based on configuration settings.</param>
        /// <seealso cref="Config"/>
        public SimpleEvent(DateTime start, DateTime? end = null, string? title = null)
        {
            Start = start;
            End = end ?? Start.AddMinutes(Config.Events.DefaultDurationInMinutes);
            Title = title ?? Config.Events.DefaultTitle;
        }
    }
}
