using System;
using System.Collections.Generic;

namespace RallyDakar.Dominio.Entidades
{
    public class Temporada
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DateFim { get; set; }

        public ICollection<Equipe> Equipes { get; set; }

    }
}
