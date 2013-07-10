using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeFactorSearchNS
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() > 0)
            {
                // Assumption: Somebody is going to try breaking this by passing in multiple arguments, no arguments, or who knows what else
                // Just in case someone passes mulitple arguments in, assume they are filepaths
                foreach (string argument in args)
                {
                    if (File.Exists(argument))
                    {
                        PrimeFactorSearch primeFactorSearch = new PrimeFactorSearch(argument);

                        // Friendly message if no numbers were in the file
                        if (primeFactorSearch.primeFactors.Count > 0)
                        {
                            foreach (var p in primeFactorSearch.primeFactors)
                            {
                                StringBuilder outputBuilder = new StringBuilder();

                                outputBuilder.Append("The prime factors of " + p.Key.ToString() + " are: ");

                                int i = 0;
                                foreach (var v in p.Value)
                                {
                                    outputBuilder.Append(v.ToString());
                                    i++;    // So one knows when a comma should be appended
                                    if (i < p.Value.Count)
                                        outputBuilder.Append(", ");
                                }

                                Console.WriteLine(outputBuilder.ToString());
                            }
                        }
                        else
                            Console.WriteLine("No integers to factor in file {0}", argument);
                    }
                    else
                        Console.WriteLine("The file {0} does not exist or could not be found.", argument);

                    Console.WriteLine("=======================================================");
                }
            }
            else
            {
                Console.WriteLine("No arguments passed in...");
            }

            Console.WriteLine("Press the enter key to exit...");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                // Don't exit until enter is pushed
            }
        }
    }
}
