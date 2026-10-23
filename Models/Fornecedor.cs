using System;
using System.Collections.Generic;

namespace oficina_api.Models;

public partial class Fornecedor
{
    public int FornecedorId { get; set; }

    public string FornecedorRazaoSocial { get; set; } = null!;

    public string FornecedorCnpj { get; set; } = null!;

    public string? FornecedorTelefone { get; set; }

    public string? FornecedorEmail { get; set; }

    public virtual ICollection<FornecedorPeca> FornecedoresPecas { get; set; } = new List<FornecedorPeca>();
}
