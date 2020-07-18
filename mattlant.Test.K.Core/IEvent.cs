// (c) 2020 mattlant
// See LICENSE file for license information

using System;

namespace mattlant.Test.K
{
    /// <summary>
    /// A simplified event interface
    /// </summary>
    /// <remarks>
    /// This event interface is simplified for coding test purposes.
    /// </remarks>
    public interface IEvent
    {
        /// <summary>
        /// Gets and sets the start <see cref="DateTime"/> of the <see cref="IEvent"/>.
        /// </summary>
        DateTime Start { get; set; }

        /// <summary>
        /// Gets and sets the end <see cref="DateTime"/> of the <see cref="IEvent"/>.
        /// </summary>
        DateTime End { get; set; }

        /// <summary>
        /// Gets and sets the title of the <see cref="IEvent"/>.
        /// </summary>
        string Title { get; set; }
    }
}