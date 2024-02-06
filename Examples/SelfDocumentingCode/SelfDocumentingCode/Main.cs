/// <summary>
/// <list type="bullet">
///   <item>Author:     Jim de St. Germain</item>
///   <item>Date:       Spring 2022</item>
/// </list>
/// 
/// <para>
///   This project/solution code shows:
///   1) how hard it is to read bad code
///   2) how to comment
///   3) how to "self-document"
/// </para>
/// <para>
///   Show something falling.
/// </para>
/// </summary>
/// 
using Unclear;

Clarity clarity_rating = Clarity.commented;

Console.WriteLine( "Showing Velocities" ); 

if ( clarity_rating == Clarity.unclear )
{
    double dt = 1.0;
    double total = 100;
    double coeff =.7; // 1.0: belly down, .7 : head down human,  .1 : smooth sphere (see: https://en.wikipedia.org/wiki/Drag_coefficient)
    double step = dt/total;

    double a = 0;
    double v = 0;
    double t = 0;

    List<double> fallings = new List<double>();

    while ( t < total )
    {
        Console.WriteLine( $"{t:0.00} {v:0.00000}" );

        a = 9.8 - coeff * v * v * step;

        v += a * step;

        t += step;
    }
}
else if ( clarity_rating == Clarity.commented ) 
{
    // compute fall velocity in the Earth's atmosphere
    double time =  0.0;             // current time step
    double vel  = -1.0;             // current velocity
    double dt   =  1.0 / 100.0;     // delta time - i.e., the time step

    // call a method to compute the velocity over the first 100 seconds of falling, using the given time step
    List<double> test = Falling.falling(100,dt);

    // print the velocities
    foreach ( var val in test )
    {
        time += dt;
        // "short circuit" if terminal velociy achieved
        if ( Math.Abs( vel - val ) < .00000001 )
        {
            Console.WriteLine( "Terminal Velocity seems to be reached" );
            break;
        }
        vel = val;
        Console.WriteLine( $"{time:0.00} {val:0.00000}" );
    }
}
else if ( clarity_rating == Clarity.self_documented )
{
    double elapsed_time            =   0.0;  // all times are in seconds
    double previous_velocity       =  -1.0;
    double delta_t                 =   1.0 / 100.0;
    const double equality_constant =   0.00000001;

    IEnumerator<double> velocity = 
        SelfDocumented.Falling.compute_fall_velocities_over_time( 
            delta_t: delta_t,
            drag_coef: SelfDocumented.Falling.human_head_down).GetEnumerator();

    double change_in_velocity;
    velocity.MoveNext(); // Reminder: enumerators start "before" the data

    do
    {
        double current_velocity = velocity.Current;
        elapsed_time           += delta_t;
        change_in_velocity      = Math.Abs( previous_velocity - current_velocity );

        velocity.MoveNext(); 
        previous_velocity = current_velocity;

        Console.WriteLine( $"{elapsed_time:0.00} {current_velocity:0.00000}" );

    } while ( change_in_velocity > equality_constant );

    Console.WriteLine( "Terminal Velocity seems to be reached" );

}

// code moved to self-documenting (and commented) code


enum Clarity { unclear, commented, self_documented }

