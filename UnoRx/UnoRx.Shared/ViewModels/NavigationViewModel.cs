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
				.WhenAnyValue(vm => vm.SelectedNavigationItem)
				.Select(navItem => navItem.ViewModelType)
				.Select(vmType => (IRoutableViewModel)_ServiceProvider.GetRequiredService(vmType))
				.InvokeCommand(Router.Navigate)
				.DisposeWith(disposables);
			});
		}

		public IReadOnlyList<MenuItem> NavigationItems => new List<MenuItem>
		{
			new MenuItem(typeof(HomeViewModel), "Home", "Home"),
			new MenuItem(typeof(SettingsViewModel), "Settings", "Setting")
		}.AsReadOnly();

		[Reactive]
		public MenuItem SelectedNavigationItem { get; set; }

		public RoutingState Router { get; } = new RoutingState();
		public ViewModelActivator Activator { get; } = new ViewModelActivator();
	}

	public class MenuItem
	{
		public MenuItem(Type viewModelType, string title, string symbol)
		{
			ViewModelType = viewModelType;
			Title = title;
			Symbol = symbol;
		}

		public Type ViewModelType { get; }

		public string Title { get; }

		public string Symbol { get; }
	}
}
