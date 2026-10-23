using System;
using System.Collections.Generic;

namespace oficina_api.Models;

public partial class Fabricante
{
    public int FabricanteId { get; set; }

    public string FabricanteNome { get; set; } = null!;

    public string? FabricanteCnpj { get; set; }

    public string? FabricantePais { get; set; }

    public virtual ICollection<Peca> Pecas { get; set; } = new List<Peca>();
}
