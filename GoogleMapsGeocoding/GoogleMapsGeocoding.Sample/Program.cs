// <copyright file="Program.cs" company="">
// All rights reserved.
// </copyright>
// <author>Alberto Puyana</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMapsGeocoding.Sample
{
    /// <summary>
    /// Sample console app.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Arguments of the program.</param>
        static void Main(string[] args)
        {
            var task = TestCoder.AllAsync();

            task.Wait();

            Console.WriteLine(string.Empty);
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }
    }
}
