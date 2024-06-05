using Api.Models;
using Api.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimaisController : ControllerBase
    {
        private readonly IAnimaisRepositorio _animaisRepositorio;

        public AnimaisController(IAnimaisRepositorio animaisRepositorio)
        {
            _animaisRepositorio = animaisRepositorio;
        }

        [HttpGet("GetAllAnimais")]
        public async Task<ActionResult<List<AnimaisModel>>> GetAllAnimais()
        {
            List<AnimaisModel> animais = await _animaisRepositorio.GetAll();
            return Ok(animais);
        }

        [HttpGet("GetAnimalId/{id}")]
        public async Task<ActionResult<AnimaisModel>> GetAnimalId(int id)
        {
            AnimaisModel animal = await _animaisRepositorio.GetById(id);
            return Ok(animal);
        }

        [HttpPost("InsertAnimais")]
        public async Task<ActionResult<AnimaisModel>> InsertAnimais([FromBody] AnimaisModel animaisModel)
        {
            AnimaisModel animal = await _animaisRepositorio.InsertAnimais(animaisModel);
            return Ok(animal);
        }

        [HttpPut("UpdateAnimais/{id:int}")]
        public async Task<ActionResult<AnimaisModel>> UpdateAnimais(int id, [FromBody] AnimaisModel animaisModel)
        {
            animaisModel.AnimalId = id;
            AnimaisModel animal = await _animaisRepositorio.UpdateAnimais(animaisModel, id);
            return Ok(animal);
        }

        [HttpDelete("DeleteAnimais/{id:int}")]
        public async Task<ActionResult<AnimaisModel>> DeleteAnimais(int id)
        {
            bool deleted = await _animaisRepositorio.DeleteAnimais(id);
            return Ok(deleted);
        }

    }
}
