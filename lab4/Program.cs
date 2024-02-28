using System;
using System.IO;

public class CodeTranslation
{
    static double PI = Math.PI;
    static double EPS = 5.0306980803327406e-16;
    static double a = 0;
    static double b = 1;
    static int n = 31;
    static double h = (b - a) / (n - 1);
    static double h_min = 1e-10;

    static double TrapezoidRule(double[] x, Func<double, double> f, int n, double h)
    {
        double res = 0;
        for (int i = 1; i < n; i++)
        {
            double f_imin1 = 0; 
            double f_i = 0;
            if (i == 1)
            {
                f_imin1 = f(x[i - 1]);
                f_i = f(x[i]);
            }
            else
            {
                f_imin1 = f_i;
                f_i = f(x[i]);
            }
            res += (f_i + f_imin1) * h / 2;
        }
        return res;
    }

    static int ErrorEstimationRunge(double[] x, Func<double, double> f, int n, double h)
    {
        int IER = 0;
        double runge_error = 1;
        int it = 0;
        double integral1 = 0;
        double integral2 = 0;
        double h_new = h;
        while (!(runge_error < EPS))
        {
            double runge_error_prev = runge_error;
            if (runge_error == 1)
            {
                integral1 = TrapezoidRule(x, f, n, h);
                h_new = h / 2;
                int n_new = (int)Math.Round((b - a) / h_new) + 1;
                double[] x_new = new double[n_new];
                for (int i = 0; i < n_new; i++)
                {
                    x_new[i] = a + i * h_new;
                }
                integral2 = TrapezoidRule(x_new, f, n_new, h_new);
            }
            else
            {
                integral1 = integral2;
                h_new /= 2;
                int n_new = (int)Math.Round((b - a) / h_new) + 1;
                double[] x_new = new double[n_new];
                for (int i = 0; i < n_new; i++)
                {
                    x_new[i] = a + i * h_new;
                }
                integral2 = TrapezoidRule(x_new, f, n_new, h_new);
            }
            if (h_new < h_min)
            {
                IER = 2;
                break;
            }
            runge_error = Math.Abs(integral1 - integral2) / 0.5;
            if (runge_error_prev < runge_error)
            {
                IER = 1;
                break;
            }
            it++;
        }
        if (IER == 0)
        {
            int coef = (int)(h / (h_new * 2));
            Console.WriteLine($"|I_(h/c)| = I_(h/{coef}) = {integral1}");
            Console.WriteLine($"|I_(h/(2*c))| = I_(h/{2 * coef}) = {integral2}");
            Console.WriteLine($"eps = {runge_error}");
            Console.WriteLine($"Шаг H на котором было получено решение: {it}");
            using (StreamWriter writer = new StreamWriter("result.txt"))
            {
                writer.WriteLine($"|I_(h/c)| = I_(h/{coef}) = {integral1}");
                writer.WriteLine($"|I_(h/(2*c))| = I_(h/{2 * coef}) = {integral2}");
                writer.WriteLine($"eps = {runge_error}");
                writer.WriteLine($"Шаг H на котором было получено решение: {it}");
            }
        }
        return IER;
    }

    static void Main()
    {
        int IER = 0;

        if (b <= a)
        {
            IER = 3;
        }

        double[] x = new double[n];
        for (int i = 0; i < n; i++)
        {
            x[i] = a + i * h;
        }

        if (h < h_min && IER == 0)
        {
            IER = 2;
        }

        Func<double, double> FOO1 = (double x) => Math.Sin(x);
        Func<double, double> FOO2 = (double x) => Math.Cos(x);
        Func<double, double> FOO3 = (double x) => 1 / (1 + 25 * x * x);
        Func<double, double> FOO4 = (double x) => x;

        Func<double, double> f = FOO4;
        double integral = TrapezoidRule(x, f, n, h);
        Console.WriteLine("Численный интеграл: " + integral);

        // Plotting code using appropriate C# libraries

        IER = ErrorEstimationRunge(x, f, n, h);

        switch (IER)
        {
            case 0:
                Console.WriteLine("IER = 0. Программа отработала корректно");
                break;
            case 1:
                Console.WriteLine("IER = 1. Решение не получено, погрешность перестала уменьшаться");
                break;
            case 2:
                Console.WriteLine("IER = 2. Шаг интерполирования стал недопустимо малым");
                break;
            case 3:
                Console.WriteLine("IER = 3. Ошибка входных данных b <= a");
                break;
        }
    }
}





//class Program
//{
//    static void Main(string[] args)
//    {
//        //локальная функция
//        double f(double x) => x / (x - 1);

//        var result = RightRectangle(f, 4, 10, 1000);
//        Console.WriteLine("Формула правых прямоугольников: {0}", result);

//    }
//    static double RightRectangle(Func<double, double> f, double a, double b, int n)
//    {
//        var h = (b - a) / n;
//        var sum = 0d;
//        for (var i = 1; i <= n; i++)
//        {
//            var x = a + i * h;
//            sum += f(x);
//        }

//        var result = h * sum;
//        return result;
//    }
//}
