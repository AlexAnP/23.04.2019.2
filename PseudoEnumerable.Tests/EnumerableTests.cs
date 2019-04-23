using System;
using System.Collections.Generic;
using NUnit;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region filter tests

        [TestCase(new int[] { 2, 77, 43, -12, 0, 20, -31 }, ExpectedResult = new int[] { 2, -12, 0, 20 })]
        [TestCase(new int[] { 1, 333, 4, 55, -1, 89, 1 }, ExpectedResult = new int[] { 4 })]
        [TestCase(new int[] { int.MinValue, int.MaxValue }, ExpectedResult = new int[] { int.MinValue })]
        public IEnumerable<int> FilterWithEvenNumberTest(int[] array)
        {
            Func<int, bool> predicate = number => Math.Abs(number % 2) == 0;
            return Enumerable.Filter(array, predicate);
        }

        [TestCase(arg1: new string[] { "one", "two", "three", "four" }, arg2: "o",  ExpectedResult = new string[] {"one", "two", "four"})]
        [TestCase(arg1: new string[] {"there", "are", "no", "string", "that", "matched"}, arg2: "W", ExpectedResult = new string[] {})]
        public IEnumerable<string> FilterWithStringContainsCharTest(string[] array, string s)
        {
            Func<string, bool> predicate = str => str.Contains(s);
            return Enumerable.Filter(array, predicate);
        }

        #endregion

        #region ForAll Tests

        [TestCase(new int[] { 2, 77, 43, 12, 0, 20, 31 }, ExpectedResult = true)]
        [TestCase(new int[] { 1, 333, 4, 55, -1, 89, 1 }, ExpectedResult = false)]
        [TestCase(new int[] { int.MaxValue,0, 3, 4, 0, 999999, 123, int.MaxValue }, ExpectedResult = true)]
        [TestCase(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1}, ExpectedResult = false)]
        public bool ForAllWithIntegerTest(int[] array)
        {
            Func<int, bool> predicate = number => number>=0;
            return Enumerable.ForAll(array, predicate);
        }

        [TestCase(arg: new string[] { "six", "google", "cSharp", "notsix", "" }, ExpectedResult = false)]
        [TestCase(arg: new string[] { "123456", "google", "cSharp", "notsix", "string" }, ExpectedResult = true)]
        public bool ForAllWithStringTest(string[] array)
        {
            Func<string, bool> predicate = str => str.Length == 6;
            return Enumerable.ForAll(array, predicate);
        }

        #endregion
    }
}