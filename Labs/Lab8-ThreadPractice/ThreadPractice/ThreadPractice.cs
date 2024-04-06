using System.Diagnostics;

namespace CS3500Lab8;

/// <summary>
///   <para>
///     Author: CS 3500 Course Staff
///   </para>
///   <para>
///     Demonstration code for two threads accessing shared memory
///   </para>
/// </summary>
public class ThreadPractice
{
    /// <summary>
    ///   <para>
    ///     An object used as the door/lock to keep
    ///     multiple threads from accessing the same _shared resource_ in
    ///     the same _critical region_ at the same _time_.
    ///   </para>
    /// 
    ///   <remark>
    ///     <para>
    ///         Note: there is nothing special about using an "object" object here. We
    ///               could use a Student object (if we had one) or the ThreadPractice object
    ///               (which we do have, i.e., "this").
    ///      </para>
    ///      <para>
    ///          Using a specially named locking object makes it clearer what is going on.
    ///      </para>
    ///      <para>
    ///          Further, making the object READONLY eliminates the possible error of locking
    ///          on the door, then assigning Door a new object, and then locking on it again.  That 
    ///          would NOT provide thread safety
    ///      </para>
    ///   </remark>
    /// </summary>
    private readonly object Door = new object();

    /// <summary>
    ///   Shared memory utilized by two threads
    /// </summary>
    private int Count = 0;

    /// <summary>
    ///   Create two threads and execute them simultaneously
    /// </summary>
    public void Demo()
    {
        // Create two thread objects
        Thread thread1 = new Thread(DoProgramComputationalWork);
        Thread thread2 = new Thread(DoProgramComputationalWork);
        //Thread thread1 = new Thread(sub);
        //Thread thread2 = new Thread(add);

        // begin the work
        thread1.Start();
        thread2.Start();

        // Question: how many threads are running at this point?

        // time the work
        Stopwatch watch = new Stopwatch();
        watch.Start();

        // As long as one of the threads is running, give
        // updates on the value of count
        while ( thread1.IsAlive || thread2.IsAlive )
        {
            Console.WriteLine( "count = " + Count );
            Thread.Sleep( 1000 );  // Don't spew too much to the console

            //
            // Continually "looking" and "sleeping" is called "polling".
            //
            // Polling means continuously "ask" to see if something has been done and then
            // take action.  It is usually an inefficient way to check for changes.  Modern
            // systems use events to "tell" the main thread when changes have taken place rather
            // than have the main thread continuously "ask".
            //
        }

        //
        // TODO: write the join syntax below to tell the main thread to wait
        //       for each worker to finish.  Then comment out the while loop above.
        //

        watch.Stop();

        Console.WriteLine("Took: " + watch.ElapsedMilliseconds + " milliseconds");

        // Display the final value of count.  If the threads are well-behaved,
        // it will be zero.
        Console.WriteLine("Final value of count = " + Count);
        Console.Read();
    }

    /// <summary>
    ///   <para>
    ///     Defines how much "work" to do in our methods.
    ///   </para>
    ///   <para>
    ///     Set this value to something that takes 5-10 seconds on your computer, or
    ///     longer if you want to have more time to debug/view the task manager.
    ///   </para>
    /// </summary>
    private readonly int WorkIterations = 1_000_000_000;

    /// <summary>
    ///   <para>
    ///     Runs a long loop that increments and then decrements the count
    ///     over and over. At each "completion" of the loop body, the value of
    ///     count should not be changed.
    ///   </para>
    ///   <para>
    ///     This simulates doing a long computation such as computing the next Bitcoin value
    ///   </para>
    /// </summary>
    public void DoProgramComputationalWork()
    {
        for ( int i = 0; i < WorkIterations; i++ )
        {
            Count++;

            Count--;
        }
    }

    /// <summary>
    ///   <para>
    ///     This function is the counterpart to the DecrementCounter method and does the opposite,
    ///     adding one to count, WorkIteration times.
    ///   </para>
    ///   <para>
    ///     If you want to play more (and you should) try running the threads
    ///     on add and sub.  Try locking :
    ///   </para>
    ///   
    ///   <list type="number">
    ///     <item>
    ///       inside the loop
    ///     </item>
    ///     <item>
    ///       outside the loop
    ///     </item>
    ///   </list>
    /// 
    /// </summary>
    public void IncrementCounter()
    {
        for ( int i = 0; i < WorkIterations; i++ )
        {
            Count++;
        }
    }

    /// <summary>
    ///   <para>
    ///     This function is the counterpart to the IncrementCounter method and does the opposite,
    ///     subtracting one from count, WorkIteration times.
    ///   </para>
    /// </summary>
    public void DecrementCounter()
    {
        for ( int i = 0; i < WorkIterations; i++ )
        {
            Count--;
        }
    }

}