using Autofac;
using MvvmLibrary.Factorias;
using Xamarin.Forms;

namespace MvvmLibrary.Modulo
{
    // Registro de factorías
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ViewFactory>().
                 As<IViewFactory>().
                 SingleInstance();
            builder.RegisterType<Navigator>().
                 As<INavigator>().
                 SingleInstance();
            
        }
    }
}
