using System;
using System.ComponentModel;

namespace MvvmLibrary.ViewModel.Base
{
    public interface IViewModel : INotifyPropertyChanged
    {
        string Titulo { get; set; }

        // se utiliza para cambiar el estado del ViewModel
        void SetState<T>(Action<T> action) where T : class, IViewModel;
    }
}