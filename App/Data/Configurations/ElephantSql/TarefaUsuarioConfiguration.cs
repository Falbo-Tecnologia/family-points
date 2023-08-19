namespace Data.Configurations.ElephantSql;

public class TarefaUsuarioConfiguration : IEntityTypeConfiguration<TarefaUsuario>
{
    public void Configure(EntityTypeBuilder<TarefaUsuario> builder)
    {
        builder.ToTable("tarefa_usuario", "dbo");

        builder.HasKey(x => x.Id).HasName("pk_tarefa_usuario");

        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id");
        builder.Property(x => x.IdTarefa).HasColumnName("id_tarefa");
        builder.Property(x => x.IdUsuario).HasColumnName("id_usuario");
        builder.Property(x => x.DataCadastro).HasColumnName("data_cadastro");
        builder.Property(x => x.UsuarioCadastro).HasColumnName("usuario_cadastro");

        builder.HasOne(x => x.Tarefa).WithMany(x => x.TarefasUsuarios).HasForeignKey(x => x.IdUsuario).HasConstraintName("fk_tarefa_usuario__tarefa");
        builder.HasOne(x => x.Usuario).WithMany(x => x.TarefasUsuarios).HasForeignKey(x => x.IdUsuario).HasConstraintName("fk_tarefa_usuario__usuario");
    }
}
