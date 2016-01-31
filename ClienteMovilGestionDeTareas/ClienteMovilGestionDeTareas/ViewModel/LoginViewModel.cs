using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Autofac;
using ClienteMovilGestionDeTareas.Model;
using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using DataModel.ViewModel;
using MvvmLibrary.Factorias;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.ViewModel
{
    public class LoginViewModel : GeneralViewModel
    {
        public IComponentContext Context { get; set; }

        // COMMANDS
        public ICommand cmdLogin { get; set; }
        public ICommand cmdRegistro { get; set; }

        // PROPERTIES
        public string IniciarLabel { get { return "Iniciar sesión"; } }
        public string RegistroLabel { get { return "Registro"; } }
        public string TituloEmail { get { return "Email"; } }
        public string TituloPassword { get { return "Contraseña"; } }

        private UsuarioModel _usuario;
        public UsuarioModel Usuario
        {
            get { return _usuario; }
            set { SetProperty(ref _usuario, value); }
        }


        // CTOR
        public LoginViewModel(INavigator navigator, IServicioDatos servicioDatos, Session session, IPage page, IComponentContext ctx) : base(navigator, servicioDatos, session, page)
        {
            Context = ctx;
            Usuario = new UsuarioModel();
            Usuario = new UsuarioModel();
            cmdLogin = new Command(IniciarSesion);
            cmdRegistro = new Command(NuevoUsuario);
        }

        private async void NuevoUsuario()
        {
            //await _navigator.PushAsync<RegistroViewModel>();
        }


        private async void IniciarSesion()
        {

            try
            {
                IsBusy = true;
                var us = await _servicioDatos.ValidarUsuario(_usuario);
                if (us != null)
                {
                    Session.User = us;
                    //DependencyService.Get<IServicioFicheros>().GuardarTexto("Datos-del-usuario-" + Session.User.Email, "AppSettings.xml");
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

                    await _navigator.PushAsync<HomeViewModel>(o =>
                    {
                        o.Titulo = "Bienvenido " + Session.User.Nombre;
                        o.Grupos = new ObservableCollection<GrupoVm>(oc);
                    }
                    );
                }
                else
                {
                    await _page.MostrarAlerta("Imposible iniciar sesión", "Email o contraseña incorrectos", "OK");
                }
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