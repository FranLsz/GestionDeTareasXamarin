using System;
using System.Threading.Tasks;
using MvvmLibrary.ViewModel.Base;
using Xamarin.Forms;

namespace MvvmLibrary.Factorias
{
    public class Navigator : INavigator
    {
        private readonly Lazy<INavigation> _navigation;
        private readonly IViewFactory _viewFactory;
        public INavigation Navigation { get { return _navigation.Value; } }

        public Navigator(Lazy<INavigation> navigation, IViewFactory viewFactory)
        {
            _navigation = navigation;
            _viewFactory = viewFactory;
        }

        public async Task<IViewModel> PopAsync()
        {
            Page vista = await Navigation.PopAsync();
            // binding context almacena cual es el objeto que le va a dar el data binding
            return vista.BindingContext as IViewModel;
        }

        public async Task<IViewModel> PopModalAsync()
        {
            Page vista = await Navigation.PopModalAsync();
            return vista.BindingContext as IViewModel;
        }

        public async Task PopToRootAsync()
        {
            await Navigation.PopToRootAsync();
        }

        public async Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> action = null) where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var vista = _viewFactory.Resolve<TViewModel>(out viewModel, action);
            await Navigation.PushAsync(vista);
            return viewModel;
        }

        public async Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            var vista = _viewFactory.Resolve<TViewModel>(viewModel);
            await Navigation.PushAsync(vista);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(Action<TViewModel> action = null) where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var vista = _viewFactory.Resolve<TViewModel>(out viewModel, action);
            await Navigation.PushModalAsync(vista);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            var vista = _viewFactory.Resolve<TViewModel>(viewModel);
            await Navigation.PushModalAsync(vista);
            return viewModel;
        }


    }
}
