using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

    public class Globals //Global Variables
    {
        public static Stack<char> operatorStack = new Stack<char>(); //Operator char stack
        public static Stack<double> operandStack = new Stack<double>(); //Operand double stack
        public static Queue<string> Expression = new Queue<string>(); //Post Expression string queue. I have used a queue to be able to print it in the correct order and traverse it in the correct order (left to right)
    }


        public static void BODMAS1()
        {
            string a = "(18+5*2)+(10/(5-3))";
            int i = new int();
            int b = new int();
            double j = new double();
            double result = new double();
            string temp = string.Empty;
            SampleClass P = new SampleClass();

            while (i < a.Length)
            {
                switch (a[i])
                {
                    case '(':
                        Globals.operatorStack.Push(a[i]);
                        break;
                    case '^':
                        if (Globals.operatorStack.Count == 0) //if stack is empty, it should be pushed
                        {
                            Globals.operatorStack.Push(a[i]);
                        }
                        else
                        {
                            if (P.Precedence(a[i]) > P.Precedence(Globals.operatorStack.Peek()))
                            {
                                Globals.operatorStack.Push(a[i]);
                            }
                            else
                            {
                                Globals.Expression.Enqueue(Globals.operatorStack.Pop().ToString());
                                Globals.operatorStack.Push(a[i]);
                            }
                        }
                        break;
                    case '*':
                        if (Globals.operatorStack.Count == 0)
                        {
                            Globals.operatorStack.Push(a[i]);
                        }
                        else
                        {
                            if (P.Precedence(a[i]) > P.Precedence(Globals.operatorStack.Peek()))
                            {
                                Globals.operatorStack.Push(a[i]);
                            }
                            else
                            {
                                Globals.Expression.Enqueue(Globals.operatorStack.Pop().ToString());
                                Globals.operatorStack.Push(a[i]);
                            }
                        }
                        break;
                    case '/':
                        if (Globals.operatorStack.Count == 0)
                        {
                            Globals.operatorStack.Push(a[i]);
                        }
                        else
                        {
                            if (P.Precedence(a[i]) > P.Precedence(Globals.operatorStack.Peek()))
                            {
                                Globals.operatorStack.Push(a[i]);
                            }
                            else
                            {
                                Globals.Expression.Enqueue(Globals.operatorStack.Pop().ToString());
                                Globals.operatorStack.Push(a[i]);
                            }
                        }
                        break;
                    case '+':
                        if (Globals.operatorStack.Count == 0)
                        {
                            Globals.operatorStack.Push(a[i]);
                        }
                        else
                        {
                            if (P.Precedence(a[i]) > P.Precedence(Globals.operatorStack.Peek()))
                            {
                                Globals.operatorStack.Push(a[i]);
                            }
                            else
                            {
                                Globals.Expression.Enqueue(Globals.operatorStack.Pop().ToString());
                                Globals.operatorStack.Push(a[i]);
                            }
                        }
                        break;
                    case '-':
                        if (Globals.operatorStack.Count == 0)
                        {
                            Globals.operatorStack.Push(a[i]);
                        }
                        else
                        {
                            if (P.Precedence(a[i]) > P.Precedence(Globals.operatorStack.Peek()))
                            {
                                Globals.operatorStack.Push(a[i]);
                            }
                            else
                            {
                                Globals.Expression.Enqueue(Globals.operatorStack.Pop().ToString());
                                Globals.operatorStack.Push(a[i]);
                            }
                        }
                        break;
                    case ')':
                        temp = Globals.operatorStack.Pop().ToString();
                        while (temp != "(")
                        {
                            Globals.Expression.Enqueue(temp.ToString());
                            temp = Globals.operatorStack.Pop().ToString();
                        }
                        break;
                    case ' ': //if user enters a space in between, it should be skipped
                        break;
                    case '.': //this hasn't been handled as of now, so it is being skipped
                        break;
                    default: //we have a digit
                        string d = string.Empty;
                        while (int.TryParse(a[i].ToString(), out b)) //we can have multiple digits and they should be combined to a single number
                        {
                            d += a[i];
                            i++;
                        }
                        Globals.Expression.Enqueue(d);
                        i--;
                        break;
                }
                i++;
            }
            while (Globals.operatorStack.Count != 0) //Add operators remaining in the stack to the expression
            {
                Globals.Expression.Enqueue(Globals.operatorStack.Pop().ToString());
            }

            //Display Postfix Expression
            Console.WriteLine("Postfix Expression: ");
            foreach (string c in Globals.Expression)
                Console.Write(c + " ");
            Console.WriteLine();

            //Calculate the result by evaluating the postfix expression
            while (Globals.Expression.Count > 0)
            {
                temp = Globals.Expression.Dequeue();
                switch (temp)
                {
                    case "+":
                        j = Globals.operandStack.Pop();
                        result = Globals.operandStack.Pop() + j;
                        Globals.operandStack.Push(result);
                        break;
                    case "-":
                        j = Globals.operandStack.Pop();
                        result = Globals.operandStack.Pop() - j;
                        Globals.operandStack.Push(result);
                        break;
                    case "*":
                        j = Globals.operandStack.Pop();
                        result = Globals.operandStack.Pop() * j;
                        Globals.operandStack.Push(result);
                        break;
                    case "/":
                        j = Globals.operandStack.Pop();
                        result = Globals.operandStack.Pop() / j;
                        Globals.operandStack.Push(result);
                        break;
                    case "^":
                        j = Globals.operandStack.Pop();
                        result = Math.Pow(Globals.operandStack.Pop(), j);
                        Globals.operandStack.Push(result);
                        break;
                    default: Globals.operandStack.Push(double.Parse(temp));
                        break;
                }
            }
            result = Globals.operandStack.Pop();
            Console.WriteLine("Result: " + result);
        }
