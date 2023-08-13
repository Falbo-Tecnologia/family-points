namespace Data.Configurations.ElephantSql;

public class TipoUsuarioConfiguration : IEntityTypeConfiguration<TipoUsuario>
{
    public void Configure(EntityTypeBuilder<TipoUsuario> builder)
    {
        builder.ToTable("tipo_usuario", "dbo");

        builder.HasKey(x => x.Id).HasName("pk_tipo_usuario");

        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id");
        builder.Property(x => x.Tipo).HasColumnName("tipo");
        
        builder.HasMany(x => x.Usuarios).WithOne(x => x.TipoUsuario).HasForeignKey(x => x.IdTipoUsuario).HasConstraintName("fk_usuario__tipo_usuario");
    }
}
