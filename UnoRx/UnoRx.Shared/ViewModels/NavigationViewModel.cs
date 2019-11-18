using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace UnoRx.ViewModels
{
  public class NavigationViewModel : ViewModelBase, IScreen, IActivatableViewModel
  {
    readonly IServiceProvider _ServiceProvider;
    public NavigationViewModel(IServiceProvider serviceProvider)
    {
      _ServiceProvider = serviceProvider;
      SelectedNavigationItem = NavigationItems.First();

      this.WhenActivated(disposables =>
      {
        this
          .WhenAnyValue(vm => vm.Router.Changed, vm => vm.SelectedNavigationItem, (_, navItem) => navItem)
          .DistinctUntilChanged()
          .Select(navItem => navItem.ViewModelType)
          .Select(vmType => (IRoutableViewModel)serviceProvider.GetRequiredService(vmType))
          .InvokeCommand(Router.Navigate)
          .DisposeWith(disposables);
      });
    }

    public IReadOnlyList<NavigationItem> NavigationItems => new List<NavigationItem>
       {
         new NavigationItem { Title = "Home", ViewModelType = typeof(HomeViewModel), Symbol = "Home" },
         new NavigationItem { Title = "Settings", ViewModelType = typeof(SettingsViewModel), Symbol = "Setting"}
       }.AsReadOnly();

    [Reactive]
    public NavigationItem SelectedNavigationItem { get; set; }

    public RoutingState Router { get; } = new RoutingState();
    public ViewModelActivator Activator { get; } = new ViewModelActivator();
  }

  public class NavigationItem
  {
    public Type ViewModelType { get; set; }

    public string Title { get; set; }

    public string Symbol { get; set; }
  }
}
