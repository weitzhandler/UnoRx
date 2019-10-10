using ReactiveUI;
using ReactiveUI.Uno;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Windows.UI.Xaml;

namespace UnoRx.Views
{
#if HAS_UNO && __IOS__
  [global::Foundation.Register]
#endif
  [SuppressMessage("Design", "CA1010:Collections should implement generic interface", Justification = "Deliberate usage")]
  public abstract
#if HAS_UNO
        partial
#endif
        class AppReactivePage<TViewModel> :
        ReactiveUI.Uno.ReactivePage<TViewModel>, IViewFor<TViewModel>
        where TViewModel : class
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="AppReactivePage{TViewModel}"/> class.
    /// </summary>
    // needed so the others are optional.
    protected AppReactivePage()
    {
      AttachHandlers();
    }

#if HAS_UNO
#if __ANDROID__
    /// <summary>
    /// Initializes a new instance of the <see cref="ReactivePage{TViewModel}"/> class.
    /// Native constructor, do not use explicitly.
    /// </summary>
    /// <remarks>
    /// Used by the Xamarin Runtime to materialize native
    /// objects that may have been collected in the managed world.
    /// </remarks>
    /// <param name="javaReference">A <see cref="IntPtr"/> containing a Java Native Interface (JNI) object reference.</param>
    /// <param name="transfer">A <see cref="JniHandleOwnership"/> indicating how to handle handle.</param>
    protected AppReactivePage(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer)
        : base(javaReference, transfer)
    {
      AttachHandlers();
    }
#endif
#if __IOS__
    /// <summary>
    /// Initializes a new instance of the <see cref="ReactivePage{TViewModel}"/> class.
    /// Native constructor, do not use explicitly.
    /// </summary>
    /// <param name="handle">Handle to the native control.</param>
    /// <remarks>
    /// Used by the Xamarin Runtime to materialize native.
    /// objects that may have been collected in the managed world.
    /// </remarks>
    protected AppReactivePage(IntPtr handle)
        : base(handle)
    {
      AttachHandlers();
    }
#endif
#endif

    void AttachHandlers()
    {
      DataContextChanged += (s, e) => OnDataContextChanged(e.NewValue);
      RegisterPropertyChangedCallback(ViewModelProperty, (s, p) => OnViewModelChanged());
    }

    protected virtual void OnDataContextChanged(object newValue)
    {
      if (ViewModel != newValue)
        ViewModel = newValue as TViewModel;
    }

    protected virtual void OnViewModelChanged()
    {
      if (DataContext != ViewModel)
        DataContext = ViewModel;
    }
  }
}
