using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    [Route("api/equipes/{equipeId}/telemetria")]
    public class TelemetriaController : ControllerBase
    {
        private readonly ITelemetriaRepositorio _telemetriaRepositorio;
        private readonly IEquipeRepositorio _equipeRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<TelemetriaController> _logger;
        public TelemetriaController(ITelemetriaRepositorio telemetriaRepositorio, IMapper mapper,
            ILogger<TelemetriaController> logger,
            IEquipeRepositorio equipeRepositorio)
        {
            _telemetriaRepositorio = telemetriaRepositorio;
            _mapper = mapper;
            _logger = logger;
            _equipeRepositorio = equipeRepositorio;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TelemetriaModelo>> Obter(int equipeId)
        {
            try
            {
                _logger.LogInformation($"Verificando se Equipe: {equipeId} existe na base");
                if (!_equipeRepositorio.Existe(equipeId))
                {
                    _logger.LogWarning($"Equipe id não foi identificad - Equipeid: {equipeId}");
                    return NotFound();
                }

                _logger.LogInformation($"Obtendo os dadas da telemetria para a equipe: {equipeId}");
                var dadosTelemetria = _telemetriaRepositorio.ObterTodosPorEquipe(equipeId);

                if (!dadosTelemetria.Any())
                {
                    _logger.LogInformation($"Não foi retornado dados de telemetria para a equipe informada: {equipeId}");                    
                    return NotFound("Não foi retornado dados de telemetria para a equipe informada");
                }
                   
                var dadosTelemetriaModelo = _mapper.Map<IEnumerable<TelemetriaModelo>>(dadosTelemetria);

                return Ok(dadosTelemetriaModelo);
                                              
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex.ToString()}");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }

        [HttpGet("{telemetriaId}",Name = "ObterTelemetria")]
        public ActionResult<TelemetriaModelo> ObterTelemetria(int equipeId, int telemetriaId)
        {
            try
            {
                _logger.LogInformation($"Verificando se Equipe: {equipeId} existe na base");
                if (!_equipeRepositorio.Existe(equipeId))
                {
                    _logger.LogWarning($"Equipe id não foi identificada - Equipeid: {equipeId}");
                    return NotFound();
                }

                _logger.LogInformation($"Verificando se telemetria: {telemetriaId} existe na base");
                if (!_telemetriaRepositorio.Existe(telemetriaId))
                {
                    _logger.LogWarning($"Telemetria id não foi identificado - TelemetriaId: {telemetriaId}");
                    return NotFound();
                }

                _logger.LogWarning($"Obter Telemetria Por Equipeid: {equipeId} e TelemetriaId{telemetriaId}");
                var telemetria = _telemetriaRepositorio.ObterPor(equipeId, telemetriaId);
                if(telemetria == null)
                {
                    _logger.LogWarning($"Telemetria não identificada - TelemetriaId: {telemetriaId}");
                    _logger.LogWarning($"Equipeid: {equipeId}");
                    return NotFound("Telemetria não identificada");
                }

                var telemetriaModelo = _mapper.Map<TelemetriaModelo>(telemetria);

                return Ok(telemetriaModelo);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex.ToString()}");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }

        [HttpPost]
        public ActionResult<TelemetriaModelo> Adicionar(int equipeId, TelemetriaModelo telemetriaModelo)
        {
            try
            {
                _logger.LogInformation($"Verificando se Equipe: {equipeId} existe na base");
                if (!_equipeRepositorio.Existe(equipeId))
                {
                    _logger.LogWarning($"Equipe id não foi identificada - Equipeid: {equipeId}");
                    return NotFound();
                }
                
                var telemetria = _mapper.Map<Telemetria>(telemetriaModelo);

                _logger.LogInformation($"Adicionando novo dado telemetria para o a equipe:{equipeId}");
                _telemetriaRepositorio.Adicionar(telemetria);
                return CreatedAtRoute("ObterTelemetria", new { equipeId = equipeId, telemetriaId = telemetriaModelo.Id }, telemetriaModelo);


            }
            catch(Exception ex)
            {
                _logger.LogError($"Erro: {ex.ToString()}");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }

       [HttpPut]
       public ActionResult<TelemetriaModelo> Atualizar(int equipeId, TelemetriaModelo telemetriaModelo)
        {
            try
            {
                _logger.LogInformation($"Verificando se Equipe: {equipeId} existe na base");
                if (!_equipeRepositorio.Existe(equipeId))
                {
                    _logger.LogWarning($"Equipe id não foi identificad - Equipeid: {equipeId}");
                    return NotFound();
                }

                var telemetria = _mapper.Map<Telemetria>(telemetriaModelo);

                _logger.LogInformation($"Atualizando a base de dados com o telemetria: {telemetria.Id}");
                _telemetriaRepositorio.Atualizar(telemetria);

                _logger.LogInformation($"Finalizada Operação");
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Erro: {ex.ToString()}");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }

       [HttpPatch("{telemetriaId}")]
       public ActionResult AtualizarParcialmente(int equipeId, int telemetriaId, JsonPatchDocument<TelemetriaModelo> patchDocument)
        {
            try
            {
                _logger.LogInformation($"Verificando se Equipe: {equipeId} existe na base");
                if (!_equipeRepositorio.Existe(equipeId))
                {
                    _logger.LogWarning($"Equipe id não foi identificada - Equipeid: {equipeId}");
                    return NotFound();
                }

                _logger.LogInformation($"Obter telemetria por equipe:{equipeId} e telemetriaId:{telemetriaId}");
                var telemetria = _telemetriaRepositorio.ObterPor(equipeId, telemetriaId);

                var telemetriaModelo = _mapper.Map<TelemetriaModelo>(telemetria);

                _logger.LogInformation("Aplicando atualização parcial");                
                patchDocument.ApplyTo(telemetriaModelo);

                _logger.LogInformation("Mapeando telemetriaModelo ja atualizado para instância telemetria");
                telemetria = _mapper.Map(telemetriaModelo, telemetria);

                _logger.LogInformation($"Atualizando telemetria na base{telemetriaId}");
                _telemetriaRepositorio.Atualizar(telemetria);

                _logger.LogInformation($"Operação finalizada");

                return NoContent();

            }
            catch(Exception ex)
            {
                _logger.LogError($"Erro: {ex.ToString()}");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
            
        }
       [HttpDelete("{telemetriaId}")]
       public ActionResult Deletar(int equipeId, int telemetriaId)
        {
            try
            {
                _logger.LogInformation($"Verificando se Equipe: {equipeId} existe na base");
                if (!_equipeRepositorio.Existe(equipeId))
                {
                    _logger.LogWarning($"Equipe id não foi identificada - Equipeid: {equipeId}");
                    return NotFound();
                }

                _logger.LogInformation($"Verificando se Telemetria: {telemetriaId} existe na base");
                if (!_telemetriaRepositorio.Existe(telemetriaId))
                {
                    _logger.LogWarning($"Telemetria id não foi identificada - Telemetria: {telemetriaId}");
                    return NotFound();
                }

                _logger.LogWarning($"Obter telemetria: {telemetriaId} da base");
                var telemetria = _telemetriaRepositorio.ObterPor(equipeId, telemetriaId);

                _logger.LogWarning($"Deletar telemetria: {telemetriaId} da base");
                _telemetriaRepositorio.Deletar(telemetria);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Erro: {ex.ToString()}");
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte");
            }
        }

        [HttpOptions]
        public IActionResult ListarOperacoesPermitidas()
        {            
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
            return Ok();
        }
    }
}
