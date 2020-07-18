// (c) 2020 mattlant
// See LICENSE file for license information

using System.Collections.Generic;
using System.Linq;
using mattlant.Test.K.Analysis;
using Moq;
using NUnit.Framework;

namespace mattlant.Test.K.Tests.Core.Analysis
{
    [TestFixture]
    public class EventConflictsAnalyzerTests
    {
        [TestCase(9, 30, 30, 7, 30, 30,
            TestName = "Does analyzer return no conflict for each event 9:30-30 7:30-30",
            ExpectedResult = new[] {0, 0}, TestOf = typeof(EventConflictsAnalyzer))]

        [TestCase(9, 30, 30, 11, 30, 30,
            TestName = "Does analyzer return no conflict for each event 9:30-30 11:30-30",
            ExpectedResult = new[] {0, 0}, TestOf = typeof(EventConflictsAnalyzer))]

        [TestCase(9, 30, 30, 9, 00, 60,
            TestName = "Does analyzer return 1 conflict for each event 9:30-30 9:00-60",
            ExpectedResult = new[] {1, 1}, TestOf = typeof(EventConflictsAnalyzer))]

        [TestCase(9, 00, 30, 9, 30, 60,
            TestName = "Does analyzer return no conflict for each event 9:00-30 9:30-60",
            ExpectedResult = new[] {0, 0}, TestOf = typeof(EventConflictsAnalyzer))]

        [TestCase(9, 30, 30, 9, 30, 30,
            TestName = "Does analyzer return 1 conflict for each event 9:30-30 9:30-30",
            ExpectedResult = new[] {1, 1}, TestOf = typeof(EventConflictsAnalyzer))]

        [TestCase(9, 00, 60, 9, 30, 60,
            TestName = "Does analyzer return 1 conflict for each event 9:00-60 9:30-60",
            ExpectedResult = new[] {1, 1}, TestOf = typeof(EventConflictsAnalyzer))]
        public int[] AnalyzerReturnsCorrectConflicts(
            int hour1, int minute1, int duration1,
            int hour2, int minute2, int duration2)
        {
            var analyzer = new EventConflictsAnalyzer(new EventArraySortSorter());

            var eventList = new List<IEvent>();

            eventList.Add(MockFactory.CreateEventMockForGetOnly(hour1, minute1, duration1, "Event Mock 1").Object);
            eventList.Add(MockFactory.CreateEventMockForGetOnly(hour2, minute2, duration2, "Event Mock 2").Object);

            EventConflicts[] conflicts = analyzer.Process(eventList).ToArray();

            if (!conflicts.Any())
                return new[] {0, 0};

            return new[]
            {
                conflicts[0].FutureConflicts.Count + conflicts[0].PastConflicts.Count,
                conflicts[1].FutureConflicts.Count + conflicts[1].PastConflicts.Count
            };



        }
    }
}