using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandlerLogical2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LinearFunction());
        }

        public static string start(string function)
        {
            function = Files.Helper.clear(function);
            if (string.IsNullOrEmpty(function))
                return "Informe alguma expressão";

            switch (function)
            {
                case "f(x)=x":
                    return "Preencha o valor de 'x' em f(x)";
                case "f(x)=x²":
                    return "Preencha o valor de 'x' em f(x)";
                case "ax²+b-c=0":
                    return "Preencha os valores de 'a', 'b' e 'c'";
            }
            try {
                switch (Files.Helper.typeOfEquation(function))
                {
                    case 0:
                        return "Formato inválido";
                    case 1:
                        return "Resultado: " + Files.Function.function(function, Files.Helper.getFactorFunction(function)).ToString();
                    case 2:
                        return "x = " + Files.Function.linearEquation(function).ToString();
                    case 3:
                        return Files.Function.bhaskara(function).ToString();
                }
                return "Formato inválido";
            }
            catch (Exception e)
            {
                return "Formato inválido";
            }
        }
    }
}
