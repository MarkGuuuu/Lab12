using Lab4;
/// <summary>
///   Author: H. James de St. Germain
///   Date:   Spring 2022
///   
///   The following code is for Lab4 - Mastering Visual Studio.
///   
///   It is intended to allow students to try various keyboard shortcuts and
///   other refactoring techniques.
/// </summary>

Console.WriteLine( "Testing Quick Sort" );

int[] data = { 6, 6, 2, 5, 5, 4, 3, 4, 6, 5, 4, 1, 6, 2, 3, 6, 3, 4, 5, 5, 6 };

QS.Sort( data, 0, data.Length-1 ); // TODO: Step 2b

string result = "";

// TODO: Step 9
//  The following code is useful for debugging. Uncomment it all at once.
//  Can you figure out how to comment it again with // vs. /* ?
//foreach (var val in data )
//{
//    result += val + ", " ;
//}

//result = result.Substring( 0, result.Length-2 );


Console.WriteLine( result );  // TODO: Step 2a

Student x = new Student();

Student y = new Student();

if ( x == y )
{
    Console.WriteLine( "equal!" );
}

if ( x == null )  // does your equals operator get called?
{
    Console.WriteLine( "what?" );
}

