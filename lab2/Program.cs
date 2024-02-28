using MathNet.Numerics.Interpolation;


Console.WriteLine("Введите количество отрезков, на которых строится кубический сплайн");
int N = Convert.ToInt32(Console.ReadLine()) + 1 ;

Console.WriteLine("Введите вектор значений аргументов в порядке возрастания ");
double[] X = new double[N];
for (int i = 0; i < N; i++)
{
    X[i] = Convert.ToDouble(Console.ReadLine());
}

Console.WriteLine("Введите вектор значений функции f(x) в узлах интерполяции");
double[] Y = new double[N];
for (int i = 0; i < N; i++)
{
    Y[i] = Convert.ToDouble(Console.ReadLine());
}


Console.WriteLine("Введите значение аргумента, при котором будет вычисляться интерполяционное значение функции");
double XX = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("Введите левой константы краевых условий. ");
double A = Convert.ToDouble(Console.ReadLine());
Console.WriteLine("Введите правой константы краевых условий. ");
double B = Convert.ToDouble(Console.ReadLine());

if (N < 3)
    Console.WriteLine("IER  1 – кубический сплайн не может быть построен (N < 2)");
else if (!Common.IsAscending(X))
    Console.WriteLine("IER  2 – нарушен порядок возрастания аргумента в входном векторе X");
else if (XX < X[0] || XX > X[X.Length-1])
    Console.WriteLine("IER = 3 – точка XX не принадлежит отрезку [x0,xn]");
else
{

    var q = CubicSpline.InterpolateBoundariesSorted(X, Y, SplineBoundaryCondition.SecondDerivative, A, SplineBoundaryCondition.SecondDerivative, B);
    Console.WriteLine(q.Interpolate(XX));
}
