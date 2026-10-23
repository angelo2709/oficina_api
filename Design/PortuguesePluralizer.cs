using Microsoft.EntityFrameworkCore.Design;

namespace oficina_api.Design;

public class PortuguesePluralizer : IPluralizer
{
    public string Singularize(string identifier)
    {
        return identifier.ToLower() switch
        {
            "categorias" => "Categoria",
            "fabricantes" => "Fabricante",
            "pecas" => "Peca",
            "fornecedores" => "Fornecedor",

            // O EF pode entregar "FornecedoresPecas"
            // em vez do nome original "fornecedores_pecas".
            "fornecedorespecas" => "FornecedorPeca",
            "fornecedores_pecas" => "FornecedorPeca",

            _ => identifier
        };
    }

    public string Pluralize(string identifier)
    {
        return identifier switch
        {
            "Categoria" => "Categorias",
            "Fabricante" => "Fabricantes",
            "Peca" => "Pecas",
            "Fornecedor" => "Fornecedores",
            "FornecedorPeca" => "FornecedoresPecas",

            _ => identifier
        };
    }
}