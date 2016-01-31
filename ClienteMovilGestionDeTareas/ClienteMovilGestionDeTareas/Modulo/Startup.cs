using Autofac;
using ClienteMovilGestionDeTareas.View;
using ClienteMovilGestionDeTareas.ViewModel;
using MvvmLibrary.Factorias;
using MvvmLibrary.Modulo;
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
            viewFactory.Register<LoginViewModel, Login>();
            viewFactory.Register<HomeViewModel, Home>();
        }

        protected override void ConfigureApplication(IContainer container)
        {
            var viewFactory = container.Resolve<IViewFactory>();
            var main = viewFactory.Resolve<LoginViewModel>(vm =>
            {
                vm.Titulo = "Inicio de sesión";
            });
            var np = new NavigationPage(main);
            _application.MainPage = np;
        }
    }
}