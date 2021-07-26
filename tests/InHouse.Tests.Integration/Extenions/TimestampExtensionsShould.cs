using InHouse.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InHouse.Tests.Integration.Extenions
{
    public class TimestampExtensionsShould
    {

        [Theory]
        [InlineData(36000, "10 AM")]
        [InlineData(45000, "12:30 PM")]
        [InlineData(72000, "8 PM")]
        [InlineData(64800, "6 PM")]
        [InlineData(3600, "1 AM")]
        [InlineData(43200, "12 PM")]
        [InlineData(86399, "11:59:59 PM")]
        public void ConvertUnixTimeStampTo12HourClockFormattedString(int timestmap, string expected)
        {
            var result = timestmap.Get12HourClockTime();
            Assert.Equal(expected, result);
        }

    }
}
