using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using MvvmLibrary.Factorias;
using MvvmLibrary.ViewModel.Base;

namespace RedContactos.ViewModel
{
    public class GeneralViewModel : ViewModelBase
    {
        protected INavigator _navigator;
        protected IServicioDatos _servicio;
        protected IPage _page;
        protected Session Session;

        public GeneralViewModel(INavigator navigator, IServicioDatos servicio, Session session, IPage page)
        {
            _navigator = navigator;
            _servicio = servicio;
            _page = page;
            Session = session;
        }
    }
}