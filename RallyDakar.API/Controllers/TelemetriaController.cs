using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RallyDakar.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Controllers
{
    
    [ApiController]
    [Route("api/telemetria")]
    public class TelemetriaController : ControllerBase
    {
        private readonly ITelemetriaRepositorio _telemetriaRepositorio;
        private readonly IMapper _mapper;
        public TelemetriaController(ITelemetriaRepositorio telemetriaRepositorio, IMapper mapper)
        {
            _telemetriaRepositorio = telemetriaRepositorio;
            _mapper = mapper;
        }
    }
}
