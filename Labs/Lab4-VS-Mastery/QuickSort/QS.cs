using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
///   The following code originated from: http://csharpexamples.com/c-quick-sort-algorithm-implementation/
///   
///   It has been modified for use in the CS 3500 lab
///   
///   Author: H. James de St. Germain
///   Date: Spring 2022
///   
///   Quicksort recursive sorting algorithm
/// </summary>

namespace Lab4
{
    // TODO: Step 7a
    //         Refactor: Rename QS to QuickSort  (Ctrl R Ctrl R)
    public class QS 
    {
        /// <summary>
        ///   TODO: Step 7d
        ///   Try extracting the interface from the QuickSort Class
        ///   This is a dummy method to show the use of "extract interface"
        /// </summary>
        /// <returns></returns>
        public string Version()
        {
            return "1.0";
        }

        public int add_one( int i)
        {
            // TODO: Step 10
            //   select this entire method and format it.
    int j = i - 10;
            if ( j > i)
            j = i;
                         return i+1;

            // TODO: Step 11
            // foreach (int k in i)    
        }

        public static void Sort( int[] arr, int start, int end )
        {
            int i;

            if ( start < end )
            {
                // TODO: Step 7c
                //   Everything from "Jim partition the array..." to "Jim Done Partition" should be extrated into its own method!
                // Jim partition the array  (Step X - extract method)
                int temp;
                int p = arr[end];
                i = start - 1;

                for ( int j = start; j <= end - 1; j++ )
                {
                    // TODO: Step 7b
                    //   - insert the following if around the
                    //       code below. Don't just uncomment, use the "Surround With"
                    //       shortcut!
                    //if ( arr[j] <= p )
                    //{
                    i++;
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    //}
                }

                // TODO: Step 13
                temp = arr[i + 1];
                arr[i + 1] = arr[end];
                arr[end] = temp;
                i = i + 1;
                // Jim Done Partitioning

                // TODO: Step 3
                //  Use the Jump to Line and then remove these outdated lines of commenting

                Sort( arr, i + 1, end );        // TODO: Step 8
                Sort( arr, start, i - 1 );      // Reverse these lines

                // TODO: Step 14
                //   Use "View Call Hierarcy" on Sort above.
            }
        }

    } // TODO: Step 4 
      //    - Jump back and forth to the starting { and back  (Ctrl ])
}


// TODO: Step 5
//   jump all the way back into the TestQS file and then back here.
//   Ctrl -,  Ctrl Shift -


//
//  TODO: Step 6
//   If you have been deleting these comments, your "clipboard ring" should have
//   a good number of things to paste.  (Ctrl Shift Insert)
// 

