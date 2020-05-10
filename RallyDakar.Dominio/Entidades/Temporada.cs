using System;
using System.Collections.Generic;
using System.Linq;

namespace RallyDakar.Dominio.Entidades
{
    public class Temporada
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DateFim { get; set; }

        public virtual ICollection<Equipe> Equipes { get; set; }

        public Temporada()
        {
            Equipes = new List<Equipe>();
        }

        public void AdicionarEquipe(Equipe equipe)
        {
            //pré-condições
            if (equipe != null && equipe.Validado())
            {
                if(!Equipes.Any(e => e.Id == equipe.Id)){
                    Equipes.Add(equipe);                    
                }
            }
        }

        public Equipe ObterPorId(int id)
        {
            return Equipes.FirstOrDefault(e => e.Id == id);
        }

        

    }
}
