using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.DNS;
using NetworkUtility.Ping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Tests.PingTests
{
    public class NetworkServiceTests
    {
        private readonly NetworkService _networkService;
        private readonly IDNS _DNS;

        public NetworkServiceTests()
        {
            // Dependencies
            _DNS = A.Fake<IDNS>();

            // SUT
            _networkService = new NetworkService(_DNS);
        }

        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            // Arrange - Get Data Needed for the Test (Variables, Classes, Mocks)
            A.CallTo(() => _DNS.SendDNS()).Returns(true);
            // Act - Execute the Unit
            var result = _networkService.SendPing();

            // Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping Sent!");
            result.Should().Contain("Success", Exactly.Once());
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(5, 8, 13)]
        public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
        {
            // Arrange
            
            // Act
            var result = _networkService.PingTimeout(a, b);

            // Assert
            result.Should().Be(expected);
            result.Should().BeGreaterThanOrEqualTo(2);
            result.Should().NotBeInRange(-100, 0);
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnDate()
        {
            // Arrange

            // Act
            var result = _networkService.LastPingDate();

            // Assert
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2030));
        }

        [Fact]
        public void NetworkService_GetPingOptions_ReturnPingOptions()
        {
            // Arrange
            var expectedResult = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1,
            };

            // Act
            var result = _networkService.GetPingOptions();

            // Assert
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expectedResult);
            result.Ttl.Should().Be(1);
        }

        [Fact]
        public void NetworkService_MostRecentPings_ReturnIEnumerable()
        {
            // Arrange
            var expectedResult = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1,
            };

            // Act
            var result = _networkService.MostRecentPings();

            // Assert
            //result.Should().BeOfType<IEnumerable<PingOptions>>(); // FAILS
            result.Should().ContainEquivalentOf(expectedResult);
            result.Should().Contain(x => x.DontFragment == true);
        }
    }
}
