using System;
using Xunit;

namespace UnitTests.Examples
{
    public class XUnit : IDisposable 
    {
        // http://xunit.github.io/docs/getting-started.html
        // Basic use of xUnit for creating unit tests.

        public XUnit()
        {
            /* Instead of [SetUp] & [TearDown], xUnit instead uses the constructor,
             * as the class is instantiated each time a test is called.
             * For [TearDown] you can implement IDisposable.Dispose as a replacement.
             * xUnit instantiates a new class for each test case.
             */
        }

        public void Dispose()
        {
            // Put [TearDown] code here.
        }

        // Facts are tests which are always true. They test invariant conditions.
        [Fact]
        public void PassTest()
        {
            // Example of a passing test.
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailTest()
        {
            // Example of a failing test.
            Assert.Equal(5, Add(2, 2));
        }

        static int Add(int x, int y)
        {
            return x + y;
        }

        // Theories are tests which are only true for a particular set of data.
        // Runs seperate tests for each of the InlineData lines, which makes it easy to run several times,
        // with different arguments.
        [Theory]
        [InlineData(3)] 
        [InlineData(5)]
        [InlineData(6)] // This fails, and will show in the Test Explorer as the culprit.
        public void Theory(int value)
        {
            Assert.True(IsOdd(value));
        }

        // Helper function.
        static bool IsOdd(int value)
        {
            return value%2 == 1;
        }
    }
}
