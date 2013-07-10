using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeFactorSearchNS
{
    public class PrimeFactorSearch
    {
        #region Properties

        public List<int> inputIntegers { get; private set; } 
        public Dictionary<int, List<int>> primeFactors { get; private set; }

        #endregion

        #region Constructors

        public PrimeFactorSearch() 
        {
            this.inputIntegers = new List<int>();
            this.primeFactors = new Dictionary<int, List<int>>();
        }

        public PrimeFactorSearch(string integerFile)
        {
            this.inputIntegers = new List<int>();
            this.primeFactors = new Dictionary<int, List<int>>();

            this.Execute(integerFile);
        }

        #endregion

        #region Methods

        // Executes the prime factor search
        public Dictionary<int, List<int>> Execute(string integerFile)
        {
            this.inputIntegers = this.ReadFile(integerFile);
            foreach (int integer in this.inputIntegers)
            {
                this.primeFactors.Add(integer, this.PrimeFactor(integer));
            }

            return this.primeFactors;
        }

        // Read and parse a file that contains integers to find prime factors for
        public List<int> ReadFile(string integerFile)
        {
            List<int> inputData = new List<int>();
            using (StreamReader integerFileReader = new StreamReader(integerFile))
            {
                while (integerFileReader.Peek() >= 0)
                {
                    string line = integerFileReader.ReadLine();


                    // Assumption: Ignore non-numeric Characters
                    int parsedInteger;
                    bool validInteger = Int32.TryParse(line, out parsedInteger);

                    if (validInteger)
                    {
                        // Assumption: Ignore negative integers and zero -- Prime numbers are typically defined as a natural number greater than one which are only divisible by themselves and 1
                        // Assumption: Duplicate numbers in the file should be ignored
                        if(inputData.IndexOf(parsedInteger) == -1 && parsedInteger > 0)
                            inputData.Add(parsedInteger);
                    }
                    
                }
            }
            return inputData;
        }

        // Get a number's prime factors
        // Assumption: We aren't factoring huge numbers (like those used in cryptography) and we are sticking with numbers that will fit in an int -- this gets problem fantastically more computationally expensive as numbers get larger
        public List<int> PrimeFactor(int number)
        {
            List<int> primeFactors = new List<int>();

            // If the number is less than 1... 
            if (number < 1)
                return primeFactors;

            // If the number is prime, no sense continuing
            if (isPrime(number))
            {
                primeFactors.Add(number);
                return primeFactors;
            }

            for (int divisor = 2; divisor < number; divisor++)
            {
                if (number % divisor == 0)
                {
                    // This number is a factor of the number
                    int factor = number / divisor;

                    // By this point, any composite number that could be the divisor would be able to be factored 
                    // by a lower prime which would have been already tested, therefore the divisor should always
                    // be a prime. 
                    primeFactors.Add(divisor);

                    if (isPrime(factor))
                        primeFactors.Add(factor);
                    else
                        primeFactors.AddRange(this.PrimeFactor(factor)); // Factor out the composite number into its parts

                    // Boiled down to prime numbers, no need to continue trying to factor with larger numbers
                    break;
                }
            }
            return primeFactors;
        }

        // Test if a number is prime or not
        public bool isPrime(int number)
        {
            for (int test = 2; test < number; test++)
            {
                if (number % test == 0 && number != test)
                    return false;
            }

            return true;
        }

        #endregion

    }
}
