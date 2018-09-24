using Xunit;

namespace IAIP.Testx
{
    public class UnitTestTests
    {
        [Fact]
        private void PassingTest() => Assert.Equal(4, Add(2, 2));

        //[Fact]
        //private void FailingTest() => Assert.Equal(5, Add(2, 2));

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        private void TestOddEven(int value)
        {
            if (value % 2 == 0)
            {
                Assert.False(IsOdd(value));
                Assert.True(IsEven(value));
            }
            else
            {
                Assert.True(IsOdd(value));
                Assert.False(IsEven(value));
            }
        }

        private int Add(int x, int y) => x + y;

        private bool IsOdd(int x) => x % 2 == 1;

        private bool IsEven(int x) => x % 2 == 0;
    }
}
