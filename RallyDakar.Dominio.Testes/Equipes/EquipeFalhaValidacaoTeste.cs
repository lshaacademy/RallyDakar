using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyDakar.Dominio.Entidades;
using System.Linq;

namespace RallyDakar.Dominio.Testes.Temporadas
{
    [TestClass]
    public class EquipeFalhaValidacaoTeste
    {        
        Equipe equipe1;
        

        [TestMethod]
        public void NaoValidaEquipeSemNomeInformado()
        {
            equipe1 = new Equipe()
            {                
                CodigoIdentificador = "KTM",
                Nome = ""
            };
            
            Assert.IsFalse(equipe1.Validado());
        }

        [TestMethod]
        public void NaoValidaEquipeSemCodigoIdentificador()
        {
            equipe1 = new Equipe()
            {                
                CodigoIdentificador = "",
                Nome = "Nometest"
            };

            Assert.IsFalse(equipe1.Validado());
        }
     
    }
}
