using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClienteMovilGestionDeTareas.Util;
using DataModel.ViewModel;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace ClienteMovilGestionDeTareas.Service
{
    public class ServicioDatos : IServicioDatos
    {
        private readonly RestClient _client;

        public ServicioDatos()
        {
            _client = new RestClient(Cadenas.UrlServicio);
        }

        #region Usuario

        public async Task<UsuarioModel> ValidarUsuario(UsuarioModel model)
        {
            var request = new RestRequest("Usuario") { Method = Method.GET };
            request.AddQueryParameter("email", model.Email);
            request.AddQueryParameter("password", model.Password);

            // la api devuelve error 404 si no existe, y restsharp peta
            var response = await _client.Execute<UsuarioModel>(request);
            if (response.IsSuccess)
                return response.Data;

            return null;

        }

        public async Task<bool> CheckUsuario(string email)
        {
            var request = new RestRequest("Usuario") { Method = Method.GET };
            request.AddQueryParameter("email", email);

            var response = await _client.Execute<bool>(request);

            if (response.IsSuccess)
                return response.Data;
            return false;
        }

        public async Task<UsuarioModel> AddUsuario(UsuarioModel model)
        {
            var request = new RestRequest("Usuario")
            {
                Method = Method.POST
            };
            request.AddJsonBody(model);
            var response = await _client.Execute<UsuarioModel>(request);

            if (response.IsSuccess)
                return response.Data;
            return null;
        }

        public async Task<ICollection<UsuarioModel>> GetUsuarios()
        {
            var request = new RestRequest("Usuario") { Method = Method.GET };

            var response = await _client.Execute<ICollection<UsuarioModel>>(request);
            if (response.IsSuccess)
                return response.Data;
            return null;
        }

        public async Task<UsuarioModel> GetUsuario(int id)
        {
            var request = new RestRequest("Usuario") { Method = Method.GET };
            request.AddQueryParameter("id", id);

            var response = await _client.Execute<UsuarioModel>(request);
            if (response.IsSuccess)
                return response.Data;

            return null;
        }
        #endregion


        #region Grupo

        public async Task<GrupoModel> AddGrupo(GrupoModel model)
        {
            var request = new RestRequest("Grupo")
            {
                Method = Method.POST
            };
            request.AddJsonBody(model);
            var response = await _client.Execute<GrupoModel>(request);

            if (response.IsSuccess)
                return response.Data;
            return null;
        }


        public async Task<ICollection<GrupoModel>> GetGrupos(int userId)
        {
            var request = new RestRequest("Grupo") { Method = Method.GET };
            request.AddQueryParameter("userId", userId);

            var response = await _client.Execute<ICollection<GrupoModel>>(request);
            if (response.IsSuccess)
                return response.Data;
            return null;
        }

        public async Task<bool> DeleteGrupo(int id)
        {
            var request = new RestRequest("Grupo") { Method = Method.DELETE };
            request.AddQueryParameter("id", id);

            var response = await _client.Execute(request);
            if (response.IsSuccess)
                return true;

            return false;
        }

        #endregion


        #region Tarea

        public async Task<TareaModel> AddTarea(TareaModel model)
        {
            var request = new RestRequest("Tarea")
            {
                Method = Method.POST
            };
            request.AddJsonBody(model);
            var response = await _client.Execute<TareaModel>(request);

            if (response.IsSuccess)
                return response.Data;
            return null;
        }

        public async Task<ICollection<TareaModel>> GetTareas(int grupoId)
        {
            var request = new RestRequest("Tarea") { Method = Method.GET };
            request.AddQueryParameter("grupoId", grupoId);

            var response = await _client.Execute<ICollection<TareaModel>>(request);
            if (response.IsSuccess)
                return response.Data;

            return null;
        }

        public async Task<bool> UpdateTarea(TareaModel model)
        {
            var request = new RestRequest("Tarea") { Method = Method.PUT };
            request.AddQueryParameter("id", model.Id);
            request.AddJsonBody(model);

            try
            {
                var response = await _client.Execute(request);
                if (response.IsSuccess)
                    return true;
            }
            catch (Exception ex)
            {

                var r = ex.Message;
            }

            return false;
        }

        public async Task<bool> DeleteTarea(int id)
        {
            var request = new RestRequest("Tarea") { Method = Method.DELETE };
            request.AddQueryParameter("id", id);

            var response = await _client.Execute(request);
            if (response.IsSuccess)
                return true;

            return false;
        }

        #endregion



    }
}