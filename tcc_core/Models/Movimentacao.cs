﻿using System.ComponentModel.DataAnnotations;

namespace tcc_core.Models
{
    public class Movimentacao
    {
        [Key] 
        public int Id { get; set; }
        [Required(ErrorMessage = "O responsável é obrigatório.")]
        public string Responsavel { get; set; } = string.Empty;
        [Required(ErrorMessage = "O tipo de movimentação é obrigatório.")]
        public string TipoMovimentacao { get; set; } = string.Empty;
        [Required(ErrorMessage = "A data da moviemntação é obrigatória.")]
        public DateTime DtMovimentacao { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel? Usuario { get; set; }

        public int? ProjetoId { get; set; }
        public Projeto? Projeto { get; set; }

        public ICollection<MovimentacaoMaterial> MovimentacaoMaterial { get; set; } = new List<MovimentacaoMaterial>();
    }
}
