using System;
using System.Threading;
using System.Diagnostics;

/// <summary>
///  Demonstration code for two threads accessing shared memory
/// </summary>
public class ThreadPractice
{
    /// <summary>
    /// An object used as the door/lock to keep
    /// multiple threads from accessing the same _shared resource_ in
    /// the same _critical region_ at the same _time_.
    /// 
    /// Note: there is nothing special about using an "object" object here. We
    ///       could use a Student object (if we had one) or the ThreadPractice object
    ///       (which we do have, i.e., "this").
    ///       
    ///       Using a specially named locking object makes it clearer what is going on.
    /// </summary>
    private object door = new object();

    /// <summary>
    ///   Shared memory utilized by two threads
    /// </summary>
    private int count = 0;

    /// <summary>
    ///   Create two threads and execute them simultaneously
    /// </summary>
    public void demo()
    {
        // Create two thread objects
        Thread thread1 = new Thread(modify);
        Thread thread2 = new Thread(modify);
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
            Console.WriteLine( "count = " + count );
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
        Console.WriteLine("Final value of count = " + count);
        Console.Read();
    }

    private readonly int iterations = 1_000_000_000;

    /// <summary>
    ///     Runs a long loop that increments and then decrements the count
    ///     over and over. At each "completion" of the loop body, the value of
    ///     count should not be changed.
    /// </summary>
    public void modify()
    {
        for ( int i = 0; i < iterations; i++ )
        {
            count++;

            count--;
        }
    }

    /// <summary>
    /// If you want to play more (and you should) try running the threads
    /// on add and sub.  Try locking :
    /// 
    /// (a) inside the loop
    /// (b) outside the loop
    /// 
    /// </summary>
    public void add()
    {
        for ( int i = 0; i < iterations; i++ )
        {
            count++;
        }
    }

    public void sub()
    {
        for ( int i = 0; i < iterations; i++ )
        {
            count--;
        }
    }



}