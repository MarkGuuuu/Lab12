
namespace For3500EventsAddViaCode;

/// <summary>
///   
///   Author: H. James de St. Germain
///   Date:   Spring 2024
///   
///   <para>
///   This code shows:
///   </para>   
///   
///   <list type="bullet">
///     <item>
///       an example of adding an event handler to a button via code rather than using XML
///     </item>
///   </list>
///   
/// </summary>
public partial class HelpPage : ContentPage
{
    /// <summary>
    ///  Initialize GUI and add to it via code.
    /// </summary>
    public HelpPage( )
    {
        InitializeComponent();

        ReturnToMainPageButton.Clicked += ReturnToMainPage;
    }

    /// <summary>
    ///   Invariant: Can only be called from a page that has been "pushed"
    ///   onto the navigation stack.
    /// </summary>
    /// <param name="sender"> ignored </param>
    /// <param name="e">      ignored </param>
    async void ReturnToMainPage( object sender, EventArgs e )
    {
        await Navigation.PopAsync(); // This will return to the main page
    }

}

