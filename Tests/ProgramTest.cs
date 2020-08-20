using Xunit;

namespace TodoAPI.Tests
{
    public class ProgramTest
    {
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        public void MyFirstTheory(int number)
        {
            Assert.True(IsOdd(number));
        }

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(3, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
        bool IsOdd(int x) 
        {
            return x % 2 == 1;
        }
    }
}