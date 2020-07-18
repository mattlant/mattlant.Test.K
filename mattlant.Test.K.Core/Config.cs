// (c) 2020 mattlant
// See LICENSE file for license information

using mattlant.Test.K.Providers.Events;

namespace mattlant.Test.K
{
    /// <summary>
    /// Holds all configuration information for application.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This would normally be handled by a configuration
    /// provider of some sort and loaded from a config file, or a
    /// configuration service/database such as etcd or ZooKeeper.
    /// </para>
    /// <para>
    /// String literals are in English only but otherwise might be localized
    /// for a real world app and retrieved from a localization lookup
    /// process.
    /// </para>
    /// </remarks>
    public static class Config
    {
        /// <summary>
        /// Configuration information for Events
        /// </summary>
        /// <remarks>
        /// The items here are self explanatory so I left them undocumented. (Good naming goes a long way!)
        /// </remarks>
        public static class Events
        {
            public const double DefaultDurationInMinutes = 60;
            public const string DefaultTitle = "(No Title)";
        }

        /// <summary>
        /// Configuration Information for all providers
        /// </summary>
        public static class Providers
        {
            /// <summary>
            /// Gets the Event Data Provider as a string in the form "[fully qualified class name],[Assembly name]"
            /// </summary>
            public const string EventDataProvider =
                "mattlant.Test.K.Providers.Events.TestDataEventDataProvider, mattlant.Test.K.Providers.BuiltIn.dll";
        }
    }
}
