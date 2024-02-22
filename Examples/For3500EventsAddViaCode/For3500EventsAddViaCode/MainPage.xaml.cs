
using System.Diagnostics;
using Windows.UI.Core;

namespace For3500EventsAddViaCode;

/// <summary>
///   
///   Author: H. James de St. Germain
///   Date:    Spring 2023
///   Updated: Spring 2024
///   
///   This code shows:
///   1) How to use the Notifier/Listener Design Pattern
///   2) How to add items to a the MAUI GUI via code, rather than hand coded XML
///   3) How to add/remove focus via programming
///   4) How to add a help page (one way of many).
/// </summary>
public partial class MainPage : ContentPage
{
    /// <summary>
    ///   Definition of the method signature that must be true for clear methods
    /// </summary>
    private delegate void Clear( );

    /// <summary>
    ///   Notifier Pattern.
    ///   When ClearAll(); is called, every "attached" method is called.
    /// </summary>
    private event Clear ClearAll;

    /// <summary>
    ///   List of Entries to show how to "move around" via enter key
    /// </summary>
    private MyEntry[] EntryColumn = new MyEntry[3];

    /// <summary>
    ///    Definition of what information (method signature) must be sent
    ///    by the Entry when it is modified.
    /// </summary>
    /// <param name="col"> col (char) in grid, e.g., A5 </param>
    /// <param name="row"> row (int) in grid,  e.g., A5 </param>
    public delegate void ActionOnCompleted( char col, int row );

    /// <summary>
    ///  Initialize GUI and add to it via code.
    /// </summary>
    public MainPage()
	{
		InitializeComponent();

        // I will need to use these later for the Enter Key functionality
        EntryColumn[0] = new MyEntry( 0, handleCellChanged );
        EntryColumn[1] = new MyEntry( 1, handleCellChanged );
        EntryColumn[2] = new MyEntry( 2, handleCellChanged );

        foreach ( var cell in EntryColumn )
        {
            EntryList.Add( cell );
            ClearAll += cell.ClearAndUnfocus;
        }

    }

    /// <summary>
    ///   This method will be called by the individual Entry elements when Enter
    ///   is pressed in them.
    ///   
    ///   The idea is to move to the next cell in the list.
    /// </summary>
    /// <param name="col"> e.g., The 'A' in A5 </param>
    /// <param name="row"> e.g., The  5  in A5 </param>
    void handleCellChanged( char col, int row )
	{
		Debug.WriteLine( $"changed: {col}{row}" );
        if ( row == 0 ) { EntryColumn[1].Focus(); }  // Notice how we can move the focus
        if ( row == 1 ) { EntryColumn[2].Focus(); }
        if ( row == 2 ) { EntryColumn[0].Focus(); }

        ErrorMessages.Text = $"Moving Focus to row {(row+1) % 3}";

    }

    /// <summary>
    ///   Shows how the single "event" method ClearAll can apply to many listeners.
    /// </summary>
    /// <param name="sender"> ignored </param>
    /// <param name="e"> ignored </param>
    void ClearButtonClicked(object sender, EventArgs e )
    {
        ErrorMessages.Text = "Clear Button Clicked";
        ClearAll();
        EntryColumn[0].Focus();
    }

    /// <summary>
    ///   Shows how to "push" a new "window" on top of the old and then
    ///   revert.  This is one way to go about the help page implementation.
    ///   
    ///   <para>
    ///     Notice that this recreates a page in code every time... You should either
    ///     only do this once, or have another xaml widget to use....
    ///   </para>
    /// </summary>
    /// <param name="sender"> ignored </param>
    /// <param name="e"> ignored </param>
    void HelpInformation( object sender, EventArgs e )
    {
        var page = new HelpPage();

        Navigation.PushAsync( page, true );
    }


}

