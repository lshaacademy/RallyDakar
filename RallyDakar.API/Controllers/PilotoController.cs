﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RallyDakar.API.Modelo;
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
        private readonly IPilotoRepositorio _pilotoRepositorio;
        private readonly IMapper _mapper;
        public PilotoController(IPilotoRepositorio pilotoRepositorio, IMapper mapper)
        {
            _pilotoRepositorio = pilotoRepositorio;
            _mapper = mapper;
        }

       
      
        [HttpGet("{id}", Name ="Obter")]
        public IActionResult Obter(int id)
        {
            try
            {
                var piloto = _pilotoRepositorio.Obter(id);
                
                if (piloto == null)
                    return NotFound();

                var pilotoModelo = _mapper.Map<PilotoModelo>(piloto);

                return Ok(pilotoModelo);

            }catch (Exception ex)
            {
                //_logger.Info(ex.toString());                
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }
        
        [HttpPost]
        public IActionResult AdicionarPiloto([FromBody]PilotoModelo pilotoModelo)
        {
            try
            {
                var piloto = _mapper.Map<Piloto>(pilotoModelo);
                

                if (_pilotoRepositorio.Existe(piloto.Id))
                    return StatusCode(409,"Já existe piloto com a mesma identificação ");

                _pilotoRepositorio.Adicionar(piloto);

                var pilotoModeloRetorno = _mapper.Map<PilotoModelo>(piloto);

                return CreatedAtRoute("Obter", new { id = piloto.Id }, pilotoModeloRetorno);

            }catch(Exception ex)
            {
                //_logger.info(ex.ToString())
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody]PilotoModelo pilotoModelo)
        {
            try
            {
                if (!_pilotoRepositorio.Existe(pilotoModelo.Id))
                    return NotFound();

                var piloto = _mapper.Map<Piloto>(pilotoModelo);

                _pilotoRepositorio.Atualizar(piloto);

                return NoContent();

            }catch(Exception ex)
            {
                //_logger.info(ex.ToString())
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarParcialmente(int id, [FromBody] JsonPatchDocument<PilotoModelo> patchPilotoModelo)
        {
            try
            {
                if (!_pilotoRepositorio.Existe(id))
                    return NotFound();

                var piloto = _pilotoRepositorio.Obter(id);

                var pilotoModelo = _mapper.Map<PilotoModelo>(piloto);


                patchPilotoModelo.ApplyTo(pilotoModelo);

                piloto = _mapper.Map(pilotoModelo, piloto);


                _pilotoRepositorio.Atualizar(piloto);

                return NoContent();

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
                
                var piloto = _pilotoRepositorio.Obter(id);
                if (piloto == null)
                    return NotFound();
                
                _pilotoRepositorio.Deletar(piloto);

                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.info(ex.ToString())
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }
        
        [HttpOptions]
        public IActionResult ListarOperacoesPermitidas()
        {
            //Response.Headers.Add("Operações permitidas", "");
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
            return Ok();
        }

    }
}
