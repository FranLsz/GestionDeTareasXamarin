namespace ClienteMovilGestionDeTareas.Service
{
    public interface IServicioFicheros
    {
        void GuardarTexto(string texto, string fichero);
        string RecuperarTexto(string fichero);
    }
}