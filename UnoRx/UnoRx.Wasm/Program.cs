using System.Reactive.PlatformServices;
using Uno.UI;
using Windows.UI.Xaml;

namespace UnoRx.Wasm
{
    public class Program
    {
        private static App App;

        static int Main()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            PlatformEnlightenmentProvider.Current.EnableWasm();
#pragma warning restore CS0618 // Type or member is obsolete

#if DEBUG
            FeatureConfiguration.UIElement.AssignDOMXamlName = true;
#endif
            Application.Start(_ => App = new App());

            return 0;
        }
    }
}
