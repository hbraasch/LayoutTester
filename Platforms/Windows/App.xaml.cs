using Microsoft.Maui.Controls;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LayoutTester.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
	/// <summary>
	/// Initializes the singleton application object.  This is the first line of authored code
	/// executed, and as such is the logical equivalent of main() or WinMain().
	/// </summary>
	public App()
	{
		this.InitializeComponent();

    }
    // https://stackoverflow.com/questions/72399551/maui-net-set-window-size
    protected override void OnLaunched(LaunchActivatedEventArgs args)
	{
		base.OnLaunched(args);

#if false
        var currentWindow = Application.Windows[0].Handler.PlatformView;
        IntPtr _windowHandle = WindowNative.GetWindowHandle(currentWindow);
        var windowId = Win32Interop.GetWindowIdFromWindow(_windowHandle);

        AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
        const int width = 600;
        const int height = 1000;

        appWindow.MoveAndResize(new RectInt32(0, 0, width, height));
#else
        var currentWindow = Application.Windows[0].Handler.PlatformView;
        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(currentWindow);
        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
        if (winuiAppWindow.Presenter is OverlappedPresenter p)
        {
            var x = winuiAppWindow.Size;
            p.Maximize();
        }
        else
        {
            const int width = 600;
            const int height = 1000;
            winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
        }
#endif

    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

