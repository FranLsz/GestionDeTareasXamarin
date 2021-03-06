﻿using System;
using System.Windows.Input;
using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using DataModel.ViewModel;
using MvvmLibrary.Factorias;
using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using RestSharp.Portable.Content;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.ViewModel.Tarea
{
    public class NuevaTareaViewModel : GeneralViewModel
    {
        public ICommand CmdAgregar { get; set; }
        public ICommand CmdAgregarUbicacion { get; set; }
        public ICommand CmdAgregarImagen { get; set; }

        public string TituloLbl => "Titulo";
        public string DescripcionLbl => "Descripción";
        public string FechaLbl => "Fecha";
        public string AgregarLbl { get; set; }
        public string AgregarImagenLbl => "Agregar imagen";
        public string AgregarDocumentoLbl => "Agregar documento";
        public string AgregarUbicacionLbl => "Agregar ubicación";


        private string _latitud;
        public string Latitud
        {
            get { return _latitud; }
            set { SetProperty(ref _latitud, value); }
        }

        private string _longitud;
        public string Longitud
        {
            get { return _longitud; }
            set { SetProperty(ref _longitud, value); }
        }

        private ImageSource _imagen;
        public ImageSource Imagen
        {
            get { return _imagen; }
            set { SetProperty(ref _imagen, value); }
        }


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
            CmdAgregarUbicacion = new Command(AgregarUbicacion);
            CmdAgregarImagen = new Command(AgregarImagen);
        }

        private async void AgregarImagen()
        {
            var f =
                await
                    CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                    {
                        Directory = "Imagenes",
                        Name = "tareaFoto.jpg"
                    });
            if (f != null)
            {
                var st = f.GetStream();
                f.Dispose();

                var l = st.Length;
                byte[] bt = new byte[1];
                st.Read(bt, 0, bt.Length);

                var fichero = new FicheroModel
                {
                    Id = 22,
                    Nombre = Convert.ToBase64String(bt)
                };

                await _servicioDatos.UploadFichero(fichero);

                Imagen = ImageSource.FromStream(() => st);
            }
        }

        private async void AgregarUbicacion()
        {
            try
            {
                var loc = CrossGeolocator.Current;
                var pos = await loc.GetPositionAsync();

                Latitud = pos.Latitude.ToString();
                Longitud = pos.Longitude.ToString();
            }
            catch (Exception e)
            {
                await _page.MostrarAlerta("", e.Message, "Ok");
            }

        }

        private async void Agregar()
        {
            try
            {
                IsBusy = true;
                if (!Editing)
                {
                    Tarea.IdGrupo = Grupo.Id;
                    Tarea.Ubicacion = string.Empty;

                    var tarea = await _servicioDatos.AddTarea(Tarea);
                    if (tarea != null)
                    {
                        await _page.MostrarAlerta("", "Tarea creada correctamente", "Ok");
                        await _navigator.PopAsync();
                    }
                }
                else
                {
                    var res = await _servicioDatos.UpdateTarea(Tarea);
                    if (res)
                    {
                        await _page.MostrarAlerta("", "Tarea actualizada correctamente", "Ok");
                        await _navigator.PopAsync();
                    }
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