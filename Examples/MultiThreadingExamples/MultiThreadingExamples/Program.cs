using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
///  Author: H. James de St. Germain
///  Date:   2016
///  The following code demonstrates threaded examples.
///  
///   Example one shows how to start a thread to do some work.
/// 
/// </summary>

namespace L14_Threading_Examples
{
    class Basic_Mutual_Exclusion
    {
        static int EXAMPLE_TO_SHOW = 1;

        static void Main( string[] args )
        {
            if ( EXAMPLE_TO_SHOW == 1 )
            {
                example_1_start_work_and_tell_user();
            }
            else if ( EXAMPLE_TO_SHOW == 2 )
            {
                // Base Case of work on single thread
                example_2_shared_data_non_threaded( 1_000_000_000 );

                // Uncomment to see improvement of 'startup time' when running a second time
                //   example_2_shared_data_non_threaded( 1_000_000_000 );

                // Uncomment to see example of counting up and down at same time on two threads
                //   example_2_shared_data_threaded(       1_000_000_000 );


                // Uncomment to see that we can do parallel with NON-Shared variables
                //   example_2_unique_data_threaded( 1_000_000_000 );
                //   example_2_unique_data_threaded( 1_000_000_000 );

            }
            else if ( EXAMPLE_TO_SHOW == 3 )
            {
                example_3_non_threaded( 5_000_000 ); 
                example_3_split_the_work( 2, 5_000_000 );
                example_3_split_the_work( 4, 5_000_000 );
                example_3_split_the_work( 8, 5_000_000 );
            }
            Console.Read();
        }

        // ----------------------- EXAMPLE 1 CODE ----------------------------

        /// <summary>
        /// message_already_displayed is a flag that is used to 
        /// instruct the various threads to only print a message to the user once.
        /// 
        /// </summary>
        static bool message_already_displayed = false;

        /// <summary>
        ///  This function simulates starting to "big jobs" and telling
        ///  the user that they have been started.
        ///  
        ///  Note: we only want to tell the user the message once.
        /// 
        ///  Observation (1) - (see bottom of file for discussion) 
        ///      Question: How many threads are running after the "Start"?
        ///      
        /// </summary>
        private static void example_1_start_work_and_tell_user()
        {
            message_already_displayed = false;
            new Thread( example_1_do_the_work ).Start();
            example_1_do_the_work();
        }

        /// <summary>
        /// simple example of work that relies on shared data (in this case the flag) 
        /// 
        /// Observation (2)
        ///    Question: what will happen when we move the message_already_displayed
        ///    assignment to before the WriteLine?
        /// </summary>
        private static void example_1_do_the_work()
        {
            // let the user know that work has begun (only once)
            if ( !message_already_displayed )
            {
                Console.WriteLine( "Work has Commenced!" );
                message_already_displayed = true;
            }

            // do lots of work here.
        }

        // ----------------------- EXAMPLE 2 CODE ----------------------------



        /// <summary>
        /// count up and then count down  
        /// see Observation (4)
        /// </summary>
        /// <param name="how_much"> how many numbers to count up and then down to</param>
        static void example_2_shared_data_non_threaded( long how_much )
        {
            long shared_counter = 0;
            long total_counts = how_much;

            Console.WriteLine( $"Example 2 (non-threaded) - counting up to {how_much},  times." );

            Stopwatch timer = new Stopwatch();

            timer.Start();

            CountUp( ref shared_counter, total_counts );

            Console.WriteLine( $"  Counting Up Done. Counter = {shared_counter}" );

            PrintElapsedTime( timer );

            CountDown( ref shared_counter, total_counts );

            timer.Stop();

            Console.WriteLine( $"  Counting Down Done. Counter = {shared_counter}" );

            PrintElapsedTime( timer );
        }

        /// <summary>
        ///   In this example, the work of adding and subtracting is divided
        ///   evenly between two threads. One thread adds all the numbers, the other
        ///   thread subtracts them.
        /// 
        ///   Observation (3) - What will the value of shared_counter be at the end of this
        ///                     execution?
        ///                     
        ///   Observation (5) - How much faster will this code run (on two threads vs one)
        /// </summary>
        /// <param name="how_much"> Total number of times to add and subtract.</param>

        static void example_2_shared_data_threaded( long how_much )
        {
            long shared_counter = 0;
            Console.WriteLine( $"Example 2 (threaded) - counting up and down from {how_much} times." );

            // Define the work to be done by a thread            
            ThreadStart work_delegate_function_1 
                = new ThreadStart(() => CountUp(ref shared_counter, how_much ));

            // Build the threads (note: worker2 constructor is a shortcut for the same thing as worker1)
            Thread worker1 = new Thread( work_delegate_function_1 );
            Thread worker2 = new Thread( () => CountDown( ref shared_counter, how_much ) );

            // time the work!
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // let the work begin!
            worker1.Start();
            worker2.Start();

            // await the completion of the work!
            worker1.Join();
            worker2.Join();

            // How long did it take?
            timer.Stop();

            Console.WriteLine( "Example 2 done, counter = " + shared_counter );
            PrintElapsedTime( timer );

        }

