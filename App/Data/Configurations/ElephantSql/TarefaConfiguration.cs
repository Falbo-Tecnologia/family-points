namespace Data.Configurations.ElephantSql;

public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("tarefa", "dbo");

        builder.HasKey(x => x.Id).HasName("pk_tarefa");

        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id");
        builder.Property(x => x.Descricao).HasColumnName("descricao");
        builder.Property(x => x.Pontuacao).HasColumnName("pontuacao");
        builder.Property(x => x.DataCadastro).HasColumnName("data_cadastro");
        builder.Property(x => x.UsuarioCadastro).HasColumnName("usuario_cadastro");

        builder.HasMany(x => x.TarefasUsuarios).WithOne(x => x.Tarefa).HasForeignKey(x => x.IdUsuario).HasConstraintName("fk_tarefa_usuario__tarefa");
    }
}
