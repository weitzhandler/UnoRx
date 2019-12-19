using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace UnoRx.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
    }

    public abstract class RoutableViewModel : ViewModelBase, IRoutableViewModel
    {
        protected RoutableViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }

        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }
    }    
}