        /// <summary>
        ///   Uncomment these and comment out local variables in next function
        ///   to see speed up
        /// </summary>
        //static long individual_counter_1 = 0;
        //static long individual_counter_2 = 0;

        static void example_2_unique_data_threaded( long how_much )
        {
            long individual_counter_1 = 0;
            long individual_counter_2 = 0;

            Console.WriteLine( $"Example 2 (two threads, one counting up, one counting down, two variables): {how_much} times." );

            // Build the threads (note: worker2 constructor is a shortcut for the same thing as worker1)
            Thread worker1 = new Thread( () => CountUp( ref individual_counter_1, how_much ) );
            Thread worker2 = new Thread( () => CountDown( ref individual_counter_2, how_much ) );

            // time the work!
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // let the work begin!
            worker1.Start();
            worker2.Start();

            // await the completion of the work!
            worker1.Join();
            worker2.Join();

            // check result
            Console.WriteLine( $"Combine two results: {individual_counter_1 + individual_counter_2} should be 0" );

            // How long did it take?
            timer.Stop();

            Console.WriteLine( "Example 2 done, counter 1 = " + individual_counter_1 );
            Console.WriteLine( "Example 2 done, counter 2 = " + individual_counter_2 );
            PrintElapsedTime( timer );

        }

        /// <summary>
        ///   count upward for X counts
        /// </summary>
        /// <param name="counter"> increases by number_of_counts </param>
        /// <param name="number_of_counts"> how much "work" to do</param>
        /// <returns></returns>
        private static void CountUp( ref long counter, long number_of_counts )
        {
            for ( long i = 0; i < number_of_counts; i++ )
            {
                counter++;
            }

            Console.WriteLine("counting up done");
        }

        /// <summary>
        ///   count downward
        /// </summary>
        /// <param name="counter"> increases by number_of_counts </param>
        /// <param name="number_of_counts"> how much "work" to do</param>
        /// <remarks>
        ///   Why use long instead of int?  (Observation (6))
        /// </remarks>
        private static void CountDown( ref long counter, long number_of_counts )
        {
            for ( long i = 0; i < number_of_counts; i++ )
            {
                counter--;
            }

            Console.WriteLine("counting down done");
        }

        // ----------------------- EXAMPLE 3 CODE ----------------------------
        // - Finding Primes
        //


        /// <summary>
        ///   Find/Count all prime numbers from 2 to the given input value
        /// </summary>
        /// <param name="max_value"> total to check for </param>
        private static void example_3_non_threaded( long max_value )
        {
            Console.WriteLine( $"Example 3 Primes (Non-Threaded) - determining primes between 2 and {max_value}" );
            Stopwatch timer = new Stopwatch();

            timer.Start();

            var values = FindPrimesBetween(2, max_value);

            timer.Stop();

            Console.WriteLine( $"  Found {values.Count} primes from : {values[0]} to {values[values.Count - 1]}" );

            PrintElapsedTime( timer );

            Console.WriteLine( "-------------------------------------------------------" );
        }

        /// <summary>
        ///   compute primes using multiple threads
        /// </summary>
        /// <param name="threadCount"> Number of threads</param>
        /// <param name="max_value"> Make sure this is evenly divisible by threads</param>
        private static void example_3_split_the_work( int threadCount, long max_value )
        {
            Console.WriteLine( $"Example 3 Primes (Threaded {threadCount}) - determining primes between 2 and {max_value}" );
            Stopwatch timer = new Stopwatch();
            List<long> counts = new List<long>();
            long count = 0;

            timer.Start();

            long start = 2;
            long work_division = (long) (max_value / threadCount);
            //Task<List<long>>[] workers = new Task<List<long>>[threads];
            List<Thread> workers = new List<Thread>();

            for ( long i = 0; i < threadCount; i++ )
            {
                long my_start = start;

                // (Observation 7) why is this division of work wrong?
                Thread thread = new Thread( 
                    () => { counts.Add(FindPrimesBetween( my_start, my_start + work_division ).Count); } );

                //workers[i] = new Task<List<long>>( () => find_primes( my_start, my_start + work_division ) );
                //Thread thread = new Thread( () => { count += find_primes( my_start, my_start + work_division ).Count; } );  // ? why does this not work?

                workers.Add( thread );
                start += work_division;
                thread.Start();
            }

            Console.WriteLine( $"count is {count}" );

            // for making sure all work is completed before reporting results
            for ( int i = 0; i < threadCount; i++ )
            {
                workers[i].Join();
                count += counts[i];
            }
                
            // If we use "Tasks" then there is a shortcut for the above loop
            //    Task.WaitAll( workers );

            timer.Stop();

            //
            // The following loop grabs the result of each task (the computed # of primes) and adds them
            //
            //for ( int i = 0; i < threads; i++ )
            //{
                //count += workers[i].ThreadState  .Result.Count;
            //}

            Console.WriteLine( $"  All threads done. Found {count} primes" );

            PrintElapsedTime( timer );

            Console.WriteLine( "-------------------------------------------------------" );
        }

