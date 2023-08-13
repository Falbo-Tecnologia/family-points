namespace Data.Configurations.ElephantSql;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario");

        builder.HasKey(x => x.Id).HasName("pk_usuario");

        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id");
        builder.Property(x => x.Nome).HasColumnName("nome");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.Login).HasColumnName("login");
        builder.Property(x => x.Senha).HasColumnName("senha");
        builder.Property(x => x.DataCadastro).HasColumnName("data_cadastro");
        builder.Property(x => x.UsuarioCadastro).HasColumnName("usuario_cadastro");

        builder.HasMany(x => x.Tarefas).WithOne(x => x.Usuario).HasForeignKey(x => x.IdUsuario).HasConstraintName("fk_tarefa_usuario");
    }
}
