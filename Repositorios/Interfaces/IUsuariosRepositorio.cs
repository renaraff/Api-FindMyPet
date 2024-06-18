using Api.Models;

namespace Api.Repositorios.Interfaces
{
    public interface IUsuariosRepositorio
    {
        Task<List<UsuariosModel>> GetAll();

        Task<UsuariosModel> GetById(int id);

        Task<UsuariosModel> InsertUser(UsuariosModel user);

        Task<UsuariosModel> UpdateUser(UsuariosModel user, int id);

        Task<bool> DeleteUser(int id);

        Task<UsuariosModel> Login(string email, string senha);
    }
}
