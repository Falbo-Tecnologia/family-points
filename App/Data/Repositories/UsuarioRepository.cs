namespace Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        protected IEncryption Encryption { get; private set; }
        private readonly AppDbContext _dbContext;

        public UsuarioRepository(AppDbContext dbContext, IEncryption encryption)
        {
            Encryption = encryption;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Usuario>> ListarAsync() => await _dbContext.Usuarios.ToListAsync();

        public async Task<Usuario> BuscarAsync(int id) => await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CadastrarDinamicoAsync(Usuario usuario = null, IEnumerable<Usuario> usuarios = null)
        {
            if (usuario != null)
                await _dbContext.Usuarios.AddAsync(usuario);

            if (usuarios != null)
                await _dbContext.Usuarios.AddRangeAsync(usuarios);

            await _dbContext.SaveChangesAsync();
        }
    }
}
