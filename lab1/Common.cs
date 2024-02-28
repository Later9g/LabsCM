
static public class Common
{
    public static bool  IsAscending(double[] data)
    {
        bool result = true;
        double prev = data[0];
        for (int i = 0;i < data.Length;i++)
        {
            if (data[i] <= prev)
            {
                result = false; break;
            }
            prev = data[i];
        }

        return result;
    }

    public static double FindSegment(double[] data,double x)
    {
        double result = data[0];
        int lenght = data.Length;
        for(int i = 0;i<lenght-1;i++)
        {
            if(x >= data[i] && x <= data[i+1])
            {
                result = data[i];
            }
        }

        return result;
    }
}
