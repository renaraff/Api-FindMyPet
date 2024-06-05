using Api.Data;
using Api.Models;
using Api.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api.Repositorios
{
    public class AnimaisRepositorio : IAnimaisRepositorio
    {
        private readonly Contexto _dbContext;

        public AnimaisRepositorio(Contexto dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AnimaisModel>> GetAll()
        {
            return await _dbContext.Animais.ToListAsync();
        }

        public async Task<AnimaisModel> GetById(int id)
        {
            return await _dbContext.Animais.FirstOrDefaultAsync(x => x.AnimalId == id);
        }

        public async Task<AnimaisModel> InsertAnimais(AnimaisModel animal)
        {
            await _dbContext.Animais.AddAsync(animal);
            await _dbContext.SaveChangesAsync();
            return animal;
        }

        public async Task<AnimaisModel> UpdateAnimais(AnimaisModel animal, int id)
        {
            AnimaisModel animais = await GetById(id);
            if (animais == null)
            {
                throw new Exception("Não encontrado.");
            }
            else
            {
                animais.AnimalNome = animal.AnimalNome;
                animais.AnimalRaca = animal.AnimalRaca;
                animais.AnimalTipo = animal.AnimalTipo;
                animais.AnimalCor = animal.AnimalCor;
                animais.AnimalSexo = animal.AnimalSexo;
                animais.AnimalObservacao = animal.AnimalObservacao;
                animais.AnimalFoto = animal.AnimalFoto;
                animais.AnimalDtDesaparecimento = animal.AnimalDtDesaparecimento;
                animais.AnimalDtEncontro = animal.AnimalDtEncontro;
                animais.AnimalStatus = animal.AnimalStatus;
                animais.UsuarioId = animal.UsuarioId;
                _dbContext.Animais.Update(animais);
                await _dbContext.SaveChangesAsync();
            }
            return animais;
        }

        public async Task<bool> DeleteAnimais(int id)
        {
            AnimaisModel animais = await GetById(id);
            if (animais == null)
            {
                throw new Exception("Não encontrado.");
            }

            _dbContext.Animais.Remove(animais);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
