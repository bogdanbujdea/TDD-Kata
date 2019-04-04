using System;
using System.Linq;

namespace MyCalculator
{
    public class StringCalculator
    {
        public const int DefaultValue = 0;

        public int Add(string numbers)
        {
            if (numbers.Contains("-"))
            {
                throw new ArgumentException("negatives not allowed");
            }
            if (numbers.StartsWith("//"))
            {
                numbers = ParseNumbersWithDelimiter(numbers);
            }
            if (numbers == "")
            {
                return DefaultValue;
            }

            return AddNumbers(numbers);
        }

        private string ParseNumbersWithDelimiter(string numbers)
        {
            var delimiter = GetDelimiter(numbers);
            var numbersLine = GetNumbers(numbers);
            numbersLine = numbersLine.Replace(delimiter, ',');
            return numbersLine;
        }

        private char GetDelimiter(string numbers)
        {
            return numbers[2];
        }

        private string GetNumbers(string numbers)
        {
            return numbers.Substring(4);
        }

        private static int AddNumbers(string numbers, params char[] delimiters)
        {
            var defaultDelimiters = new[] { ',', '\n' };
            return numbers.Split(defaultDelimiters.Concat(delimiters).ToArray())
                .Select(int.Parse)
                .Sum();
        }
    }
}