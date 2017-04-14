using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlerLogical2.Files
{
    static class Function
    {
        public static double function(string function, int value)
        {
            function = function.Replace("f(" + value.ToString() + ")=", "");
            function = Helper.replaceQuadraticWithMultiplication(function);
            function = Helper.replaceXWithTheFactor(function, double.Parse(value.ToString()));
            string[] auxi;
            string operation = "";
            int index = 0;
            int startIndex = function.Length;
            double ret = 0.0, result = 0.0, n = 0;
            char op = '0';
            
            while (function.Length > 0)
            {
                if (Helper.existOperator(function.Substring(1, function.Length - 1)))
                {
                    if (Helper.existOperator(function.Substring(0, 1)))
                    {
                        auxi = Helper.getNextOperator(function.Substring(1));
                        auxi[0] = (int.Parse(auxi[0]) + 1).ToString();
                    }
                    else
                        auxi = Helper.getNextOperator(function);

                    op = char.Parse(auxi[1]);
                    index = int.Parse(auxi[0]);
                    operation = Helper.getPreviousNumber(function, index) + op.ToString() + Helper.getNextNumber(function, index);
                    result = Helper.calc(double.Parse(Helper.getPreviousNumber(function, index)), op, double.Parse(Helper.getNextNumber(function, index)));
                    function = function.Replace(operation, result.ToString());
                }
                else {
                    ret = double.Parse(function);
                    return Math.Round(ret, 2);
                }
            }
            return Math.Round(ret, 2);
        }

        public static string bhaskara(string function)
        {
            function = function.Replace("=0", "");
            int[] values = Helper.getABCOfEquation(function);
            double delta = values[1] * values[1] - (4 * values[0] * values[2]);
            if (delta < 0)
                return "Não possui raízes reais. Delta = " + delta.ToString();
            double x1 = (-values[1] + Math.Sqrt(delta)) / (2 * values[0]);
            double x2 = (-values[1] - Math.Sqrt(delta)) / (2 * values[0]);
            return "x' = " + Math.Round(x1, 2).ToString() + " | x'' = " + Math.Round(x2, 2).ToString();
        }
        public static double linearEquation(string equation)
        {
            double result = 0.0;
            equation = Helper.putXOnOppositeSide(equation);
            string[] side = equation.Split('=');
            double right = function(side[1], 1);
            double left = function(Helper.replaceXWithTheFactor(side[0], 1.0), 1);
            result = right / left;
            return Math.Round(result, 2); 
        }
    }
}
