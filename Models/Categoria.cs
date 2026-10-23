using System;
using System.Collections.Generic;


namespace oficina_api.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string CategoriaNome { get; set; } = null!;

    public string? CategoriaDescricao { get; set; }

    public virtual ICollection<Peca> Pecas { get; set; } = new List<Peca>();
}
