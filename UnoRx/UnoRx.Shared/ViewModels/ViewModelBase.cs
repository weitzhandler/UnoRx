using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace UnoRx.ViewModels
{
  public abstract class ViewModelBase : ReactiveObject
  {
  }

  public abstract class RoutableViewModel : ViewModelBase, IRoutableViewModel
  {
    public RoutableViewModel(IScreen hostScreen)
    {
      HostScreen = HostScreen;
    }

    public string UrlPathSegment { get; }
    public IScreen HostScreen { get; }
  }
}
