// (c) 2020 mattlant
// See LICENSE file for license information

using System.Collections.Generic;

#nullable enable  

namespace mattlant.Test.K.Analysis
{
    /// <summary>
    /// Interface for building custom event analyzers based on <see cref="IEvent"/>.
    /// </summary>
    public interface IEventsAnalyzer <out TResult>
    {
        /// <summary>
        /// Processes an <see cref="IEnumerable{IEvent}"/> list of events to analyze.
        /// </summary>
        /// <param name="events">An <see cref="IEnumerable{IEvent}"/> list of events to analyze.</param>
        /// <returns>A result of type <c>TResult</c>.</returns>
        TResult Process(IEnumerable<IEvent> events);
    }
}