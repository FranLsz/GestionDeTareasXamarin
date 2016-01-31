using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using DataModel.ViewModel;
using MvvmLibrary.Factorias;
using RedContactos.ViewModel;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.ViewModel
{
    public class LoginViewModel : GeneralViewModel
    {
        // COMMANDS
        public ICommand cmdLogin { get; set; }
        public ICommand cmdRegistro { get; set; }

        // PROPERTIES
        public string IniciarLabel { get { return "Iniciar sesión"; } }
        public string RegistroLabel { get { return "Registro"; } }
        public string TitulEmail { get { return "Email"; } }
        public string TituloPassword { get { return "Contraseña"; } }

        private UsuarioModel _usuario;
        public UsuarioModel Usuario
        {
            get { return _usuario; }
            set { SetProperty(ref _usuario, value); }
        }

        // PAGE
        public Page Page;

        // CTOR
        public LoginViewModel(INavigator navigator, IServicioDatos servicio, Session session, IPage page) : base(navigator, servicio, session, page)
        {
            Page = new Page();
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
                var us = await _servicio.ValidarUsuario(_usuario);
                if (us != null)
                {
                    Session.User = us;
                    await _navigator.PushAsync<HomeViewModel>(o =>
                    {
                        o.Titulo = "Bienvenido " + Session.User.Nombre;
                    }
                    );
                }
                else
                {
                    await Page.DisplayAlert("Imposible iniciar sesión", "Email o contraseña incorrectos", "OK");
                }
            }
            catch (Exception e)
            {
                await Page.DisplayAlert("Error", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}