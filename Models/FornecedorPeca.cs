using System;
using System.Collections.Generic;

namespace oficina_api.Models;

public partial class FornecedorPeca
{
    public int FornecedorPecaId { get; set; }

    public int FornecedorId { get; set; }

    public int PecaId { get; set; }

    public decimal FornecedorPecaPrecoCusto { get; set; }

    public int? FornecedorPecaPrazoEntregaDias { get; set; }

    public string? FornecedorPecaCodigo { get; set; }

    public virtual Fornecedor Fornecedor { get; set; } = null!;

    public virtual Peca Peca { get; set; } = null!;
}
