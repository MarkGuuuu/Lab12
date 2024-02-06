namespace Commented
{
    /// <summary>
    /// <list type="bullet">
    ///   <item>Author:     Jim de St. Germain</item>
    ///   <item>Date:       Spring 2022</item>
    /// </list>
    /// 
    /// <para>
    ///   The following class determines the falling velocity
    ///   over time based.
    /// </para>
    /// </summary>
    public static class Falling
    {
        /// <summary>
        ///  Compute the velocity of a falling object over time.
        ///  <para>
        ///    The expected rate of fall is 9.8 m/s (based on earth gravity).
        ///  </para>
        ///  <para>
        ///    The coefficient of drag represents (approximately) the air resistance of a human belly down. 
        ///    1.0: belly down, 1.2 : upright human,  .1 : smooth sphere  // TODO: notice comment rot here compared to SelfDocumenting code
        ///    (see: https://en.wikipedia.org/wiki/Drag_coefficient)
        ///  </para>
        /// </summary>
        /// <param name="l"> the total time </param>
        /// <param name="s"> the time slice (delta t) </param>
        /// <returns>
        ///   An array of velocities where [0] is the initial velocity and
        ///   [l/c] is the final velocity.
        /// </returns>
        public static List<double> falling( double l = 100, double s = .1 )
        {

            double coeff=.7; 

            // acceleration, velocity, current time
            double a = 0;
            double v = 0;
            double t = 0;

            List<double> fallings = new List<double>();

            // Equation found here: https://physics.stackexchange.com/questions/104368/free-falling-object-with-air-resistance
            while ( t < l ) // compute until "out of time"
            {
                fallings.Add( v );
                a = 9.8 - coeff * v * v * s;

                v += a * s;

                t += s;
            }

            return fallings;
        }
    }

}