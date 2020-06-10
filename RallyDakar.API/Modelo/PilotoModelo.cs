using System.ComponentModel.DataAnnotations;

namespace RallyDakar.API.Modelo
{
    public class PilotoModelo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é de preenchimento Obrigatório")]
        [MinLength(5,ErrorMessage ="Nome deve ter no mínimo 5 caracteres")]
        [MaxLength(50,ErrorMessage = "Nome não pode ter mais do que 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "SobreNome é de preenchimento Obrigatório")]
        [MinLength(5, ErrorMessage = "SobreNome deve ter no mínimo 5 caracteres")]
        [MaxLength(50, ErrorMessage = "SobreNome não pode ter mais do que 50 caracteres")]
        public string SobreNome { get; set; }

        public int EquipeId { get; set; }

        //public string NomeCompleto { 
        //    get { return Nome + " " + SobreNome; } 
        //}

        public string NomeCompleto
        {
            get { return $"{Nome} {SobreNome}"; }
        }

    }
}
