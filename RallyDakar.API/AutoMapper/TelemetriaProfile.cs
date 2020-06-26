using AutoMapper;
using RallyDakar.API.Modelo;
using RallyDakar.Dominio.Entidades;

namespace RallyDakar.API.AutoMapper
{
    public class TelemetriaProfile: Profile
    {
        public TelemetriaProfile()
        {
            CreateMap<Telemetria, TelemetriaModelo>();
            CreateMap<TelemetriaModelo, Telemetria>();
        }
    }
}
