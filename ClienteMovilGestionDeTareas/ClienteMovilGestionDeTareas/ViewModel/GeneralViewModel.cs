using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using MvvmLibrary.Factorias;
using MvvmLibrary.ViewModel.Base;

namespace ClienteMovilGestionDeTareas.ViewModel
{
    public class GeneralViewModel : ViewModelBase
    {
        protected INavigator _navigator;
        protected IServicioDatos _servicioDatos;
        protected IPage _page;
        protected Session Session;

        public GeneralViewModel(INavigator navigator, IServicioDatos servicioDatos, Session session, IPage page)
        {
            _navigator = navigator;
            _servicioDatos = servicioDatos;
            _page = page;
            Session = session;
        }
    }
}