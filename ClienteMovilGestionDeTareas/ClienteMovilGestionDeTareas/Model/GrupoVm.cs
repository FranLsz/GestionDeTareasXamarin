using System.Windows.Input;
using Autofac;
using ClienteMovilGestionDeTareas.ViewModel;
using DataModel.ViewModel;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.Model
{
    public class GrupoVm
    {
        public ICommand CmdBorrar { get; set; }
        public GrupoModel GrupoModel { get; set; }
        public IComponentContext ComponentContext { get; set; }

        public GrupoVm()
        {
            CmdBorrar = new Command(RunCmdBorrar);
        }

        private async void RunCmdBorrar()
        {
            var vm = ComponentContext.Resolve<HomeViewModel>();
            await vm.BorrarGrupo(GrupoModel.Id);
        }
    }
}