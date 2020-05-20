using Microsoft.AspNetCore.Mvc;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Controllers
{
    [ApiController]
    [Route("api/pilotos")]
    public class PilotoController : ControllerBase
    {
        IPilotoRepositorio _pilotoRepositorio;
        public PilotoController(IPilotoRepositorio pilotoRepositorio)
        {
            _pilotoRepositorio = pilotoRepositorio;
        }


        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(_pilotoRepositorio.ObterTodos());
        }

        [HttpGet("{id}", Name = "Obter")]
        public IActionResult Obter(int id)
        {
            var piloto = _pilotoRepositorio.Obter(id);
            if(piloto != null)
            {
                return NotFound();
            }

            return Ok(piloto);
        }

        [HttpPost]
        public IActionResult AdicionarPiloto([FromBody]Piloto piloto)
        {
            try
            {
                if (_pilotoRepositorio.Existe(piloto))
                {
                    return BadRequest("Já existe piloto com essa id cadastrada");
                }

                _pilotoRepositorio.Adicionar(piloto);

                return CreatedAtRoute("Obter", new { id = piloto.Id }, piloto);
            }catch(Exception ex)
            {
                //return StatusCode(500, "Ocorreu um erro interno. Por favor entre contato com o suporte");
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody]Piloto piloto)
        {
            
            return Ok();
        }

        [HttpPatch]
        public IActionResult AtualizarParcialmente([FromBody]Piloto piloto)
        {            
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            return Ok();
        }
    }
}
