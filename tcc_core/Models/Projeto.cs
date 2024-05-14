using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tcc_core.Models
{
    public class Projeto
    {
        [Key]
        public int Id { get; set; }
        public string Coordenador { get; set; } = string.Empty;
        public string Natureza { get; set; } = string.Empty;
        public DateTime DtInicio { get; set; }
        public DateTime? DtFinal { get; set; }

        public ICollection<Agendamento> Agendamento { get; set; } = new List<Agendamento>();
        public ICollection<Movimentacao> Movimentacao { get; set; } = new List<Movimentacao>();
    }

}
