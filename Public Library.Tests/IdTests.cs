using FluentAssertions;
using Public_Library.LIB;
using Xunit;

namespace Public_Library.Tests
{
    public class IdTests
    {
        [Fact]
        public void Generate_ReturnsValidId()
        {
            string id = Id.Generate();

            id.Should().NotBeNull();
            id.Length.Should().BeGreaterThanOrEqualTo(15);
        }
    }
}
