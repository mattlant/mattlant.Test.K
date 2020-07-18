// (c) 2020 mattlant
// See LICENSE file for license information

using System;
using Moq;
using NUnit.Framework;

namespace mattlant.Test.K.Tests.Core
{
    [TestFixture]
    public class EventStartEndComparerTests
    {

        [TestCase(9, 30, 30, 7, 30, 30, TestName = "Comparer - No Overlap Before", ExpectedResult = 1, TestOf = typeof(EventStartEndComparer))]
        [TestCase(9, 30, 30, 11, 30, 30, TestName = "Comparer - No Overlap After", ExpectedResult = -1, TestOf = typeof(EventStartEndComparer))]
        [TestCase(9, 30, 30, 9, 30, 30, TestName = "Comparer - Same Start and End", ExpectedResult = 0, TestOf = typeof(EventStartEndComparer))]
        [TestCase(9, 30, 30, 9, 30, 60, TestName = "Comparer - Same Start Different End", ExpectedResult = -1, TestOf = typeof(EventStartEndComparer))]
        public int DoesComparerReturnCorrectResult(int hour1, int minute1, int duration1, int hour2, int minute2, int duration2)
        {
            EventStartEndComparer comparer = new EventStartEndComparer();

            var event1Mock = MockFactory.CreateEventMockForGetOnly(hour1, minute1, duration1, "Event Mock 1");
            IEvent event1 = event1Mock.Object;

            var event2Mock = MockFactory.CreateEventMockForGetOnly(hour2, minute2, duration2, "Event Mock 2");
            IEvent event2 = event2Mock.Object;

            return comparer.Compare(event1, event2);

        }
    }
}