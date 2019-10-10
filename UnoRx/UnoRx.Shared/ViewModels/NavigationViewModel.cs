using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace UnoRx.ViewModels
{
  public class NavigationViewModel : ReactiveObject, IScreen
  {
    public NavigationViewModel()
    {
    }

    public RoutingState Router { get; } = new RoutingState();
  }
}
