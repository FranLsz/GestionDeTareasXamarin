using System;
using System.Windows.Input;
using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using DataModel.ViewModel;
using MvvmLibrary.Factorias;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.ViewModel.Tarea
{
    public class NuevaTareaViewModel : GeneralViewModel
    {
        public ICommand CmdAgregar { get; set; }

        public string TituloLbl => "Titulo";
        public string DescripcionLbl => "Descripción";
        public string FechaLbl => "Fecha";
        public string AgregarLbl { get; set; }
        public string AgregarImagenLbl => "Agregar imagen";
        public string AgregarDocumentoLbl => "Agregar documento";
        public bool Editing { get; set; }

        private TareaModel _tarea;
        public TareaModel Tarea
        {
            get { return _tarea; }
            set { SetProperty(ref _tarea, value); }
        }

        public GrupoModel Grupo { get; set; }



        public NuevaTareaViewModel(INavigator navigator, IServicioDatos servicioDatos, Session session, IPage page) : base(navigator, servicioDatos, session, page)
        {
            _tarea = new TareaModel();
            CmdAgregar = new Command(Agregar);
        }

        private async void Agregar()
        {
            if (!Editing)
            {
                Tarea.IdGrupo = Grupo.Id;
                Tarea.Ubicacion = string.Empty;

                try
                {
                    var tarea = await _servicioDatos.AddTarea(Tarea);
                    if (tarea != null)
                    {
                        await _page.MostrarAlerta("", "Tarea creada correctamente", "Ok");
                        await _navigator.PopAsync();
                    }
                }
                catch (Exception) { }

            }
            else
            {
                try
                {
                    //var res = await _servicioDatos.UpdateTarea(Tarea);
                    var res = true;
                    if (res)
                    {
                        await _page.MostrarAlerta("", "Tarea actualizada correctamente", "Ok");
                        await _navigator.PopAsync();
                    }
                }
                catch (Exception) { }
            }
        }
    }
}