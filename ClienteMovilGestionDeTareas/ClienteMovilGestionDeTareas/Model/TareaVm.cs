using System.Windows.Input;
using Autofac;
using ClienteMovilGestionDeTareas.ViewModel;
using ClienteMovilGestionDeTareas.ViewModel.Tarea;
using DataModel.ViewModel;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.Model
{
    public class TareaVm
    {
        public ICommand CmdBorrar { get; set; }
        public ICommand CmdFinalizar { get; set; }
        public ICommand CmdEditar { get; set; }

        public TareaModel TareaModel { get; set; }
        public IComponentContext ComponentContext { get; set; }

        public TareaVm()
        {
            CmdBorrar = new Command(RunCmdBorrar);
            CmdFinalizar = new Command(RunCmdFinalizar);
            CmdEditar = new Command(RunCmdEditar);
        }

        private async void RunCmdEditar()
        {
            var vm = ComponentContext.Resolve<ListadoTareasViewModel>();
            await vm.EditarTarea(TareaModel);

        }

        private async void RunCmdFinalizar()
        {
            var vm = ComponentContext.Resolve<ListadoTareasViewModel>();
            await vm.FinalizarTarea(TareaModel);
        }

        private async void RunCmdBorrar()
        {
            var vm = ComponentContext.Resolve<ListadoTareasViewModel>();
            await vm.BorrarTarea(TareaModel.Id);
        }
    }
}