using System.Collections.Generic;
using System.Threading.Tasks;
using DataModel.ViewModel;

namespace ClienteMovilGestionDeTareas.Service
{
    public interface IServicioDatos
    {
        #region Usuario

        Task<UsuarioModel> ValidarUsuario(UsuarioModel model);
        Task<bool> CheckUsuario(string email);
        Task<UsuarioModel> AddUsuario(UsuarioModel model);
        Task<ICollection<UsuarioModel>> GetUsuarios();
        Task<UsuarioModel> GetUsuario(int id);

        #endregion


    }
}