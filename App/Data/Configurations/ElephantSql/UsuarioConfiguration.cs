namespace Data.Configurations.ElephantSql;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario", "dbo");

        builder.HasKey(x => x.Id).HasName("pk_usuario");

        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id");
        builder.Property(x => x.IdTipoUsuario).HasColumnName("id_tipo_usuario");
        builder.Property(x => x.Nome).HasColumnName("nome");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.Apelido).HasColumnName("apelido");
        builder.Property(x => x.Senha).HasColumnName("senha");
        builder.Property(x => x.DataCadastro).HasColumnName("data_cadastro");
        builder.Property(x => x.UsuarioCadastro).HasColumnName("usuario_cadastro");

        builder.HasOne(x => x.TipoUsuario).WithMany(x => x.Usuarios).HasForeignKey(x => x.IdTipoUsuario).HasConstraintName("fk_usuario__tipo_usuario");
        builder.HasMany(x => x.TarefasUsuarios).WithOne(x => x.Usuario).HasForeignKey(x => x.IdUsuario).HasConstraintName("fk_tarefa_usuario__usuario");
        builder.HasMany(x => x.UsuarioOpcoes).WithOne(x => x.Usuario).HasForeignKey(x => x.IdUsuario).HasConstraintName("fk_usuario_opcao__usuario");
    }
}
