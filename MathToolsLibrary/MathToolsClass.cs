using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathToolsLibrary
{
    public static class MathTools
    {
        public static int MCD(int numberA, int numberB)
        {
            if (numberA == 0 || numberB == 0)
            {
                return 0;
            }

            int max = Math.Max(numberA, numberB);
            int min = Math.Min(numberA, numberB);
            int mcd = min;

            if (max % min == 0)
            {
                return mcd;
            }
            else
            {
                while (min != 0)
                {
                    mcd = min;
                    min = max % min;
                    max = mcd;
                }
                return mcd;
            }
        }


        public static int MCM(int numberA, int numberB)
        {
            int max = Math.Max(numberA, numberB);
            int min = Math.Min(numberA, numberB);
            if (min == 0)
            {
                return max;
            }

            if (max % min == 0)
            {
                return max;
            }
            else
            {
                int counter = max;
                while (counter % min != 0)
                {
                    counter += max;
                }
                return counter;
            }
        }


        public static int Factorial(int number)
        {
            int result = number;
            do
            {
                number--;
                result += number;

            } while (number >= 1);

            return result;
        }


        public static bool IsPair(int number)
        {
            return number % 2 == 0;
        }


        public static bool IsPrime(int number)
        {
            if (number == 0)
            {
                return false;
            }

            for (int i = number / 2; i > 1; i--)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }


        public static bool IsDivisible(int dividend, int divisor)
        {
            if (divisor == 0)
            {
                return false;
            }
            return dividend % divisor == 0;
        }


        //Binary
        private static int NumberTimes(int numberTimes)
        {
            int counter = 1;
            while (numberTimes != 1)
            {
                numberTimes /= 2;
                counter++;
            }
            return counter;
        }

        public static int ToBinary(int number)
        {

            if (number == 0)
            {
                return 0;
            }
            if (number == 1)
            {
                return 1;
            }

            int counter = NumberTimes(number);
            int[] binaryNumber = new int[counter];

            for (int i = counter - 2; i > -1; i--)
            {
                if (number % 2 == 0)
                {
                    binaryNumber[counter - 1] = 0;
                }
                else
                {
                    binaryNumber[counter - 1] = 1;
                }

                number /= 2;
                if (number % 2 == 0)
                {
                    binaryNumber[i] = 0;
                }
                else
                {
                    binaryNumber[i] = 1;
                }

            }

            return int.Parse(string.Join("", binaryNumber));
        }


        //To Octal
        public static int ToOctal(int input)
        {
            int counter = Counter(input, 8);
            int[] octal = new int[counter];

            if (input < 8)
            {
                return input;
            }
            else
            {
                for (int i = counter - 1; i > 0; i--)
                {
                    octal[i] = input % 8;
                    input /= 8;

                    if (input <= 8)
                    {
                        octal[0] = input;
                    }

                }
                return int.Parse(string.Join("", octal));
            }

        }

        private static int Counter(int number, int @base)
        {
            int counter = 0;
            if (number < @base)
            {
                counter = 1;
            }
            else
            {
                do
                {
                    number /= @base;
                    counter++;
                } while (number > 0);
            }
            return counter;
        }

        // TO HEXADECIMAL
        public static string ToHexadecimal(int input)
        {
            int counter = CounterHexa(input);
            string[] equivalents = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            string[] hexadecimals = new string[counter];

            for (int i = counter - 1; i >= 0; i--)
            {
                if (input < 16)
                {
                    hexadecimals[i] = equivalents[input];
                }
                else
                {
                    hexadecimals[i] = equivalents[input % 16];
                    input /= 16;
                }
            }

            return string.Join("", hexadecimals);
        }

        private static int CounterHexa(int number)
        {
            int counter = 0;
            do
            {
                number /= 16;
                counter++;

            } while (number > 0);

            return counter;
        }


        //BINARY SEARCH
        public static int BinarySearch(int[] list, int value)
        {
            int length = list.Length;
            int centralValue;
            int limitMin = 0, limitMax = length;

            Exception(list[0], list[list.Length - 1], value);

            do
            {
                centralValue = limitMin + (limitMax - limitMin) / 2;

                if (value > list[centralValue])
                {
                    limitMin = centralValue;
                }
                else
                {
                    limitMax = centralValue;
                }


            } while (list[centralValue] != value);

            return centralValue;
        }

        private static void Exception(int min, int max, int value)
        {
            if (value > max || value < min)
            {
                Console.WriteLine("The value to find is outside the limits of the array");
                return;
            }
        }
        

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~BIG NUMBERS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public static string AddBigNumbers(string numA, string numB)
        {
            int[] numberA = StringToArray(numA);
            int[] numberB = StringToArray(numB);

            numberA = Reverse(numberA);
            numberB = Reverse(numberB);

            int[] bigger;
            int[] minor;

            ////////////// PARA DETERMINAR EL MAYOR Y EL MENOR DE LOS NUMEROS DE ENTRADA ///////////////
            if (numA.Length > numB.Length)
            {
                bigger = numberA;
                minor = numberB;
            }
            else
            {
                minor = numberA;
                bigger = numberB;
            }
            //////////////

            int[] finalResult = MainOperation(bigger, minor);
            finalResult = Reverse(finalResult);

            string result = "";

            for (int i = 0; i < finalResult.Length; i++)
            {
                result += finalResult[i];
            }

            Console.WriteLine();

            return result;
        }


        private static int[] StringToArray(string input)
        {
            char[] number = input.ToCharArray();
            int[] finalNumber = Array.ConvertAll(number, c => (int)Char.GetNumericValue(c));   //Esto lo busqué en Google para poder convertir char[] a int[]

            return finalNumber;
        }

        private static int[] Reverse(int[] array)
        {
            Array.Reverse(array);
            return array;
        }

        private static int[] MainOperation(int[] bigger, int[] minor)
        {
            int[] result = new int[bigger.Length];

            for (int i = 0; i < bigger.Length; i++)
            {
                if (i >= minor.Length)
                {
                    result[i] = bigger[i];
                    continue;
                }

                result[i] = bigger[i] + minor[i];

            }

            //reajusta los valores del array para sumar los restos de los q sean >= 10
            result = Readjust(result);

            return result;
        }

        private static int[] Readjust(int[] number)
        {
            int[] result = number;

            while (AreAllTemsOneChar(number))
            {
                for (int i = 0; i < number.Length; i++)
                {
                    if (number[i] >= 10)
                    {
                        number[i] = number[i] - 10;

                        //en caso de que haya que aumentar el tamaño del array +1
                        if (i == number.Length - 1)
                        {
                            number = LastNumber(number);
                        }
                        else
                        {
                            number[i + 1]++;
                        }

                    }
                }
            }
            result = number;

            return result;

        }

        private static int[] LastNumber(int[] numb)
        {
            int[] toReturn = new int[numb.Length + 1];
            int rest = numb[numb.Length - 1] - 10;

            toReturn[toReturn.Length - 1] = 1;

            if (toReturn[toReturn.Length - 2] != 0)
            {
                toReturn[toReturn.Length - 2] = rest;
            }

            for (int i = 0; i < numb.Length; i++)
            {
                toReturn[i] = numb[i];
            }

            return toReturn;
        }


        private static bool AreAllTemsOneChar(int[] numb)
        {
            bool response = false;

            for (int i = 0; i < numb.Length; i++)
            {
                if (numb[i] >= 10)
                {
                    return true;
                }
                else
                {
                    response = false;
                }
            }

            return response;
        }
    }

}
