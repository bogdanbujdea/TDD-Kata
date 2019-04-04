using System;
using FluentAssertions;
using NUnit.Framework;

namespace MyCalculator.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        private StringCalculator _sc;

        [Test]
        public void Add_EmptyString_ReturnsDefaultValue()
        {
            _sc = CreateStringCalculator();

            int result = _sc.Add("");

            Assert.AreEqual(StringCalculator.DefaultValue, result);
        }

        [TestCase("1", 1)]
        [TestCase("2", 2)]
        public void Add_SingleNumber_ReturnsNumber(string numbers, int expectedResult)
        {
            _sc = CreateStringCalculator();

            int result = _sc.Add(numbers);

            result.Should().Be(expectedResult);
        }

        [TestCase("1,2", 3)]
        [TestCase("1,2,3", 6)]
        [TestCase("1,2,3,4,5", 15)]
        public void Add_MultipleNumbers_ReturnsSum(string numbers, int expectedResult)
        {
            _sc = CreateStringCalculator();

            int result = _sc.Add(numbers);

            result.Should().Be(expectedResult);
        }

        [TestCase("1\n2,3", 6)]
        [TestCase("1,2\n3", 6)]
        [TestCase("1\n2", 3)]
        [TestCase("1\n2\n3", 6)]
        public void Add_WithNewLinesAndCommas_ReturnsSum(string numbers, int expectedResult)
        {
            _sc = CreateStringCalculator();

            int result = _sc.Add(numbers);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Add_EmptyStringWithDelimiter_ReturnsDefault()
        {
            _sc = new StringCalculator();

            int result = _sc.Add("//;\n");

            result.Should().Be(StringCalculator.DefaultValue);
        }

        [Test]
        public void Add_SingleDigitWithDelimiter_ReturnsNumber()
        {
            _sc = new StringCalculator();

            int result = _sc.Add("//;\n1");

            result.Should().Be(1);
        }

        [Test]
        public void Add_SingleNumberWithDelimiter_ReturnsNumber()
        {
            _sc = new StringCalculator();

            int result = _sc.Add("//;\n10");

            result.Should().Be(10);
        }

        [TestCase("//;\n10;11", 21)]
        [TestCase("//\n\n10\n11", 21)]
        public void Add_TwoNumbersWithDelimiter_ReturnsSum(string numbers, int expectedResult)
        {
            _sc = CreateStringCalculator();

            int result = _sc.Add(numbers);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void Add_NegativeNumber_ThrowsException()
        {
            _sc = CreateStringCalculator();

            Action action = () => _sc.Add("-1");

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Add_NegativeNumber_ThrowsExceptionWithMessage()
        {
            _sc = CreateStringCalculator();

            Action action = () => _sc.Add("-1");

            action.Should().Throw<ArgumentException>().WithMessage("negatives not allowed");
        }

        [Test]
        public void Add_NegativeNumber_ThrowsExceptionWithMessageContainingNumbers()
        {
            _sc = CreateStringCalculator();

            Action action = () => _sc.Add("-1");

            action.Should().Throw<ArgumentException>().WithMessage("negatives not allowed");
        }

        private static StringCalculator CreateStringCalculator()
        {
            return new StringCalculator();
        }
    }
}
