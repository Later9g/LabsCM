int N = 0;
while(N == 0)
{
    Console.WriteLine("Введите количество узлов интерполяции, в которых заданы значения функций");
    N = Convert.ToInt32(Console.ReadLine());
}

Console.WriteLine("Введите вектор значений аргументов в порядке возрастания (вектор узлов интерполяции)");
double[] X = new double[N];
for (int i = 0; i < N; i++)
{
    X[i] = Convert.ToDouble(Console.ReadLine());
}

Console.WriteLine("Введите вектор значений функции в узлах интерполяции");
double[] Y = new double[N];
for (int i = 0; i < N; i++)
{
    Y[i] = Convert.ToDouble(Console.ReadLine());
}

Console.WriteLine("Введите значение аргумента, при котором будет вычисляться интерполяционное значение функции");
double XX = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("Введите степень «барицентрического» многочлена Лагранжа, с помощью которого будет вычисляться значение функции в точке XX");
int m = Convert.ToInt32(Console.ReadLine());


if(N < m + 1)
{
    System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "result_1.txt", "IER = 1 интерполяционный многочлен степени m не может быть построен ( N < m + 1)");
}
else if (!Common.IsAscending(X))
{
    System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "result_1.txt", "IER = 2 нарушен порядок возрастания аргумента в входном векторе X\n");
}
else
{
    var result = LagrangeInterpolation(X, Y, N, XX, m);
    Console.WriteLine("RESULT: ");
    Console.WriteLine(result);
    System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "result_1.txt",
        "YY = " + Convert.ToString(result) + "\nIER = 0"
        );
}


static double LagrangeInterpolation(double[] X, double[] Y, int N, double XX, int m)
{
    double sum = 0.0;
    for (int i = 0; i <= m; i++)
    {
        double product = Y[i];
        for (int j = 0; j <= m; j++)
        {
            if (j != i)
            {
                product *= (XX - X[j]) / (X[i] - X[j]);
            }
        }
        sum += product;
    }

    return sum;
}

