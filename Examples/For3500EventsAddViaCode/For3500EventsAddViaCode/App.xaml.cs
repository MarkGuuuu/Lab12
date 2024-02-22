namespace For3500EventsAddViaCode;

/// <summary>
///    Updated by: H. James de St. Germain
///    Updated date:  Spring 2024
///    
///    Example of how to set the window width/height
/// </summary>
public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

    /// <summary>
    ///   Code courtesy of:
    ///   
    ///     https://stackoverflow.com/questions/75870718/how-to-set-the-window-dimensions-for-the-windows-machine-target-maui
    /// </summary>
    /// <param name="activationState"> ignored </param>
    /// <returns> the window with the bounding sizes</returns>
    protected override Window CreateWindow( IActivationState activationState )
    {
        var window = new Window();
        window.MinimumHeight = 500;
        window.MaximumHeight = 800;
        
        window.Height = 800;
        window.Width = 500;

        window.MinimumWidth = 500;
        window.MaximumWidth = 800;

        window.Page = new AppShell();

        return window;
    }

}
