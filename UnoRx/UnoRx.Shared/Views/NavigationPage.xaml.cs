﻿using UnoRx.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnoRx.Views
{
    public abstract partial class NavigationPageBase : AppReactivePage<NavigationViewModel>
    {
    }                                              

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class NavigationPage : NavigationPageBase
    {
        public NavigationPage()
        {
            this.InitializeComponent();
        }
    }
}
