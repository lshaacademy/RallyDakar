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

      
        [HttpGet("{id}", Name ="Obter")]
        public IActionResult Obter(int id)
        {
            try
            {
                var piloto = _pilotoRepositorio.Obter(id);
                if (piloto == null)
                    return NotFound();

                return Ok(piloto);

            }catch (Exception ex)
            {
                //_logger.Info(ex.toString());                
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }
        
        [HttpPost]
        public IActionResult AdicionarPiloto([FromBody]Piloto piloto)
        {
            try
            {
                if (_pilotoRepositorio.Existe(piloto.Id))
                    return StatusCode(409,"Já existe piloto com a mesma identificação ");

                _pilotoRepositorio.Adicionar(piloto);

                return CreatedAtRoute("Obter", new { id = piloto.Id }, piloto);

            }catch(Exception ex)
            {
                //_logger.info(ex.ToString())
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody]Piloto piloto)
        {
            try
            {
                if (!_pilotoRepositorio.Existe(piloto.Id))
                    return NotFound();
                
                _pilotoRepositorio.Atualizar(piloto);

                return NoContent();

            }catch(Exception ex)
            {
                //_logger.info(ex.ToString())
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }

        [HttpPatch]
        public IActionResult AtualizarParcialmente([FromBody]Piloto piloto)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.info(ex.ToString())
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                //_logger.info(ex.ToString())
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }
    }
}
