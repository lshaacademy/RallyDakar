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
            try
            {
                var pilotos = _pilotoRepositorio.ObterTodos();
                if (!pilotos.Any())
                    return NotFound();

                return Ok(pilotos);
            }
            catch(Exception ex)
            {
                //return BadRequest(ex.ToString());
                //_logger.Info(ex.toString());
                //return BadRequest("Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }

        
        [HttpPost]
        public IActionResult AdicionarPiloto([FromBody]Piloto piloto)
        {
            return Ok();
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
