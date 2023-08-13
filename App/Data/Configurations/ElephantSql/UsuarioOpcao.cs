namespace Data.Configurations.ElephantSql;

public class UsuarioOpcaoConfiguration : IEntityTypeConfiguration<UsuarioOpcao>
{
    public void Configure(EntityTypeBuilder<UsuarioOpcao> builder)
    {
        builder.ToTable("usuario_opcao", "dbo");

        builder.HasKey(x => new { x.IdUsuario, x.IdOpcaoSistema }).HasName("pk_usuario_opcao");

        builder.Property(x => x.IdUsuario).HasColumnName("id_usuario");
        builder.Property(x => x.IdOpcaoSistema).HasColumnName("id_opcao_sistema");
        builder.Property(x => x.DataCadastro).HasColumnName("data_cadastro");
        builder.Property(x => x.UsuarioCadastro).HasColumnName("usuario_cadastro");
        
        builder.HasOne(x => x.Usuario).WithMany(x => x.UsuarioOpcoes).HasForeignKey(x => x.IdUsuario).HasConstraintName("fk_usuario_opcao__usuario");
        builder.HasOne(x => x.OpcaoSistema).WithMany(x => x.UsuarioOpcoes).HasForeignKey(x => x.IdOpcaoSistema).HasConstraintName("fk_usuario_opcao__opcao_sistema");
    }
}
