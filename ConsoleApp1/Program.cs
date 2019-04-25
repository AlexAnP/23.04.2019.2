using System;
using System.Collections.Generic;
using System.Linq;
using PseudoEnumerable;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new string[] { "one", "two", "three", "four" };

            var result = array.ForAll(s => s.Length > 3);
            var resultLinq = array.All(s => s.Length > 3);

            Console.WriteLine(result.ToString());
            Console.WriteLine(resultLinq.ToString());

            //foreach (var item in result)
            //{
            //    Console.Write($"{item} ");
            //}

            //Console.WriteLine();

            //foreach (var item in resultLinq)
            //{
            //    Console.Write($"{item} ");
            //}

            Console.Read();
        }              
    }    
}
