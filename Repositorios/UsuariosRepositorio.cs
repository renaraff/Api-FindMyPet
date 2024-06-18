using Api.Data;
using Api.Models;
using Api.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api.Repositorios
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly Contexto _dbContext;

        public UsuariosRepositorio(Contexto dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UsuariosModel>> GetAll()
        {
            return await _dbContext.Usuario.ToListAsync();
        }

        public async Task<UsuariosModel> GetById(int id)
        {
            return await _dbContext.Usuario.FirstOrDefaultAsync(x => x.UsuarioId == id);
        }

        public async Task<UsuariosModel> Login(string email, string senha)
        {
            return await _dbContext.Usuario.FirstOrDefaultAsync(x => x.UsuarioEmail == email && x.UsuarioSenha == senha);
        }
        public async Task<UsuariosModel> InsertUser(UsuariosModel user)
        {
            await _dbContext.Usuario.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UsuariosModel> UpdateUser(UsuariosModel user, int id)
        {
            UsuariosModel users = await GetById(id);
            if (users == null)
            {
                throw new Exception("Não encontrado.");
            }
            else
            {
                users.UsuarioNome = user.UsuarioNome;
                users.UsuarioTelefone = user.UsuarioTelefone;
                users.UsuarioEmail = user.UsuarioEmail;
                users.UsuarioSenha = user.UsuarioSenha;
                _dbContext.Usuario.Update(users);
                await _dbContext.SaveChangesAsync();
            }
            return users;

        }

        public async Task<bool> DeleteUser(int id)
        {
            UsuariosModel users = await GetById(id);
            if (users == null)
            {
                throw new Exception("Não encontrado.");
            }

            _dbContext.Usuario.Remove(users);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
