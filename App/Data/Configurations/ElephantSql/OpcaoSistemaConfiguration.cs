namespace Data.Configurations.ElephantSql;

public class OpcaoSistemaConfiguration : IEntityTypeConfiguration<OpcaoSistema>
{
    public void Configure(EntityTypeBuilder<OpcaoSistema> builder)
    {
        builder.ToTable("opcao_sistema", "dbo");

        builder.HasKey(x => x.Id).HasName("pk_opcao_sistema");

        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id");
        builder.Property(x => x.IdOpcaoMae).HasColumnName("id_opcao_mae");
        builder.Property(x => x.Descricao).HasColumnName("descricao");
        
        builder.HasOne(x => x.OpcaoMae).WithMany(x => x.OpcoesFilhas).HasForeignKey(x => x.IdOpcaoMae).HasConstraintName("fk_opcao_sistema__opcao_sistema");
        builder.HasMany(x => x.OpcoesFilhas).WithOne(x => x.OpcaoMae).HasForeignKey(x => x.IdOpcaoMae).HasConstraintName("fk_opcao_sistema__opcao_sistema");
        builder.HasMany(x => x.UsuarioOpcoes).WithOne(x => x.OpcaoSistema).HasForeignKey(x => x.IdOpcaoSistema).HasConstraintName("fk_usuario_opcao__opcao_sistema");
    }
}