        /// <summary>
        ///   Find some prime numbers by looping from start to end.
        ///   Note: we really should not check even numbers
        /// </summary>
        /// <param name="start"> start number to check (should be > 1) </param>
        /// <param name="end"> end number to check (should be greater than start)</param>
        /// <returns> a list of prime numbers between and including start and max</returns>
        private static List<long> FindPrimesBetween( long start, long end )
        {
            List<long> results = new List<long>();
            const int print_count = 200_000;

            for ( long i = start; i <= end; i++ )
            {
                // progress report
                if ( i % print_count == 0 )
                {
                    Console.WriteLine( $"  (Thread: {Thread.CurrentThread.ManagedThreadId,2}) Looked at {print_count} values between {i-print_count,7} to {i,7}. Found {results.Count,5} so far." );
                }

                if ( IsPrime( i ) )
                {
                    results.Add( i );
                }
            }

            //Console.WriteLine( $"  Looked for primes between {start} to {end}. Found {results.Count}." );

            return results;
        }


        /// <summary>
        ///   Determines is a positive integer greater than 1 is prime
        /// </summary>
        /// <remarks>
        ///   Really shouldn't check even divisibility over and over again....
        /// </remarks>
        /// <param name="value"> the number to check</param>
        /// <returns> true if prime </returns>
        private static bool IsPrime( long value )
        {
            for ( long i = 2; i < Math.Sqrt(value); i++ )
            {
                if ( value % i == 0 )
                {
                    return false;
                }
            }

            return true;
        }


        // ----------------------- HELPER FUNCTIONS ----------------------------

        /// <summary>
        ///  Print the elapsed time from a stopwatch object
        /// </summary>
        /// <param name="timer"> Should have been started and stopped</param>
        private static void PrintElapsedTime( Stopwatch timer )
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = timer.Elapsed;

            // Format and display the TimeSpan value. 
            if ( ts.Minutes <= 0 )
            {
                string elapsed_time = String.Format("{0:00}.{1:000} seconds",ts.Seconds, ts.Milliseconds);
                Console.WriteLine( $"    RunTime {elapsed_time}\n" );
            }
            else
            {
                string elapsed_time = String.Format("{0}:{00} mins/secs",ts.Minutes,ts.Seconds);
                Console.WriteLine( $"  RunTime {elapsed_time}\n" );
            }
        }

    }
}

// Observation 1
//
// There are two threads running.  The original thread that the main program is started
// in and the new thread created in the method and started.
//

// Observation 2
//
// The problem is that assignment and function calls are VERY FAST, but IO (input/output) is
// VERY SLOW (comparatively).  Thus as originally written, BOTH methods get into the protected
// (if) section, because whichever thread gets there first is "delayed" by the IO operation.
//

// Observation 3
//
//  This code shows what is called a "race" condition.  This means two threads are accessing data "at the same time".
//
//   Note: race means --> two entities (threads) are "racing" to see the first one
//                        to modify object, but we don't know which order it will actually happen.
//
//                        Additionally, if the "part" of the data can be modified at the same time another "part"
//                        is being modified, the state of the data can become corrupted!

// Observation 4
//
//    It is interesting to note (though perhaps not surprising) that the amount of time to count to 2000 is twice
//    the amount of time to count to 1000.  Thus predicting the time to count to 1_000_000.
//

// Observation 5
//
//    Depending on what core each thread is running on, the time to execute a multi-threaded version may
//    be significantly _LONGER_. This is because the data is normally stored in CACHE memory (fast) but
//    now has to be synced with MAIN memory (slower)
//

// Observation 6
//
//    Hint: longs and ints are not the same thing
//    Hint: what happens when you take Int.MAX and add one?
//

// Observation 7
//
//    Why is this division of work between threads wrong:
//         Thread one        0-10000
//         Thread two    10000-20000
//         Thread three  20000-30000
//
//    Hint: What is the cost of determining a prime based on (and what is its Big O)?
