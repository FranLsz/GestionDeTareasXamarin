using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using DataModel.ViewModel;
using MvvmLibrary.Factorias;

namespace ClienteMovilGestionDeTareas.ViewModel.Tarea
{
    public class DetalleTareaViewModel : GeneralViewModel
    {
        public TareaModel Tarea { get; set; }

        public DetalleTareaViewModel(INavigator navigator, IServicioDatos servicioDatos, Session session, IPage page) : base(navigator, servicioDatos, session, page)
        {
        }
    }
}