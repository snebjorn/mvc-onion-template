using System;
using NSubstitute;
using Xunit;

namespace UnitTests.Examples
{
    public class NSubstitute
    {
        /* http://nsubstitute.github.io/
         * Basic use of NSubstitute
         * Instead of deciding either to use mock or stub, we just substitute.
         */

        private readonly ICalculator _calculator;

        // Parameterless constructor instead of [SetUp].
        public NSubstitute()
        {
            _calculator = Substitute.For<ICalculator>();
        }

        // How to set up a function on a substitute to a specific return value.
        [Fact]
        public void CalculatorAdd()
        {
            _calculator.Add(1, 2).Returns(3);
            Assert.Equal(_calculator.Add(1, 2), 3);
        }

        [Fact]
        public void CalculatorMode()
        {
            _calculator.Mode.Returns("DEC");
            Assert.Equal(_calculator.Mode, "DEC");
        }

        [Fact]
        public void CalculatorReturnSequence()
        {
            _calculator.Mode.Returns("HEX", "DEC", "BIN");
            Assert.Equal(_calculator.Mode, "HEX");
            Assert.Equal(_calculator.Mode, "DEC");
            Assert.Equal(_calculator.Mode, "BIN");
        }

        // How to check whether a function has or has not been called with specific arguments.
        [Fact]
        public void CalculatorReceivedCall()
        {
            _calculator.Add(1, 2);
            _calculator.Received().Add(1, 2);
            _calculator.DidNotReceive().Add(5, 7);
        }

        [Fact]
        public void CalculatorAddWithTen()
        {
            _calculator.Add(10, -5);
            _calculator.Received().Add(10, Arg.Any<int>());
            _calculator.Received().Add(10, Arg.Is<int>(x => x < 0));
        }

        // For testing with events.
        [Fact]
        public void CalculatorEventWasRaised()
        {
            var eventWasRaised = false;
            _calculator.PoweringUp += () => eventWasRaised = true;
            _calculator.PoweringUp += Raise.Event<Action>();
            Assert.True(eventWasRaised);
        }

        public interface ICalculator
        {
            int Add(int a, int b);
            string Mode { get; set; }
            event Action PoweringUp;
        }
    }
}
