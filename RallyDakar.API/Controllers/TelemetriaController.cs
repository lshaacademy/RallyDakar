using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<TelemetriaController> _logger;
        public TelemetriaController(ITelemetriaRepositorio telemetriaRepositorio, IMapper mapper, ILogger<TelemetriaController> logger)
        {
            _telemetriaRepositorio = telemetriaRepositorio;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<TelemetriaModelo> Obter(int equipeId)
        {
            
        }
    }
}
