// (c) 2020 mattlant
// See LICENSE file for license information

using System;
using System.Collections.Generic;
using System.Linq;
using mattlant.Test.K.Analysis;
using mattlant.Test.K.DI;
using mattlant.Test.K.Providers.Events;

#nullable enable

namespace mattlant.Test.K.App
{
    /// <summary>
    /// A test program for showing overlapping events.
    /// </summary>
    /// <remarks>
    /// This test program will go through each event, and find all overlap conflicts for that event.
    /// If the event does not have any conflicts, it will still list, but will state 'No conflicts'.
    /// <para></para>
    /// The test instructions said to "return the sequence of all pairs of overlapping events".
    /// I do not limit to pairs in this test program, but instead find all overlaps for each event,
    /// both forward looking and backward looking.
    /// </remarks>
    class Program
    {
        /// <summary>
        /// Entry point for this test program.
        /// </summary>
        static void Main()
        {
            InitializeDiContainer();

            IList<IEvent> events = GetAllEvents();
            WriteAllEventsToConsole(events);

            IEnumerable<EventConflicts> conflictsList = GetAllConflicts(events);
            WriteConflictsListToConsole(conflictsList.Where(c => c.HasConflicts == true));
        }

        private static IList<IEvent> GetAllEvents()
        {
            IEventDataProvider? eventLoader =
                Container.Current.Get<IEventDataProvider>() ??
                throw new ArgumentNullException(
                    $"Could not get an instance of an {nameof(IEventDataProvider)} from container.");

            IList<IEvent> events = eventLoader.LoadAllEvents();
            return events;
        }

        private static IEnumerable<EventConflicts> GetAllConflicts(IList<IEvent> events)
        {
            IEventStartEndSorter? sorter =
                Container.Current.Get<IEventStartEndSorter>() ??
                throw new ArgumentNullException(
                    $"Could not get an instance of an {nameof(IEventSorter)} from container.");

            EventConflictsAnalyzer conflictsAnalyzer = new EventConflictsAnalyzer(sorter);

            IEnumerable<EventConflicts> conflictsList = conflictsAnalyzer.Process(events);
            return conflictsList;
        }

        private static void WriteAllEventsToConsole(IList<IEvent> events)
        {
            Console.WriteLine("All events (not sorted)");
            Console.WriteLine("=====================================================");

            foreach (IEvent e in events)
                Console.WriteLine(e);
        }

        private static void WriteConflictsListToConsole(IEnumerable<EventConflicts> conflictsList)
        {
            Console.Write($"\r\n\r\nEvents conflicts (sorted) ");
            Console.WriteLine("=====================================================");

            foreach (EventConflicts conflicts in conflictsList)
            {
                Console.WriteLine($"---- Event: {conflicts.Event}");

                WriteConflictsToConsole(conflicts.FutureConflicts, "future");
                WriteConflictsToConsole(conflicts.PastConflicts, "past");

                Console.WriteLine();
            }
        }

        private static void WriteConflictsToConsole(IEnumerable<IEvent> conflicts, string type)
        {
            if (!conflicts.Any())
            {
                Console.WriteLine($"------ No {type} conflicts ---");
                return;
            }

            Console.WriteLine($"------ {conflicts.Count()} {type} conflict(s) ---");

            foreach (IEvent @event in conflicts)
                Console.WriteLine($"-------- {@event}");
        }

        private static void InitializeDiContainer()
        {
            Container.Current.Set<IEventDataProvider>(CreateEventDataProvider());
            Container.Current.Set<IEventStartEndSorter>(new EventArraySortSorter());
        }

        /// <remarks>
        /// I would typically put this in a purpose built class specifically for loading modules/plugins at runtime.
        /// </remarks>
        private static IEventDataProvider CreateEventDataProvider()
        {
            string eventDataProvider = Config.Providers.EventDataProvider.Split(',')[0].Trim();
            string eventDataProviderAssembly = Config.Providers.EventDataProvider.Split(',')[1].Trim();

            IEventDataProvider? dataProvider =
                (Activator.CreateInstanceFrom(eventDataProviderAssembly, eventDataProvider)?
                    .Unwrap() as IEventDataProvider);

            if (dataProvider != null) return dataProvider;

            throw new TypeLoadException("Could not load the IEventDataProvider.");
        }
    }
}