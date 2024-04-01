namespace Lab12_Agario_Start_JSON_Graphics;

using System.Diagnostics;
using Lab12Models;

/// <summary>
///   <list type="bullet">
///     <item>Author:  H James de St. Germain</item>
///     <item>Date:    Spring 2023</item>
///     <item>Updated: Spring 2024</item>
///     <item>Lab: 12</item>
///   </list>
///   <para>
///     This code represents a drawing surface for use with GraphicsViews, and
///     shows how to use invalidate, pass GUI elements around, compute world->screen
///     transformations, etc.
///   </para>
/// </summary>
public class MyCanvas : IDrawable
{
    /// <summary>
    ///   GUI Elements - For adapting to user requests (such as moving boxes on invalidate).
    /// </summary>
    private readonly CheckBox moveOnInvalidateCB;
    private readonly CheckBox invalidateAlwaysCB;
    private readonly GraphicsView gv;

    /// <summary>
    ///   These really should be computed at run time based on the XML specification.
    /// </summary>
    private readonly float width = 500;
    private readonly float height = 500;

    /// <summary>
    ///   world model that we have a reference to.
    /// </summary>
    private readonly List<Box> boxes = [];

    /// <summary>
    /// For use with some animation to illustrate invalidation
    /// </summary>
    private int x = 0;
    private int y = 100;

    /// <summary>
    ///   Create the IDrawable object and save important
    ///   information
    /// </summary>
    /// <param name="boxes"> the list to be drawn in the second part of the lab</param>
    /// <param name="moveOnInvalidateCheckBox"> if checked, move boxes to the right </param>
    /// <param name="invalidateAlwaysCheckBox"> if checked, recall invalidate inside of the draw routine</param>
    /// <param name="gv"> the graphics view - needed so we can call invalidate. </param>
    public MyCanvas( List<Box> boxes, CheckBox moveOnInvalidateCheckBox, CheckBox invalidateAlwaysCheckBox, GraphicsView gv )
    {
        this.boxes = boxes;
        this.moveOnInvalidateCB = moveOnInvalidateCheckBox;
        this.invalidateAlwaysCB = invalidateAlwaysCheckBox;
        this.gv = gv;
    }

    /// <summary>
    ///   <para>
    ///     Basic Draw Scene Method that is executed when the
    ///     GUI is invalidated.
    ///   </para>
    ///   <para>
    ///     Change method name to DrawOld when we move to the second part of the lab.
    ///   </para>
    /// </summary>
    /// <param name="canvas"> What we are drawing on</param>
    /// <param name="dirtyRect"> The area of the rectangle that has been changed. Not Used.</param>
    public void Draw( ICanvas canvas, RectF dirtyRect )
    {
        Debug.WriteLine( "RePainting the GUI" );

        // "Erase" the previous scene by drawing the background.
        canvas.FillColor = Colors.DarkBlue;
        canvas.FillRectangle( 0, 0, 1000, 500 );

        // for use in animation
        if ( this.moveOnInvalidateCB.IsChecked )
        {
            this.x += 5;
            if ( this.x > this.width )
            {
                this.x = 0;
            }
        }

        // Draw the static shape
        canvas.FillColor = Colors.Aqua;
        canvas.FillRectangle( x, y, 100, 100 );

        // to show the results of invalidating as fast as possible
        if ( invalidateAlwaysCB.IsChecked )
        {
            gv.Invalidate();

            // MAC users, replace the above with:
            // gv.Dispatcher.Dispatch( ( ) => gv.Invalidate() );
        }
    }
}
