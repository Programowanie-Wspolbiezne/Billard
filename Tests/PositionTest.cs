using Data;

namespace Tests
{
    public class PositionTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Position position = new Position(0, 0);

            Assert.Equal(0, position.x);
        }
    }
}