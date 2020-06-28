using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RallyDakar.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Dominio.DbContexto
{
    public class BaseDados
    {
        public static void CargaInicial(IServiceProvider serviceProvider)
        {
            using(var context = new RallyDbContexto(serviceProvider.GetRequiredService<DbContextOptions<RallyDbContexto>>()))
            {
                var temporada = new Temporada();
                temporada.Id = 1;
                temporada.Nome = "Temporada2020";
                //temporada.DataInicio = DateTime.Now.AddDays(20); Inicia daqui a 20 dias
                temporada.DataInicio = DateTime.Now;

                var equipe = new Equipe();
                equipe.Id = 1;
                equipe.Nome = "Azul";
                equipe.CodigoIdentificador = "AZL";

                var pilotoPedro = new Piloto();
                pilotoPedro.Id = 1;
                pilotoPedro.Nome = "Pedro";


                var pilotoCarlos = new Piloto();
                pilotoCarlos.Id = 2;
                pilotoCarlos.Nome = "Carlos";

                equipe.AdicionarPiloto(pilotoPedro);
                equipe.AdicionarPiloto(pilotoCarlos);

                temporada.AdicionarEquipe(equipe);

                context.Temporadas.Add(temporada);
                context.SaveChanges();

                Telemetria telemetria = new Telemetria();
                telemetria.Id = 1;
                telemetria.EquipeId = equipe.Id;
                telemetria.Data = DateTime.Now;
                telemetria.DataServidor = DateTime.Now;

                context.Telemetria.Add(telemetria);
                context.SaveChanges();
                



            }
        }
    }
}
