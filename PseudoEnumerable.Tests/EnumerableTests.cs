using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        [TestCase(new int[] { 2, 77, 43, -12, 0, 20, -31 }, ExpectedResult = new int[] { 2, -12, 0, 20 })]
        [TestCase(new int[] { 1, 333, 4, 55, -1, 89, 1 }, ExpectedResult = new int[] { 4 })]
        [TestCase(new int[] { int.MinValue, int.MaxValue }, ExpectedResult = new int[] { int.MinValue })]
        public int[] FilterWithEvenNumberTest(int[] array)
        {
            Func<int, bool> predicate = number => Math.Abs(number % 2) == 0;
            return Enumerable.Filter(array, predicate).ToArray();
        }

        [TestCase(arg:new string[] { "one", "two" , "three", "four" }, ExpectedResult = new string[] {"one", "two", "four"})]        
        public string[] FilterWithStringContainsCharTest(string[] array)
        {
            Func<string, bool> predicate = str => str.Contains('o');
            return Enumerable.Filter(array, predicate).ToArray();
        }
    }
}