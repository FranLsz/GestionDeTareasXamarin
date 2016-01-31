using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using MvvmLibrary.Factorias;

namespace ClienteMovilGestionDeTareas.ViewModel.Tarea
{
    public class EditarTareaViewModel : GeneralViewModel
    {
        public EditarTareaViewModel(INavigator navigator, IServicioDatos servicioDatos, Session session, IPage page) : base(navigator, servicioDatos, session, page)
        {

        }
    }
}