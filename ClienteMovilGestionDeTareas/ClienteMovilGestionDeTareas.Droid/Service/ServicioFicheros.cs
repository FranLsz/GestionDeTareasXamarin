using System;
using System.IO;
using ClienteMovilGestionDeTareas.Service;
using RedContactos.Droid.Service;
using Xamarin.Forms;
using Environment = System.Environment;
using Path = System.IO.Path;

[assembly: Dependency(typeof(ServicioFicheros))]
namespace RedContactos.Droid.Service
{
    public class ServicioFicheros : IServicioFicheros
    {
        public void GuardarTexto(string texto, string fichero)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var ruta = Path.Combine(path, fichero);
            //File.Create(ruta).Dispose(); ;
            File.WriteAllText(ruta, texto);

            /*StreamWriter stream = File.CreateText(ruta);
            stream.Write(texto);
            stream.Close();*/
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