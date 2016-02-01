using System.Collections.ObjectModel;
using Autofac;
using ClienteMovilGestionDeTareas.Model;
using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using ClienteMovilGestionDeTareas.View;
using ClienteMovilGestionDeTareas.View.Grupo;
using ClienteMovilGestionDeTareas.View.Tarea;
using ClienteMovilGestionDeTareas.ViewModel;
using ClienteMovilGestionDeTareas.ViewModel.Grupo;
using ClienteMovilGestionDeTareas.ViewModel.Tarea;
using DataModel.ViewModel;
using MvvmLibrary.Factorias;
using MvvmLibrary.Modulo;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.Modulo
{
    public class Startup : AutofacBootstrapper
    {
        private readonly App _application;

        public Startup(App application)
        {
            _application = application;
        }

        public override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
            builder.RegisterModule<GestionDeTareasModule>();
        }

        protected override void RegisterViews(IViewFactory viewFactory)
        {
            viewFactory.Register<LoginViewModel, LoginView>();
            viewFactory.Register<HomeViewModel, HomeView>();
            viewFactory.Register<NuevoGrupoViewModel, NuevoGrupoView>();
            viewFactory.Register<ListadoTareasViewModel, ListadoTareasView>();
            viewFactory.Register<NuevaTareaViewModel, NuevaTareaView>();
            viewFactory.Register<DetalleTareaViewModel, DetalleTareaView>();
        }

        protected override void ConfigureApplication(IContainer container)
        {
            var viewFactory = container.Resolve<IViewFactory>();

            var txt = DependencyService.Get<IServicioFicheros>().RecuperarTexto(Cadenas.SettingsFile);


            if (string.IsNullOrEmpty(txt))
            {
                var main = viewFactory.Resolve<LoginViewModel>(vm =>
                {
                    vm.Titulo = "Inicio de sesión";
                });
                var np = new NavigationPage(main);
                _application.MainPage = np;
            }
            else
            {
                var data = JsonConvert.DeserializeObject<UsuarioModel>(txt);
                Session.User = data;
                var main = viewFactory.Resolve<HomeViewModel>(vm =>
                {
                    vm.Titulo = "Bienvenido de nuevo " + Session.User.Nombre;
                });
                var np = new NavigationPage(main);
                _application.MainPage = np;
            }


        }
    }
}