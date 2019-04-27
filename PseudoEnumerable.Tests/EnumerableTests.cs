using System;
using System.Collections;
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

        [Test]
        public void FilterWithNullSourceTest()
        {
            Func<int, bool> predicate = number => Math.Abs(number % 2) == 0;
            Assert.Throws<ArgumentNullException>(() => Enumerable.Filter(null, predicate));
        }

        [Test]
        public void FilterWithNullPredicateTest()
        {
            var array = new string[] { "one", "two", "three", "four" };
            Assert.Throws<ArgumentNullException>(() => Enumerable.Filter(array, null));
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

        [Test]
        public void ForAllWithPersonTest1()
        {
            var array = new Person[]
            {
                new Person("Jack", 21),
                new Person("Tom", 35),
                new Person("Jane", 12),
                new Person("Tom", 27),
                new Person("Alice", 60)
            };
            Func<Person, bool> predicate = person => person.Age < 30;
            Assert.AreEqual(false, Enumerable.ForAll(array, predicate));
        }

        [Test]
        public void ForAllWithPersonTest2()
        {
            var array = new Person[]
            {
                new Person("Jack", 21),
                new Person("Tom", 5),
                new Person("Jane", 12),
                new Person("Tom", 27),
                new Person("Alice", 5)
            };
            Func<Person, bool> predicate = person => person.Age < 30;
            Assert.AreEqual(true, Enumerable.ForAll(array, predicate));
        }

        [Test]
        public void ForAllWithNullSourceTest()
        {
            Func<string, bool> predicate = str => str.Length < 10;
            Assert.Throws<ArgumentNullException>(() => Enumerable.ForAll(null, predicate));
        }

        [Test]
        public void ForAllWithNullPredicateTest()
        {
            var array = new string[] { "one", "two", "three", "four" };
            Assert.Throws<ArgumentNullException>(() => Enumerable.ForAll(array, null));
        }

        #endregion

        #region Transform Tests

        [TestCase(new double[] { 0.001, 12, -65.09, 123.9, 0.5 }, ExpectedResult = new int[] { 0, 12, -65, 123, 0 })]
        [TestCase(new double[] { 0.0001, 0.0002, 0.0003, -0.0004, -0.004 }, ExpectedResult = new int[] { 0, 0, 0, 0, 0 })]
        [TestCase(new double[] { 13, 77, 90, -3, 7483274, -378 }, ExpectedResult = new int[] { 13, 77, 90, -3, 7483274, -378 })]
        public IEnumerable<int> TransformDoubleToIntTest(double[] array)
        {
            Func<double, int> transformer = d => (int)d;
            return Enumerable.Transform(array, transformer);
        }

        [TestCase(new char[] { 'a', '1', '@', '*', '7' }, ExpectedResult = new string[] { "a", "1", "@", "*", "7" })]
        [TestCase(new char[] { ' ', '0', '/', '9', 'т' }, ExpectedResult = new string[] { " ", "0", "/", "9", "т" })]
        public IEnumerable<string> TransformCharToStringTest(char[] array)
        {
            Func<char, string> transformer = ch => ch.ToString();
            return Enumerable.Transform(array, transformer);
        }

        [Test]
        public void TransformPointToStringTest()
        {
            var array = new Point[] { new Point(10, 20), new Point(-3, -4), new Point(0, 0), new Point(3, 1), new Point(-1, 1) };
            var expected = new string[] { "10", "-3", "0", "3", "-1" };
            Func<Point, string> transformer = ch => ch.X.ToString();
            Assert.AreEqual(expected, Enumerable.Transform(array, transformer));
        }

        [Test]
        public void TransformTupleToPersonTest()
        {
            var array = new (string, int)[] { ("Jack", 21), ("Tom", 35), ("Jane", 12), ("Alice", 60) };
            var expected = new Person[]
            {
                new Person("Jack", 21),
                new Person("Tom", 35),
                new Person("Jane", 12),
                new Person("Alice", 60)
            };
            Func<(string, int), Person> transformer = tuple => new Person(tuple.Item1, tuple.Item2);
            Assert.AreEqual(expected, Enumerable.Transform(array, transformer).ToArray());
        }

        [Test]
        public void TransformlWithNullDelegateTest()
        {
            var array = new string[] { "one", "two", "three", "four" };
            Assert.Throws<ArgumentNullException>(() => Enumerable.Transform<string, int>(array, null));
        }

        [Test]
        public void TransformlWithNullSourceTest()
        {
            Func<string, int> transformer = item => item.Length;
            Assert.Throws<ArgumentNullException>(() => Enumerable.Transform(null, transformer));
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

        [Test]
        public void SortByWithNullSourceTest()
        {
            Func<string, int> sorter = item => item.Length;
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortBy(null, sorter));
        }

        [Test]
        public void SortByWithNullDelegateTest()
        {
            var array = new int[] { 9, 100, 0, 237284, 3, 1 };
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortBy<int, string>(array, null));
        }

        [Test]
        public void SortByWithNullComparerTest()
        {
            var array = new string[] { "11111", "1111", "1", "11", "string", "1111111111", "1" };
            Func<string, int> sorter = item => item.Length;
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortBy(array, sorter, null));
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

        [Test]
        public void SortDescendingByWithNullSourceTest()
        {
            Func<string, int> sorter = item => item.Length;
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortByDescending(null, sorter));
        }

        [Test]
        public void SortByDescendingWithNullDelegateTest()
        {
            var array = new int[] { 9, 100, 0, 237284, 3, 1 };
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortByDescending<int, string>(array, null));
        }

        [Test]
        public void SortByDescendingWithNullComparerTest()
        {
            var array = new string[] { "11111", "1111", "1", "11", "string", "1111111111", "1" };
            Func<string, int> sorter = item => item.Length;
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortByDescending(array, sorter, null));
        }

        #endregion       

        #region Generator Tests

        [TestCase(-1, 12, ExpectedResult = new int[] { -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
        [TestCase(234, 1, ExpectedResult = new int[] { 234 })]
        [TestCase(7, 0, ExpectedResult = new int[] { })]
        public IEnumerable<int> GeneratorTest(int start, int count) => Enumerable.GenerateNumbers(start, count);

        [Test]
        public void GeneratorWithInvalidArgumentTest() =>
            Assert.Throws<ArgumentException>(() => Enumerable.GenerateNumbers(1, -1));

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

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}