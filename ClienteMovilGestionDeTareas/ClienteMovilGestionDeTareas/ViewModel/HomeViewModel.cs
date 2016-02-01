using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using ClienteMovilGestionDeTareas.Model;
using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using ClienteMovilGestionDeTareas.ViewModel.Grupo;
using ClienteMovilGestionDeTareas.ViewModel.Tarea;
using DataModel.ViewModel;
using MvvmLibrary.Factorias;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.ViewModel
{
    public class HomeViewModel : GeneralViewModel
    {
        public IComponentContext Context { get; set; }

        public ICommand CmdNuevoGrupo { get; set; }

        public string MisGruposLbl => "Mis grupos de tareas";
        public string NuevoGrupoLbl => "Nuevo grupo";

        private ObservableCollection<GrupoVm> _grupos;
        public ObservableCollection<GrupoVm> Grupos
        {
            get
            {
                return _grupos;
            }
            set { SetProperty(ref _grupos, value); }
        }

        public GrupoVm GrupoSeleccionado
        {
            get { return _grupoSeleccionado; }
            set
            {
                VerListaTareas(value);
                _grupoSeleccionado = null;
                SetProperty(ref value, null);
            }
        }

        private GrupoVm _grupoSeleccionado;


        public HomeViewModel(INavigator navigator, IServicioDatos servicioDatos, Session session, IPage page, IComponentContext ctx) : base(navigator, servicioDatos, session, page)
        {
            Context = ctx;
            CmdNuevoGrupo = new Command(NuevoGrupo);
            GetGrupos();
        }

        private async void GetGrupos()
        {
            var grupos = await _servicioDatos.GetGrupos(Session.User.Id);

            var oc = new ObservableCollection<GrupoVm>();
            foreach (var contactoModel in grupos)
            {
                oc.Add(new GrupoVm()
                {
                    ComponentContext = Context,
                    GrupoModel = contactoModel
                });
            }
            Grupos = oc;
        }

        public async void VerListaTareas(GrupoVm model)
        {
            try
            {
                IsBusy = true;
                var tareas = await _servicioDatos.GetTareas(model.GrupoModel.Id);

                var oc = new ObservableCollection<TareaVm>();
                foreach (var tareaModel in tareas)
                {
                    oc.Add(new TareaVm()
                    {
                        ComponentContext = Context,
                        TareaModel = tareaModel
                    });
                }

                await _navigator.PushAsync<ListadoTareasViewModel>(vm =>
                {
                    vm.Titulo = model.GrupoModel.Nombre;
                    vm.Grupo = model.GrupoModel;
                    vm.Tareas = new ObservableCollection<TareaVm>(oc);
                });
            }
            catch (Exception ex)
            {
                await _page.MostrarAlerta("Error", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void NuevoGrupo()
        {
            await _navigator.PushAsync<NuevoGrupoViewModel>(vm =>
            {
                vm.Titulo = "Nuevo grupo";
            });
        }

        public async Task BorrarGrupo(int id)
        {
            try
            {
                IsBusy = true;
                var res = await _servicioDatos.DeleteGrupo(id);

                if (res)
                {
                    Grupos.Remove(Grupos.First(o => o.GrupoModel.Id == id));
                    await _page.MostrarAlerta("", "Grupo eliminado correctamente", "Ok");

                }
                else
                    await _page.MostrarAlerta("", "El grupo no pudo ser borrado", "Ok");
            }
            catch (Exception ex)
            {
                await _page.MostrarAlerta("Error", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}