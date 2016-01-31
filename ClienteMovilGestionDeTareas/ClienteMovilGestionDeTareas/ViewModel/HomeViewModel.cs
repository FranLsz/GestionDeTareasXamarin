using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using MvvmLibrary.Factorias;
using RedContactos.ViewModel;

namespace ClienteMovilGestionDeTareas.ViewModel
{
    public class HomeViewModel : GeneralViewModel
    {
        public HomeViewModel(INavigator navigator, IServicioDatos servicio, Session session, IPage page) : base(navigator, servicio, session, page)
        {
        }

    }
}