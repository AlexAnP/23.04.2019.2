using System;
using System.Collections.Generic;
using System.Linq;
using NET1.S._2019.Piskur._07.Classes;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region Filter tests

        [TestCase(new int[] { 2, 77, 43, -12, 0, 20, -31 }, ExpectedResult = new int[] { 2, -12, 0, 20 })]
        [TestCase(new int[] { 1, 333, 4, 55, -1, 89, 1 }, ExpectedResult = new int[] { 4 })]
        [TestCase(new int[] { int.MinValue, int.MaxValue }, ExpectedResult = new int[] { int.MinValue })]
        public IEnumerable<int> FilterWithEvenNumberTest(int[] array)
        {
            Func<int, bool> predicate = number => Math.Abs(number % 2) == 0;
            return Enumerable.Filter(array, predicate);
        }

        [TestCase(arg1: new string[] { "one", "two", "three", "four" }, arg2: "o", ExpectedResult = new string[] { "one", "two", "four" })]
        [TestCase(arg1: new string[] { "there", "are", "no", "strings", "that", "matched" }, arg2: "Alex", ExpectedResult = new string[] { })]
        [TestCase(arg1: new string[] { ".NET training" }, arg2: " ", ExpectedResult = new string[] { ".NET training" })]
        [TestCase(arg1: new string[] { "", "123", "tests for filter" }, arg2: "", ExpectedResult = new string[] { "", "123", "tests for filter" })]
        public IEnumerable<string> FilterWithStringContainsCharTest(string[] array, string s)
        {
            Func<string, bool> predicate = str => str.Contains(s);
            return Enumerable.Filter(array, predicate);
        }

        [TestCase(arg: new string[] { "one", "two", "three", "four" }, ExpectedResult = new string[] { "one", "two", "three" })]
        [TestCase(arg: new string[] { "there ", "is", "no", "string", "that", "match " }, ExpectedResult = new string[] { })]
        [TestCase(arg: new string[] { ".NET training" }, ExpectedResult = new string[] { ".NET training" })]
        [TestCase(arg: new string[] { "", "123", "tests for filter" }, ExpectedResult = new string[] { "123" })]
        public IEnumerable<string> FilterWithStringLenghtIsNotEvenTest(string[] array)
        {
            Func<string, bool> predicate = str => Math.Abs(str.Length % 2) != 0;
            return Enumerable.Filter(array, predicate).ToArray();
        }

        [Test]
        public void FilterWithPointsTest()
        {
            var array = new Point[] { new Point(10, 20), new Point(-3, -4), new Point(0, 0), new Point(3, 1), new Point(-1, 1) };
            var expected = new Point[] { new Point(-3, -4), new Point(3, 1) };
            Func<Point, bool> predicate = point => point.X > point.Y;
            Assert.AreEqual(expected, Enumerable.Filter(array, predicate));
        }

        #endregion

        #region ForAll Tests

        [TestCase(new int[] { 2, 77, 43, 12, 0, 20, 31 }, ExpectedResult = true)]
        [TestCase(new int[] { 1, 333, 4, 55, -1, 89, 1 }, ExpectedResult = false)]
        [TestCase(new int[] { int.MaxValue, 0, 3, 4, 0, 999999, 123, int.MaxValue }, ExpectedResult = true)]
        [TestCase(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1 }, ExpectedResult = false)]
        public bool ForAllWithIntegerTest(int[] array)
        {
            Func<int, bool> predicate = number => number >= 0;
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

        #region SortBy Ascending Tests

        [Test]
        public void SortByWithoutComparerTest1()
        {
            var array = new int[] { 9, 100, 0, 237284, 3, 1 };
            var expected = new int[] { 0, 1, 100, 237284, 3, 9 };
            Func<int, string> sorter = item => item.ToString();
            Assert.AreEqual(expected, Enumerable.SortBy(array, sorter).ToArray());
        }

        [Test]
        public void SortByWithoutComparerTest2()
        {
            var array = new string[] { "11111", "1111", "1", "11", "string", "1111111111", "1" };
            var expected = new string[] { "1", "1", "11", "1111", "11111", "string", "1111111111" };
            Func<string, int> sorter = item => item.Length;
            Assert.AreEqual(expected, Enumerable.SortBy(array, sorter).ToArray());
        }

        [Test]
        public void SortByWithoutComparerTest3()
        {
            var array = new int[] { 845, -9, 12, 3, 14, -666 };
            var expected = new int[] { -666, -9, 3, 12, 14, 845 };
            Func<int, int> sorter = item => item;
            Assert.AreEqual(expected, Enumerable.SortBy(array, sorter).ToArray());
        }

        [Test]
        public void SortByWithComparerTest()
        {
            var array = new int[] { 9, 100, 0, 237284, 3, 1 };
            var expected = new int[] { 9, 0, 3, 1, 100, 237284 };
            var comparer = new SortAscending();
            Func<int, string> sorter = item => item.ToString();
            Assert.AreEqual(expected, Enumerable.SortBy(array, sorter, comparer).ToArray());
        }

        #endregion

        #region SortBy Descending Tests

        [Test]
        public void SortByDescendingWithoutComparerTest1()
        {
            var array = new int[] { 9, 100, 0, 237284, 3, 1 };
            var expected = new int[] { 9, 3, 237284, 100, 1, 0 };
            Func<int, string> sorter = item => item.ToString();
            Assert.AreEqual(expected, Enumerable.SortByDescending(array, sorter).ToArray());
        }

        [Test]
        public void SortByDescendingWithoutComparerTest2()
        {
            var array = new string[] { "11111", "1111", "1", "11", "string", "1111111111", "1" };
            var expected = new string[] { "1111111111", "string", "11111", "1111", "11", "1", "1" };
            Func<string, int> sorter = item => item.Length;
            Assert.AreEqual(expected, Enumerable.SortByDescending(array, sorter).ToArray());
        }

        [Test]
        public void SortByDescendingWithoutComparerTest3()
        {
            var array = new int[] { 845, -9, 12, 3, 14, -666 };
            var expected = new int[] { 845, 14, 12, 3, -9, -666 };
            Func<int, int> sorter = item => item;
            Assert.AreEqual(expected, Enumerable.SortByDescending(array, sorter).ToArray());
        }

        [Test]
        public void SortByDescendingWithComparerTest()
        {
            var array = new int[] { 9, 100, 0, 237284, 3, 1 };
            var expected = new int[] { 237284, 100, 9, 0, 3, 1 };
            var comparer = new SortAscending();
            Func<int, string> sorter = item => item.ToString();
            Assert.AreEqual(expected, Enumerable.SortByDescending(array, sorter, comparer).ToArray());
        }

        #endregion
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}