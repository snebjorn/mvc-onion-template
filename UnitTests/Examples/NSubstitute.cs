using System;
using NSubstitute;
using UnitTests.Examples.Classes;
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

        // Example of setting a return value to a substitute.
        [Fact]
        public void CalculatorAdd()
        {
            _calculator.Add(1, 2).Returns(3);
            Assert.Equal(_calculator.Add(1, 2), 3);
        }

        // Example of setting a return value to a substitute.
        [Fact]
        public void CalculatorMode()
        {
            _calculator.Mode.Returns("DEC");
            Assert.Equal(_calculator.Mode, "DEC");
        }

        // Defining several arguments to Returns, will return them in that sequence when called
        // several times.
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

        // Test that tests whether the function was called with 10 as the first argument
        // and with negative second argument.
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

        // Substitutes are created from interfaces.
        public interface ICalculator
        {
            int Add (int a, int b);
            string Mode { get; set; }
            event Action PoweringUp;
        }
    }
}
