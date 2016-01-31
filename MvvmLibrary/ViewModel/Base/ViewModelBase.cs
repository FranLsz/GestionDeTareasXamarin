using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmLibrary.ViewModel.Base
{
    public class ViewModelBase : IViewModel
    {
        private bool _isBusy;
        private double _opacity;
        private bool _enabled;

        public ViewModelBase()
        {
            Opacity = 1;
            Enabled = true;
        }

        // cada vez que una propiedad cambia de valor se lanza el evento
        public event PropertyChangedEventHandler PropertyChanged;

        public string Titulo { get; set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                Opacity = value ? 0.5 : 1;
                Enabled = !value;
                SetProperty(ref _isBusy, value);
            }
        }

        public double Opacity
        {
            get { return _opacity; }
            set { SetProperty(ref _opacity, value); }
        }

        public bool Enabled
        {
            get { return _enabled; }

            set
            { SetProperty(ref _enabled, value); }
        }

        // CallerMemberName es el nombre del origen de la llamada
        protected virtual bool SetProperty<T>(ref T variable, T valor, [CallerMemberName] string nombre = null)
        {
            // si tienen el mismo valor return false
            if (object.Equals(variable, valor))
                return false;
            variable = valor;
            OnPropertyChanged(nombre);
            return true;

        }

        protected void OnPropertyChanged([CallerMemberName]string nombre = null)
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        public void SetState<T>(Action<T> action) where T : class, IViewModel
        {
            action?.Invoke(this as T);
        }
    }
}