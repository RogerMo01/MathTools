using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathToolsLibrary
{
    public class Calculator
    {
        // Requiere como parámetro un string de notación algebraica con espacios entre carácteres
        public static decimal Calculate(string algebraicExpession)
        {
            string[] rpnExpression = ToRPN(algebraicExpession);

            LinkedList<string> stack = new LinkedList<string>();
            LinkedList<string> output = new LinkedList<string>();

            foreach (var element in rpnExpression)
            {
                if (IsNumber(element))
                    stack.AddLast(element);
                else
                {
                    if (stack.Count < 2)
                        throw new Exception("Not enough arguments have been entered");

                    decimal newValue = Operate(decimal.Parse(stack.Last.Previous.Value), decimal.Parse(stack.Last.Value), element);

                    stack.RemoveLast();
                    stack.RemoveLast();
                    stack.AddLast(Convert.ToString(newValue));
                }
            }

            if (stack.Count > 1)
            {
                throw new Exception("Too much elements have been entered");
            }

            return decimal.Parse(stack.First.Value);
        }

        public static string[] ToRPN(string input)
        {
            string[] tokens = input.Split(' ');
            LinkedList<string> stack = new LinkedList<string>();
            LinkedList<string> output = new LinkedList<string>();
            string[] operatorSymbols = { "^", "/", "*", "+", "-" };

            foreach (var token in tokens)
            {
                if (IsNumber(token))
                {
                    output.AddLast(token);
                    continue;
                }

                if (IsOperator(token, operatorSymbols))
                {
                    if (stack.Count > 0)
                    {
                        while (IsOperator(stack.First.Value, operatorSymbols))
                        {
                            // If o1 es asociativo izquierdo y su precedencia es menor que (una precedencia más baja) o igual a la de o2
                            //  ó o1 es asociativo derecho y su precedencia es menor que(una precedencia más baja) que la de o2
                            if ((Association(token) < 0 && GetPrecedence(token) <= GetPrecedence(stack.First.Value)) || (Association(token) > 0 && GetPrecedence(token) < GetPrecedence(stack.First.Value)))
                            {
                                output.AddLast(stack.First.Value);
                                stack.RemoveFirst();

                                if (stack.Count == 0)
                                    break;
                                else continue;
                            }
                            break;
                        }
                    }

                    stack.AddFirst(token);
                    continue;
                }

                if (token == "(")
                    stack.AddFirst(token);

                if (token == ")")
                {
                    while (stack.First.Value != "(")
                    {
                        output.AddLast(stack.First.Value);
                        stack.RemoveFirst();

                        if (stack.First == null)
                            throw new Exception("Pharentheses are not balanced");
                    }

                    stack.RemoveFirst();
                    //Si el token en el tope de la pila es un token de función, póngalo (pop) en la cola de salida.
                }
            }

            while (stack.Count > 0)
            {
                if (output.First.Value == "(")
                    throw new Exception("Pharentheses are not balanced");

                output.AddLast(stack.First.Value);
                stack.RemoveFirst();
            }

            return output.ToArray();
        }

        private static bool IsNumber(string token)
        {
            foreach (var item in token)
            {
                if (Char.IsNumber(item) || item == '.')
                    continue;
                else return false;
            }
            return true;
        }

        public static bool IsOperator(string token, string[] operatorSymbols)
        {
            return operatorSymbols.Any(x => x == token);
        }

        private static void AddToStack(string value, string[] functionSymbols)
        {
            // HEREEEEEEEEEEEEEEEEEEEEEEEEEE
        }

        private static int Association(string operatorA)
        {
            return (operatorA == "^") ? 1 : -1;
        }

        private static int GetPrecedence(string operatorO1)
        {
            if (operatorO1 == "^")
                return 3;
            if (operatorO1 == "/" || operatorO1 == "*")
                return 2;
            if (operatorO1 == "+" || operatorO1 == "-")
                return 1;

            throw new Exception("Token is not an operator");
        }

        private static decimal Operate(decimal numberA, decimal numberB, string Operator)
        {
            switch (Operator)
            {
                case "+":
                    return numberA + numberB;
                case "-":
                    return numberA - numberB;
                case "*":
                    return numberA * numberB;
                case "/":
                    return numberA / numberB;
                case "^":
                    return (decimal)Math.Pow((double)numberA, (double)numberB);
                default:
                    throw new Exception("Unknown operator");
            }
        }
    }
}
