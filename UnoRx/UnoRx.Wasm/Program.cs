using Splat.Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.PlatformServices;
using Uno.UI;
using Windows.UI.Xaml;

namespace UnoRx.Wasm
{
  public class Program
  {
    private static App _app;

    static int Main(string[] args)
    {
#pragma warning disable CS0618 // Type or member is obsolete
      PlatformEnlightenmentProvider.Current.EnableWasm();
#pragma warning restore CS0618 // Type or member is obsolete

      FeatureConfiguration.UIElement.AssignDOMXamlName = true;
      Windows.UI.Xaml.Application.Start(_ => _app = new App());

      return 0;
    }
  }
}
