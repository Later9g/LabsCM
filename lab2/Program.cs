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

var q = CubicSpline.InterpolateBoundariesSorted(X, Y, SplineBoundaryCondition.SecondDerivative, A, SplineBoundaryCondition.SecondDerivative, B);


Console.WriteLine(q.Interpolate(XX));
