

using System;
using System.Collections.Generic;
using System.Linq;

namespace RallyDakar.Dominio.Entidades
{
    public class Equipe
    {
        public int Id { get; set; }
        public string CodigoIdentificador { get; set; }
        public string Nome { get; set; }

        public int TemporadaId { get; set; }

        public virtual Temporada Temporada { get; set; }

        public ICollection<Piloto> Pilotos { get; set; }

        public bool Validado()
        {
            if (string.IsNullOrEmpty(Nome))
                return false;

            if (string.IsNullOrEmpty(CodigoIdentificador))
                return false;

            return true;
        }

        public Equipe()
        {
            Pilotos = new List<Piloto>();
        }
        public void AdicionarPiloto(Piloto piloto)
        {
            if (piloto != null && piloto.Validado())
            {
                if (!Pilotos.Any(p => p.Id == piloto.Id))
                    Pilotos.Add(piloto);
            }
        }

        public Piloto ObterPorId(int id)
        {
            return Pilotos.FirstOrDefault(p => p.Id == id);
        }
    }
}
