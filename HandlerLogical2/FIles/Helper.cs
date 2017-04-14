using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlerLogical2.Files
{
    class Helper
    {
        public static string[] getNextOperator(string function)
        {
            string[] result = new string[2];
            int index = -1;
            char op = '0';
            if (function.IndexOf("*") != -1)
            {
                result[0] = function.IndexOf("*").ToString();
                result[1] = "*";
                return result;
            }
            else if (function.IndexOf("/") != -1)
            {
                result[0] = function.IndexOf("/").ToString();
                result[1] = "/";
                return result;
            }
            else
            {
                while (op == '0')
                {
                    index++;
                    char caracter = char.Parse(function.Substring(index, 1));
                    switch (caracter)
                    {
                        case '+':
                            op = '+';
                            break;
                        case '-':
                            op = '-';
                            break;
                    }
                }
            }
            result[0] = index.ToString();
            result[1] = op.ToString();
            return result;
        }
        public static bool existOperator(string function)
        {
            return (function.Contains("+") || function.Contains("/") || function.Contains("*") || function.Contains("-"));
        }
        public static double calc(double n1, char op, double n2)
        {
            double result = 0;
            switch (op)
            {
                case '+':
                    result = n1 + n2;
                    break;
                case '-':
                    result = n1 - n2;
                    break;
                case '*':
                    result = n1 * n2;
                    break;
                case '/':
                    result = n1 / n2;
                    break;
            }
            return result;
        }

        public static int[] getABCOfEquation(string function)
        {
            int[] result = new int[3];
            bool negative = false;
            int auxi = 0;
    
            #region 1
            if (existOperator(function.Substring(0, 1)))
            {
                if (function.Substring(0, 1).Equals("-"))
                    negative = true;
                function = function.Substring(1, function.Length - 1);
            }

            if (function.IndexOf("x²") == 0)
            {
                if (negative)
                    result[0] = -1;
                else
                    result[0] = 1;
                function = function.Replace("x²", "");
            }
            else
            {
                auxi = int.Parse(Helper.getPreviousNumber(function, function.IndexOf("x²")));
                if (negative)
                    result[0] = - auxi;
                else
                    result[0] = auxi;
                function = function.Replace(auxi.ToString() + "x²", "");
            }
            #endregion
            #region 2
            if (existOperator(function.Substring(0, 1)))
            {
                if (function.Substring(0, 1).Equals("-"))
                    negative = true;
                else
                    negative = false;
                function = function.Substring(1, function.Length - 1);
            }
            if (function.IndexOf("x") == 0)
            {
                if (negative)
                    result[1] = -1;
                else
                    result[1] = 1;
                function = function.Replace("x", "");
            }
            else
            {
                auxi = int.Parse(Helper.getPreviousNumber(function, function.IndexOf("x")));
                if (negative)
                    result[1] = - auxi;
                else
                    result[1] = auxi;
                function = function.Replace(auxi.ToString() + "x", "");
            }
            #endregion
            #region 3
            if (function.Length > 0)
            {
                if (existOperator(function.Substring(0, 1)))
                {
                    if (function.Substring(0, 1).Equals("-"))
                        negative = true;
                    else
                        negative = false;
                    function = function.Substring(1, function.Length - 1);
                }
                if (negative)
                    result[2] = -int.Parse(function);
                else
                    result[2] = int.Parse(function);
            }
            else
                result[2] = 0;
            #endregion

            return result;
        }

        public static string replaceXWithTheFactor(string function, double factor)
        {
            if (function.IndexOf('x') == 0)
            {
                function = factor.ToString() + function.Substring(1, function.Length - 1);
            }
            string previousCharacter = "";
            while (function.IndexOf('x') != -1) {
                previousCharacter = function.Substring((function.IndexOf("x") - 1), 1);
                if (existOperator(previousCharacter))
                    function = function.Substring(0, function.IndexOf("x") + 1).Replace("x", factor.ToString()) + function.Substring(function.IndexOf("x") + 1, function.Length - (function.IndexOf("x") + 1));
                else
                    function = function.Substring(0, function.IndexOf("x") + 1).Replace("x", "*" + factor.ToString()) + function.Substring(function.IndexOf("x") + 1, function.Length - (function.IndexOf("x") + 1));
            }
            return function;
        }
        public static string replaceQuadraticWithMultiplication(string function)
        {
            while (function.IndexOf("²") != -1) {
                string n = getPreviousNumber(function, function.IndexOf("²")).ToString();
                function = function.Replace(n+"²", n+"*"+n);
            }
            return function;
        }
        public static string getPreviousNumber(string function, int operatorIndex)
        {
            string n = "";
            function = function.Substring(0, operatorIndex);
            for(int auxi = function.Length; auxi > 0; auxi--)
            {
                if (!existOperator(function.Substring(auxi - 1, 1)) || auxi == 1)
                {
                    n = function.Substring(auxi - 1, 1) + n;
                }
                else
                    return n;
            }
            return n;
        }
        public static string getNextNumber(string function, int operatorIndex)
        {
            string n = "";
            function = function.Substring(operatorIndex + 1, function.Length - (operatorIndex +1));
            for (int auxi = 0; auxi < function.Length; auxi++)
            {
                if (!existOperator(function.Substring(auxi, 1)))
                {
                    n = n + function.Substring(auxi, 1);
                }
                else
                    return n;
            }
            return n;
        }
        public static string clear(string function)
        {
            function = function.ToLower();
            function = function.Replace(" ", "");
            return function;
        }
        public static int typeOfEquation(string function)
        {
            function = clear(function);
            
            if (!function.Substring(function.Length - 1, 1).Equals("=") && function.IndexOf("²") != 0 && function.Contains("=")) {
                if (function.Contains("f("))
                {
                    //funcção linear (função)- tipo: 1
                    if (function.IndexOf("²") == -1)
                        return 1;
                    //função quadrática (função)- tipo: 1
                    else if (function.IndexOf("²") != -1)
                        return 1;
                }
                else
                {
                    //equação do segundo grau (bhaskara) - tipo: 3
                    if ((function.Substring(function.Length - 2, 2).Equals("=0")) && (occurrencesCharacter(function, 'x') == 2) && (function.IndexOf("²") != -1) && (Helper.occurrencesCharacter(function, '²') == 1))
                        return 3;
                    //equação do primeiro grau - tipo 2
                    else if (function.Contains("x"))
                        return 2;
                }
            }
            return 0;
        }
        public static int occurrencesCharacter(string word, char term)
        {
            int occurr = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (word.Substring(i, 1).Equals(term.ToString()))
                    occurr++; 
            }
            return occurr;
        }
        public static int getFactorFunction(string linerFunction)
        {
            string factor = "";
            string[] function = linerFunction.Split('=');
            linerFunction = function[0].Replace("f(", "");
            factor = linerFunction.Replace(")", "");
            
            return int.Parse(factor);
        }
        public static string putXOnOppositeSide(string equation)
        {
            string right = putEverythingToRight(equation);
            string left = "";
            while (right.IndexOf("x") != -1)
            {
                if (right.IndexOf("x") != 0)
                {
                    if (existOperator(right.Substring(right.IndexOf("x") - 1, 1)))
                    {
                        string op = reverseTheOperator(right.Substring(right.IndexOf("x") - 1, 1));
                        left += op + "x";
                        right = right.Remove(right.IndexOf("x") - 1, 2);
                    }
                    else
                    {
                        string n = getPreviousNumber(right, right.IndexOf("x"));
                        int indexOfN = right.IndexOf("x") - n.Length;
                        if (indexOfN == 0)
                        {
                            left += "-" + n + "x";
                            right = right.Substring(right.IndexOf("x") + 1);
                        }
                        else
                        {
                            string op = reverseTheOperator(right.Substring(indexOfN - 1, 1));
                            left += op + n + "x";
                            right = right.Remove(indexOfN - 1, n.Length + 2);
                        }
                    }
                }
                else
                {
                    left += "-x";
                    right = right.Substring(1);
                }
            }
            return left + "=" + right;
        }
        public static string putEverythingToRight(string equation)
        {
            string[] auxi = new string[2];
            string[] eq = equation.Split('=');
            string right = eq[1];
            string left = eq[0];
            while (left.Length > 0)
            {
                if (existOperator(left))
                {
                    auxi = getNextOperator(left);
                    if (auxi[0] != "0")
                    {
                        right += "-" + getPreviousNumber(equation, int.Parse(auxi[0]));
                        left = left.Substring(int.Parse(auxi[0]));
                    }
                    else
                    {
                        string n = getNextNumber(left, int.Parse(auxi[0])).ToString();
                        string op = reverseTheOperator(auxi[1]);
                        right += op + n;
                        left = left.Substring(left.IndexOf(n) + n.Length);
                    }
                }
                else {
                    right += "-" + left;
                    return right;
                }
            }
            return right;
        }

        public static string reverseTheOperator(string op)
        {
            switch (op)
            {
                case "+":
                    return "-";
                case "-":
                    return "+";
                case "*":
                    return "/";
                case "/":
                    return "*";
                default:
                    return op;
            }
        }
    }
}
