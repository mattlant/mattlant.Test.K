// (c) 2020 mattlant
// See LICENSE file for license information

using System;
using System.Collections.Generic;

namespace mattlant.Test.K.Providers.Events
{
    /// <summary>
    /// Provides Event Test Data.
    /// </summary>
    public class TestDataEventDataProvider : IEventDataProvider
    {
        private static readonly Random random = new Random(3);

        private const int NumberOfRandomEvents = 50;


        /// <inheritdoc />
        public IList<IEvent> LoadAllEvents()
        {
            return GenerateRandomEvents();
        }

        private static List<IEvent> GenerateRandomEvents()
        {
            List<IEvent> events = new List<IEvent>() { GenerateRandomEvent(1) };

            for (int i = 2; i < NumberOfRandomEvents; i++)
            {
                events.Insert(random.Next(0, events.Count - 1), GenerateRandomEvent(i));
            }

            return events;
        }

        private static IEvent GenerateRandomEvent(int eventNumber)
        {
            return new SimpleEvent(
                new DateTime(
                    year: 2020, 
                    month: 7, 
                    day: random.Next(1, 28), 
                    hour: random.Next(9, 17), 
                    minute: random.Next(0,5) * 10, 
                    second:0),
                title:$"Random Event {eventNumber}"
                );
        }



    }
}