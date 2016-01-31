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

        #region Tareas

        Task<GrupoModel> AddGrupo(GrupoModel model);
        Task<ICollection<GrupoModel>> GetGrupos(int userId);
        Task<bool> DeleteGrupo(int id);

        #endregion

        #region Tareas

        Task<TareaModel> AddTarea(TareaModel model);
        Task<ICollection<TareaModel>> GetTareas(int grupoId);
        Task<bool> UpdateTarea(TareaModel model);
        Task<bool> DeleteTarea(int id);

        #endregion
    }
}