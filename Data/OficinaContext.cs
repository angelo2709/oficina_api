using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using oficina_api.Models;

namespace oficina_api.Data;

public partial class OficinaContext : DbContext
{
    public OficinaContext(DbContextOptions<OficinaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Fabricante> Fabricantes { get; set; }

    public virtual DbSet<Fornecedor> Fornecedores { get; set; }

    public virtual DbSet<FornecedorPeca> FornecedoresPecas { get; set; }

    public virtual DbSet<Peca> Pecas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.CategoriaNome, "categoria_nome").IsUnique();

            entity.Property(e => e.CategoriaId)
                .HasColumnType("int(11)")
                .HasColumnName("categoria_id");
            entity.Property(e => e.CategoriaDescricao)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("categoria_descricao");
            entity.Property(e => e.CategoriaNome)
                .HasMaxLength(50)
                .HasColumnName("categoria_nome");
        });

        modelBuilder.Entity<Fabricante>(entity =>
        {
            entity.HasKey(e => e.FabricanteId).HasName("PRIMARY");

            entity.ToTable("fabricantes");

            entity.HasIndex(e => e.FabricanteCnpj, "fabricante_cnpj").IsUnique();

            entity.HasIndex(e => e.FabricanteNome, "fabricante_nome").IsUnique();

            entity.Property(e => e.FabricanteId)
                .HasColumnType("int(11)")
                .HasColumnName("fabricante_id");
            entity.Property(e => e.FabricanteCnpj)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("fabricante_cnpj");
            entity.Property(e => e.FabricanteNome)
                .HasMaxLength(50)
                .HasColumnName("fabricante_nome");
            entity.Property(e => e.FabricantePais)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("fabricante_pais");
        });

        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.HasKey(e => e.FornecedorId).HasName("PRIMARY");

            entity.ToTable("fornecedores");

            entity.HasIndex(e => e.FornecedorCnpj, "fornecedor_cnpj").IsUnique();

            entity.Property(e => e.FornecedorId)
                .HasColumnType("int(11)")
                .HasColumnName("fornecedor_id");
            entity.Property(e => e.FornecedorCnpj)
                .HasMaxLength(20)
                .HasColumnName("fornecedor_cnpj");
            entity.Property(e => e.FornecedorEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("fornecedor_email");
            entity.Property(e => e.FornecedorRazaoSocial)
                .HasMaxLength(100)
                .HasColumnName("fornecedor_razao_social");
            entity.Property(e => e.FornecedorTelefone)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("fornecedor_telefone");
        });

        modelBuilder.Entity<FornecedorPeca>(entity =>
        {
            entity.HasKey(e => e.FornecedorPecaId).HasName("PRIMARY");

            entity.ToTable("fornecedores_pecas");

            entity.HasIndex(e => new { e.FornecedorId, e.PecaId }, "fornecedor_id").IsUnique();

            entity.HasIndex(e => e.PecaId, "peca_id");

            entity.Property(e => e.FornecedorPecaId)
                .HasColumnType("int(11)")
                .HasColumnName("fornecedor_peca_id");
            entity.Property(e => e.FornecedorId)
                .HasColumnType("int(11)")
                .HasColumnName("fornecedor_id");
            entity.Property(e => e.FornecedorPecaCodigo)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("fornecedor_peca_codigo");
            entity.Property(e => e.FornecedorPecaPrazoEntregaDias)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("fornecedor_peca_prazo_entrega_dias");
            entity.Property(e => e.FornecedorPecaPrecoCusto)
                .HasPrecision(10)
                .HasColumnName("fornecedor_peca_preco_custo");
            entity.Property(e => e.PecaId)
                .HasColumnType("int(11)")
                .HasColumnName("peca_id");

            entity.HasOne(d => d.Fornecedor).WithMany(p => p.FornecedoresPecas)
                .HasForeignKey(d => d.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fornecedores_pecas_ibfk_1");

            entity.HasOne(d => d.Peca).WithMany(p => p.FornecedoresPecas)
                .HasForeignKey(d => d.PecaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fornecedores_pecas_ibfk_2");
        });

        modelBuilder.Entity<Peca>(entity =>
        {
            entity.HasKey(e => e.PecaId).HasName("PRIMARY");

            entity.ToTable("pecas");

            entity.HasIndex(e => e.CategoriaId, "categoria_id");

            entity.HasIndex(e => e.FabricanteId, "fabricante_id");

            entity.HasIndex(e => e.PecaSku, "peca_sku").IsUnique();

            entity.Property(e => e.PecaId)
                .HasColumnType("int(11)")
                .HasColumnName("peca_id");
            entity.Property(e => e.CategoriaId)
                .HasColumnType("int(11)")
                .HasColumnName("categoria_id");
            entity.Property(e => e.FabricanteId)
                .HasColumnType("int(11)")
                .HasColumnName("fabricante_id");
            entity.Property(e => e.PecaEstoqueAtual)
                .HasColumnType("int(11)")
                .HasColumnName("peca_estoque_atual");
            entity.Property(e => e.PecaNome)
                .HasMaxLength(100)
                .HasColumnName("peca_nome");
            entity.Property(e => e.PecaPrecoVenda)
                .HasPrecision(10)
                .HasColumnName("peca_preco_venda");
            entity.Property(e => e.PecaSku)
                .HasMaxLength(30)
                .HasColumnName("peca_sku");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Pecas)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("pecas_ibfk_1");

            entity.HasOne(d => d.Fabricante).WithMany(p => p.Pecas)
                .HasForeignKey(d => d.FabricanteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("pecas_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
