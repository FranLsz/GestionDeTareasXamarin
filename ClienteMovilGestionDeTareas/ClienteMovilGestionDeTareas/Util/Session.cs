using System.Collections.Generic;
using DataModel.ViewModel;

namespace ClienteMovilGestionDeTareas.Util
{
    public class Session
    {
        public static UsuarioModel User
        {
            get { return (UsuarioModel)_session["User"]; }
            set { _session["User"] = value; }
        }

        private static Dictionary<string, object> _session = new Dictionary<string, object>();

        public object this[string index]
        {
            get { return _session[index]; }
            set { _session[index] = value; }
        }
    }
}