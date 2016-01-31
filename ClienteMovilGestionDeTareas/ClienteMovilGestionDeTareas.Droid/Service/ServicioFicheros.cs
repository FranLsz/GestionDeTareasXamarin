using System;
using System.IO;
using ClienteMovilGestionDeTareas.Droid.Service;
using ClienteMovilGestionDeTareas.Service;
using Xamarin.Forms;
using Environment = System.Environment;
using Path = System.IO.Path;

[assembly: Dependency(typeof(ServicioFicheros))]
namespace ClienteMovilGestionDeTareas.Droid.Service
{
    public class ServicioFicheros : IServicioFicheros
    {
        public void GuardarTexto(string texto, string fichero)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var ruta = Path.Combine(path, fichero);
            File.WriteAllText(ruta, texto);
        }

        public string RecuperarTexto(string fichero)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var ruta = Path.Combine(path, fichero);
            try
            {
                return File.ReadAllText(ruta);
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}