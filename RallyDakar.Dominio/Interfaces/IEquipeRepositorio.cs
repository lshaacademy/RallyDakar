using RallyDakar.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Dominio.Interfaces
{
    public interface IEquipeRepositorio
    {
        void Adicionar(Equipe equipe);
        IEnumerable<Equipe> ObterTodos();
        Equipe Obter(int equipeId);
        bool Existe(int equipeId);
        void Atualizar(Equipe equipeId);
        void Deletar(Equipe equipeId);        
    }
}
