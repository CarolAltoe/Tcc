using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tcc_core.Models.Enuns;

namespace tcc_core.Models
{
    public class Projeto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "O coordenador é obrigatório.")]
        public string Coordenador { get; set; } = string.Empty;
        [Required(ErrorMessage = "A natureza do projeto é obrigatória.")]
        public NaturezaProjeto Natureza { get; set; }
        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime DtInicio { get; set; }
        public DateTime? DtFinal { get; set; }

        public ICollection<Agendamento>? Agendamento { get; set; } = new List<Agendamento>();
        public ICollection<Movimentacao>? Movimentacao { get; set; } = new List<Movimentacao>();
    }

}
