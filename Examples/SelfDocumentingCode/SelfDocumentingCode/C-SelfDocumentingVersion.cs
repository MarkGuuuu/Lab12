namespace SelfDocumented
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
        public const double human_belly_down = 1.0;
        public const double human_head_down  = 0.7;  // see https://commented.com/everyday-science/terminal-velocity-of-a-human-free-fall-and-drag-force-159eb5366374
        public const double smooth_sphere    = 0.1;

        /// <summary>
        ///  <para>
        ///    Compute the velocities of a falling object, starting at rest, 
        ///    in the Earth's atmosphere over time.
        ///  </para>
        ///  
        ///  <para>
        ///    The expected rate of acceleration is 9.8 m/s (based on earth gravity)
        ///    without atmosphere.
        ///  </para>
        ///  
        ///  <para>
        ///    The coefficient of drag represents (approximately) the air resistance of a human belly down. 
        ///    See constants at top of file. 
        ///    
        ///    Sources: https://en.wikipedia.org/wiki/Drag_coefficient
        ///             https://commented.com/everyday-science/terminal-velocity-of-a-human-free-fall-and-drag-force-159eb5366374
        ///  </para>
        /// </summary>
        /// 
        /// <remarks>
        ///   It should be noted that the smaller the delta_t, the better the
        ///   computation will mirror reality, but at the expense of more computations
        /// </remarks>
        /// 
        /// <param name="delta_t"> e.g., the "change in time" over which the simulation occurs in seconds </param>
        /// <param name="drag_coef"> what "shape" we are simulating
        /// 
        /// <returns>
        ///   The "next" velocity in time based on a simulated falling object where [0] 
        ///   is the initial velocity.
        /// </returns>
        public static IEnumerable<double> compute_fall_velocities_over_time( double delta_t = 1.0, double drag_coef = smooth_sphere)
        {
            List<double> velocities_over_time = new List<double>();

            double velocity     = 0;

            while (true)
            {
                yield return velocity;

                double acceleration = 9.8 - drag_coef * velocity * velocity * delta_t;
                velocity           += acceleration                          * delta_t;
            }
        }
    }

}