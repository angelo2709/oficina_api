using System;
using System.Collections.Generic;

namespace oficina_api.Models;

public partial class Peca
{
    public int PecaId { get; set; }

    public int CategoriaId { get; set; }

    public int FabricanteId { get; set; }

    public string PecaSku { get; set; } = null!;

    public string PecaNome { get; set; } = null!;

    public decimal PecaPrecoVenda { get; set; }

    public int PecaEstoqueAtual { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;

    public virtual Fabricante Fabricante { get; set; } = null!;

    public virtual ICollection<FornecedorPeca> FornecedoresPecas { get; set; } = new List<FornecedorPeca>();
}
