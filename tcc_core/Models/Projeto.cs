using System;
using System.Collections.Generic;

namespace tcc_core.Models
{
    public class Projeto
    {
        public int Id { get; set; }
        public string Coordenador { get; set; }
        public string Natureza { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime? DtFinal { get; set; }

        public ICollection<Agendamento> Agendamentos { get; set; }
        public ICollection<Movimentacao> Movimentacoes { get; set; }
    }

}
