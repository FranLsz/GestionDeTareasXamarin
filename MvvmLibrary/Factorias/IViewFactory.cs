using System;
using MvvmLibrary.ViewModel.Base;
using Xamarin.Forms;

namespace MvvmLibrary.Factorias
{
    public interface IViewFactory
    {
        // Registra un ViewModel con su Page
        void Register<TViewModel, TView>()
            where TViewModel : class, IViewModel
            where TView : Page;

        // Devuelve Page a partir de su ViewModel
        Page Resolve<TViewModel>(Action<TViewModel> action = null)
            where TViewModel : class, IViewModel;

        Page Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> action = null)
            where TViewModel : class, IViewModel;

        Page Resolve<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel;
    }
}