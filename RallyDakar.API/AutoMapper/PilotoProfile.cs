using AutoMapper;
using RallyDakar.API.Modelo;
using RallyDakar.Dominio.Entidades;

namespace RallyDakar.API.AutoMapper
{
    public class PilotoProfile: Profile
    {
        public PilotoProfile()
        {
            CreateMap<Piloto, PilotoModelo>();
            CreateMap<PilotoModelo, Piloto>();
        }
    }
}
