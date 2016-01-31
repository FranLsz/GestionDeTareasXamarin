using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ClienteMovilGestionDeTareas.Model;
using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using DataModel.ViewModel;
using MvvmLibrary.Factorias;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.ViewModel.Tarea
{
    public class ListadoTareasViewModel : GeneralViewModel
    {
        public ICommand CmdNuevaTarea { get; set; }

        public string MisTareasLbl => "Mis tareas";
        public string NuevaTareaLbl => "Nueva tarea";

        private ObservableCollection<TareaVm> _tareas;
        public ObservableCollection<TareaVm> Tareas
        {
            get
            {
                return _tareas;
            }
            set { SetProperty(ref _tareas, value); }
        }

        private TareaVm _tareaSeleccionada;
        public TareaVm TareaSeleccionada
        {
            get { return _tareaSeleccionada; }
            set
            {
                VerDetalleTarea(value);
                _tareaSeleccionada = null;
                SetProperty(ref value, null);
            }
        }

        public GrupoModel Grupo { get; set; }

        public ListadoTareasViewModel(INavigator navigator, IServicioDatos servicioDatos, Session session, IPage page) : base(navigator, servicioDatos, session, page)
        {
            CmdNuevaTarea = new Command(NuevaTarea);
        }

        private async void NuevaTarea()
        {
            await _navigator.PushAsync<NuevaTareaViewModel>(vm =>
            {
                vm.Titulo = "Nueva tarea para " + Titulo;
                vm.Grupo = Grupo;
            });
        }

        private void VerDetalleTarea(TareaVm value)
        {
            //
        }

        public async Task BorrarTarea(int id)
        {
            var res = await _servicioDatos.DeleteTarea(id);

            if (res)
            {
                Tareas.Remove(Tareas.First(o => o.TareaModel.Id == id));
                await _page.MostrarAlerta("", "Tarea eliminada correctamente", "Ok");

            }
            else
                await _page.MostrarAlerta("", "La tarea no pudo ser borrada", "Ok");
        }

        public async Task EditarTarea(TareaModel model)
        {
            //
        }

        public async Task FinalizarTarea(TareaModel model)
        {
            model.Finalizada = true;
            var res = await _servicioDatos.UpdateTarea(model);

            if (res)
            {
                await _page.MostrarAlerta("", "Tarea finalizada", "Ok");

            }
            else
                await _page.MostrarAlerta("", "La tarea no pudo ser finalizada", "Ok");
        }

    }
}