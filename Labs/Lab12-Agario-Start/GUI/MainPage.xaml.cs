namespace Lab12_Agario_Start_JSON_Graphics;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using Lab12Models;

/// <summary>
///   <list type="bullet">
///     <item>Author: H. James de St. Germain</item>
///     <item>Date: Spring 2023</item>
///     <item>Updated: Spring 2024</item>
///     <item>Copyright: Jim de St. Germain </item>
///     <item>
///       <remark>
///         Sample code should be used as an inspiration and learning tool. Do not simply cut and paste it into your project.
///       </remark>
///     </item>
///   </list>
///   <para>
///   This project represents a GUI example of how to:
///   </para>
///   <list type="bullet">
///     <item> Connect to a Server </item>
///     <item> Communicate using a protocol of messages (and JSON) </item>
///     <item> Serialize/Deserialize JSON into objects</item>
///     <item> Use a Canvas object </item>
///   </list>
/// </summary>
public partial class MainPage : ContentPage
{
    /// <summary>
    ///   Boxes to Draw (Come from network!)
    /// </summary>
    private readonly List<Box> boxes = [];

    /// <summary>
    ///   Do we try to resize the window at the start of the program?
    /// </summary>
    private bool initialResize = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    public MainPage( )
    {
        this.InitializeComponent();
        this.DrawOnMe.Drawable = new MyCanvas( boxes, MoveOnUpdateCheckBox, InvalidateAlwaysCheckBox, DrawOnMe );
    }

    /// <summary>
    ///    On the first display of window, try to set the size
    ///    to the constants below.
    /// </summary>
    /// <param name="width"> how wide the client should be</param>
    /// <param name="height"> how tall the client should be</param>
    protected override void OnSizeAllocated( double width, double height )
    {
        base.OnSizeAllocated( width, height );

        if ( this.initialResize )
        {
            this.initialResize = false;

            if ( RuntimeInformation.IsOSPlatform( OSPlatform.Windows ) )
            {
                this.Window.Width = 600;
                this.Window.Height = 900;
            }
        }
    }

    /// <summary>
    ///   <para>
    ///     When the connect button is clicked, connect to the server program
    ///     and start the sequence of communications. Update the button text for phase 2.
    ///   </para>
    ///   <para>
    ///     Once connected, swap out this button handler (ConnectBtnClicked) with a new one
    ///     that makes a network request for more boxes.
    ///   </para>
    /// </summary>
    /// <param name="sender"> ignored </param>
    /// <param name="e"> ignored </param>
    private async void ConnectBtnClicked( object? sender, EventArgs e )
    {
        Debug.WriteLine( "Not used in first part of lab." );
    }

    /// <summary>
    ///   Button Click Handler - "redraws" (invalidates) the screen.
    ///   Note: the redraw will happen at some point in the (near) future.
    /// </summary>
    /// <param name="sender"> ignored </param>
    /// <param name="e"> ignored </param>
    private void InvalidateBtnClicked( object sender, EventArgs e )
    {
        DrawOnMe.Invalidate();
    }

    /// <summary>
    /// Force the program to invalidate (and re-draw) as fast as it can.
    /// </summary>
    /// <param name="sender">ignored</param>
    /// <param name="e">ignored</param>
    private void InvalidateAlwaysBtnClicked( object sender, EventArgs e )
    {
        DrawOnMe.Invalidate();
    }

}
