/// <summary>
/// <list type="bullet">
///   <item>Author:     Jim de St. Germain</item>
///   <item>Date:       Spring 2022</item>
/// </list>
/// </summary>
namespace Unclear
{
    public static class Falling
    {
        public static List<double> falling( double length = 100, double dt = .1 )
        {

            double coeff=.7; // 1.0: belly down, 1.2 : upright human,  .1 : smooth sphere (see: https://en.wikipedia.org/wiki/Drag_coefficient)

            double step = dt;

            double a = 0;
            double v = 0;
            double t = 0;

            List<double> fallings = new List<double>();

            while ( t < length )
            {
                fallings.Add( v );
                a = 9.8 - coeff * v * v * step;

                v += a * step;

                t += step;
            }

            return fallings;
        }
    }
}