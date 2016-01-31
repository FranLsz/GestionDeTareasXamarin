using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MvvmLibrary.Factorias
{
    public class PageProxy : IPage
    {
        readonly Func<Page> _page;

        public PageProxy(Func<Page> page)
        {
            _page = page;
        }
        public INavigation Navigation { get { return _page().Navigation; } }
        public async Task MostrarAlerta(string titulo, string msg, string cancelar)
        {
            await _page().DisplayAlert(titulo, msg, cancelar);
        }

        public async Task<bool> MostrarAlerta(string titulo, string msg, string aceptar, string cancelar)
        {
            return await _page().DisplayAlert(titulo, msg, aceptar, cancelar);
        }

        public async Task<string> MostrarActionSheet(string titulo, string cancelar, string destruccion, params string[] botones)
        {
            return await _page().DisplayActionSheet(titulo, cancelar, destruccion, botones);
        }
    }
}