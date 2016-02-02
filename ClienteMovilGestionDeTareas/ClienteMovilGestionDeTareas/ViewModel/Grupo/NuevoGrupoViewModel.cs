using System;
using System.Windows.Input;
using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using DataModel.ViewModel;
using MvvmLibrary.Factorias;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.ViewModel.Grupo
{
    public class NuevoGrupoViewModel : GeneralViewModel
    {
        public ICommand CmdAgregar { get; set; }
        public ICommand CmdAgregarImagen { get; set; }

        public string NombreLbl => "Nombre";
        public string ImagenLbl => "Imagen";
        public string AgregarLbl => "Añadir grupo";
        public string AgregarImagenLbl => "Agregar imagen";

        private GrupoModel _grupo;
        public GrupoModel Grupo
        {
            get { return _grupo; }
            set { SetProperty(ref _grupo, value); }
        }

        public NuevoGrupoViewModel(INavigator navigator, IServicioDatos servicioDatos, Session session, IPage page) : base(navigator, servicioDatos, session, page)
        {
            _grupo = new GrupoModel();
            CmdAgregar = new Command(Agregar);
            CmdAgregarImagen = new Command(AgregarImagen);
            MessagingCenter.Send(this, "Hola");
        }

        private void AgregarImagen()
        {
            /*OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".txt"; // Required file extension 
            fileDialog.Filter = "Text documents (.txt)|*.txt"; // Optional file extensions
            fileDialog.ShowDialog();*/
        }

        private async void Agregar()
        {
            try
            {
                IsBusy = true;
                Grupo.IdUsuario = Session.User.Id;
                //Tarea.Imagen = string.Empty;

                var grupo = await _servicioDatos.AddGrupo(Grupo);

                if (grupo != null)
                {
                    MessagingCenter.Send(grupo, "AddGrupo");
                    await _page.MostrarAlerta("", "Grupo creado correctamente", "Ok");
                    await _navigator.PopAsync();
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