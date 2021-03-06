﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ClienteMovilGestionDeTareas.Model;
using ClienteMovilGestionDeTareas.Service;
using ClienteMovilGestionDeTareas.Util;
using DataModel.ViewModel;
using MvvmLibrary.Factorias;
using Xamarin.Forms;

namespace ClienteMovilGestionDeTareas.ViewModel.Tarea
{
    public class ListadoTareasViewModel : GeneralViewModel
    {
        public ICommand CmdNuevaTarea { get; set; }

        public string MisTareasLbl => "Mis tareas";
        public string NuevaTareaLbl => "Nueva tarea";

        private ObservableCollection<TareaVm> _tareas;
        public ObservableCollection<TareaVm> Tareas
        {
            get
            {
                return _tareas;
            }
            set { SetProperty(ref _tareas, value); }
        }

        private TareaVm _tareaSeleccionada;
        public TareaVm TareaSeleccionada
        {
            get { return _tareaSeleccionada; }
            set
            {
                VerDetalleTarea(value);
                _tareaSeleccionada = null;
                SetProperty(ref value, null);
            }
        }

        public GrupoModel Grupo { get; set; }

        public ListadoTareasViewModel(INavigator navigator, IServicioDatos servicioDatos, Session session, IPage page) : base(navigator, servicioDatos, session, page)
        {
            CmdNuevaTarea = new Command(NuevaTarea);
        }

        private async void NuevaTarea()
        {
            await _navigator.PushAsync<NuevaTareaViewModel>(vm =>
            {
                vm.Titulo = "Nueva tarea para " + Titulo;
                vm.Grupo = Grupo;
                vm.AgregarLbl = "Crear tarea";
            });
        }

        private async void VerDetalleTarea(TareaVm model)
        {
            await _navigator.PushAsync<DetalleTareaViewModel>(vm =>
            {
                vm.Titulo = "Detalles de la tarea";
                vm.Tarea = model.TareaModel;
            });
        }

        public async Task BorrarTarea(int id)
        {
            try
            {
                IsBusy = true;
                var res = await _servicioDatos.DeleteTarea(id);

                if (res)
                {
                    Tareas.Remove(Tareas.First(o => o.TareaModel.Id == id));
                    await _page.MostrarAlerta("", "Tarea eliminada correctamente", "Ok");
                }
                else
                    await _page.MostrarAlerta("", "La tarea no pudo ser borrada", "Ok");
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

        public async Task EditarTarea(TareaModel model)
        {
            await _navigator.PushAsync<NuevaTareaViewModel>(vm =>
            {
                vm.Titulo = "Editar tarea de " + Titulo;
                vm.AgregarLbl = "Guardar cambios";
                vm.Tarea = model;
                vm.Editing = true;
            });
        }

        public async Task FinalizarTarea(TareaModel model)
        {
            try
            {
                IsBusy = true;
                model.Finalizada = true;
                var res = await _servicioDatos.UpdateTarea(model);

                if (res)
                {
                    await _page.MostrarAlerta("", "Tarea finalizada", "Ok");
                }
                else
                    await _page.MostrarAlerta("", "La tarea no pudo ser finalizada", "Ok");
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