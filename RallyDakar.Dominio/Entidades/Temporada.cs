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

        public virtual ICollection<Equipe> Equipes { get; set; }

        public Temporada()
        {
            Equipes = new List<Equipe>();
        }

        public void AdicionarEquipe(Equipe equipe)
        {
            //pré-condições
            if (equipe != null)
            {
                if(equipe.Validado()){
                    Equipes.Add(equipe);                    
                }
            }
        }

        

    }
}
