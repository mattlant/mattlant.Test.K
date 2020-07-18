// (c) 2020 mattlant
// See LICENSE file for license information

using System;
using Moq;

namespace mattlant.Test.K.Tests
{
    static internal class MockFactory
    {
        public static Mock<IEvent> CreateEventMockForGetOnly(int hour1, int minute1, int duration1, string title)
        {
            var eventMock = new Mock<IEvent>();
            eventMock.SetupGet(ev => ev.Start).Returns(new DateTime(2020, 01, 01, hour1, minute1, 0));
            eventMock.SetupGet(ev => ev.End).Returns(new DateTime(2020, 01, 01, hour1, minute1, 0).AddMinutes(duration1));
            eventMock.SetupGet(ev => ev.Title).Returns(title);
            return eventMock;
        }
    }
}