using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using PrimeFactorSearchNS;

namespace PrimeFactorSearchTests
{
    [TestFixture]
    public class PrimeFactorSearchTests
    {
        [Test]
        public void ReadIntegersFromFile()
        {
            List<string> inputData = new List<string>();
            List<int> expected = new List<int>();
            for (int i = 1; i <= 25; i++)
            {
                inputData.Add(i.ToString());
                expected.Add(i);
            }

            string inputFile = CreateTestFile(inputData);

            PrimeFactorSearch primeFactorSearch = new PrimeFactorSearch();

            List<int> result = primeFactorSearch.ReadFile(inputFile);
            //List<int> result = primeFactorSearch.inputIntegers;

            CollectionAssert.AreEqual(expected, result);
        }

        // Assumption: Ignore non-numeric Characters
        [Test]
        public void IgnoreNonNumericCharactersInFile()
        {
            List<string> inputData = new List<string>();
            List<int> expected = new List<int>();
            for (int i = 1; i <= 25; i++)
            {
                inputData.Add(i.ToString());
                inputData.Add(i.ToString() + "!@$%(*&^%$");
                expected.Add(i);
            }
            inputData.Add("a23bcd");
            inputData.Add("hello");
            inputData.Add("world!");
            inputData.Add("$%^foo");
            inputData.Add("baR567");

            string inputFile = CreateTestFile(inputData);

            PrimeFactorSearch primeFactorSearch = new PrimeFactorSearch();

            List<int> result = primeFactorSearch.ReadFile(inputFile);
            //List<int> result = primeFactorSearch.inputIntegers;

            CollectionAssert.AreEqual(expected, result);            
        }

        // Assumption: Ignore negative integers and zero -- Prime numbers are typically defined as a natural number greater than one which are only divisible by themselves and 1
        [Test]
        public void IgnoreNegativeIntegersInFile()
        {
            List<string> inputData = new List<string>();
            List<int> expected = new List<int>();
            inputData.Add("0");
            for (int i = 1; i <= 25; i++)
            {
                inputData.Add(i.ToString());
                // Hacky heh, heh
                inputData.Add("-" + i.ToString());
                expected.Add(i);
            }

            string inputFile = CreateTestFile(inputData);

            PrimeFactorSearch primeFactorSearch = new PrimeFactorSearch();

            List<int> result = primeFactorSearch.ReadFile(inputFile);
            //List<int> result = primeFactorSearch.inputIntegers;

            CollectionAssert.AreEqual(expected, result);
        }

        // Assumption: Duplicate numbers in the file should be ignored
        [Test]
        public void IgnoreDuplicateIntegersInFile()
        {
            List<string> inputData = new List<string>();
            List<int> expected = new List<int>();
            for (int i = 1; i <= 25; i++)
            {
                inputData.Add(i.ToString());
                inputData.Add(i.ToString());
                expected.Add(i);
            }

            string inputFile = CreateTestFile(inputData);

            PrimeFactorSearch primeFactorSearch = new PrimeFactorSearch();

            List<int> result = primeFactorSearch.ReadFile(inputFile);
            //List<int> result = primeFactorSearch.inputIntegers;

            CollectionAssert.AreEqual(expected, result);
        }

        // The prime factorization of 1 should just be 1 since 1 * 1 = 1
        [Test]
        public void PrimeFactorizationOfOne()
        {
            PrimeFactorSearch primeFactorSearch = new PrimeFactorSearch();
            int input = 1;
            List<int> result = primeFactorSearch.PrimeFactor(input);

            List<int> expected = new List<int>();
            expected.Add(1);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void PrimeFactorsMultipliedEqualOriginalNumber()
        {
            PrimeFactorSearch primeFactorSearch = new PrimeFactorSearch();
            int input = 100;
            List<int> factors = primeFactorSearch.PrimeFactor(input);

            int result = -1;
            foreach (int i in factors)
            {
                if (result == -1)
                    result = i;
                else
                    result *= i;
            }

            Assert.AreEqual(input, result);
        }

        [Test]
        public void NegativeNumbersHaveNoPrimeFactorization()
        {
            PrimeFactorSearch primeFactorSearch = new PrimeFactorSearch();
            int input = -1000;
            List<int> result = primeFactorSearch.PrimeFactor(input);

            // No factors should mean a empty list
            List<int> expected = new List<int>();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ZeroHasNoPrimeFactorization()
        {
            PrimeFactorSearch primeFactorSearch = new PrimeFactorSearch();
            int input = 0;
            List<int> result = primeFactorSearch.PrimeFactor(input);

            // No factors should mean a empty list
            List<int> expected = new List<int>();

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void IsPrimeCorrect()
        {
            PrimeFactorSearch primeFactorSearch = new PrimeFactorSearch();

            Dictionary<int, bool> testIntegers = new Dictionary<int, bool>();
            testIntegers.Add(2, true);
            testIntegers.Add(3, true);
            testIntegers.Add(97, true);
            testIntegers.Add(253763, true);

            testIntegers.Add(4, false);
            testIntegers.Add(10, false);            
            testIntegers.Add(1000, false);
            testIntegers.Add(256859, false);
            

            foreach (var i in testIntegers)
            {
                Assert.AreEqual(i.Value, primeFactorSearch.isPrime(i.Key));
            }
        }

        // Helper method, creates the file used by PrimeFactorSearch and returns path to test file
        private string CreateTestFile(List<string> fileData)
        {
            string fileName = @".\PrimeFactorTestData.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            using (StreamWriter testFileStreamWriter = new StreamWriter(fileName))
            {
                foreach (string line in fileData)
                {
                    testFileStreamWriter.WriteLine(line);
                }
            }

            return fileName;
        }

    }
}
