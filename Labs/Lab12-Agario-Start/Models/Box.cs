namespace Lab12Models;

using System.Text.Json.Serialization;

/// <summary>
///   <list type="bullet">
///     <item>Author:   H James de St. Germain</item>
///     <item>Date:     Spring 2020</item>
///     <item>Updated:  Spring 2023 - Converted to MAUI</item>
///     <item>Lab: 12</item>
///   </list>
///   <para>
///     This code shows several important techniques and principles about dealing
///     with the following topics:
///   </para>
///   <list type="number">
///     <item> GUI - invalidation </item>
///     <item> GUI - coordinate system transformations </item>
///     <item> Networking - Protocol Usage</item>
///     <item> Threading - multiple threads accessing the same data at the same time</item>
///     <item> JSON - serialization and deserialization</item>
///   </list>
///   <para>
///     Don't just copy this code and tweak it, learn from the code, then write your
///     own version (and make it better!)
///   </para>
///   <para>
///     Build a random rectangular box at a give x,y (upper left corner) and
///     with a random w,h (width/height)
///   </para>
/// </summary>
public class Box
{
    public float X;
    public float Y;
    public float Width;
    public float Height;

    /// <summary>
    ///   build once, use often, static random number generator.
    /// </summary>
    static private Random generator = new Random();

    /// <summary>
    /// build a box somewhere (randomly) in the world with some portion of the world in length/height
    /// </summary>
    public Box( )
    {
        X = generator.Next( 0, 3000 );  //Warning: the 3000 and 2000 should really be constants in the world class!
        Y = generator.Next( 0, 2000 );
        Width = generator.Next( 50, 1000 );
        Height = generator.Next( 50, 1000 );
    }
}