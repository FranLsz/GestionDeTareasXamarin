using System;
using Autofac;
using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using ClienteMovilGestionDeTareas.View;
using ClienteMovilGestionDeTareas.View.Grupo;
using ClienteMovilGestionDeTareas.View.Tarea;
using ClienteMovilGestionDeTareas.ViewModel;
using ClienteMovilGestionDeTareas.ViewModel.Grupo;
using ClienteMovilGestionDeTareas.ViewModel.Tarea;
using MvvmLibrary.Factorias;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.Modulo
{
    public class GestionDeTareasModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServicioDatos>().As<IServicioDatos>().SingleInstance();
            builder.Register<INavigation>(ctx => App.Current.MainPage.Navigation).SingleInstance();
            builder.RegisterType<Session>().SingleInstance();

            // PAGES
            builder.RegisterType<LoginView>().SingleInstance();
            builder.RegisterType<HomeView>().SingleInstance();
            builder.RegisterType<NuevoGrupoView>().SingleInstance();
            builder.RegisterType<ListadoTareasView>().SingleInstance();
            builder.RegisterType<NuevaTareaView>().SingleInstance();
            builder.RegisterType<DetalleTareaView>().SingleInstance();

            // VIEWMODELS
            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<HomeViewModel>().SingleInstance();
            builder.RegisterType<NuevoGrupoViewModel>();
            builder.RegisterType<ListadoTareasViewModel>().SingleInstance();
            builder.RegisterType<NuevaTareaViewModel>();
            builder.RegisterType<DetalleTareaViewModel>();





            builder.RegisterInstance<Func<Page>>(() =>
            {
                // accedemos al mainpage y pedimos la masterdetaulpage
                var masterP = App.Current.MainPage as MasterDetailPage;
                // una vez tenemos el master, objetemos el page, si es nulo, obtenemos el masterdetail entero
                var page = masterP != null ? masterP.Detail : App.Current.MainPage;
                // lo mismo
                var navigation = page as IPageContainer<Page>;
                return navigation != null ? navigation.CurrentPage : page;
            });
            builder.RegisterType<DialogService>().
                 As<IDialogService>().
                 SingleInstance();
            builder.RegisterType<PageProxy>().
                 As<IPage>().
                 SingleInstance();
        }
    }
}