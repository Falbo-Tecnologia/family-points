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

        public async Task<Usuario> BuscarPorApelidoAsync(string apelido)
        {
            return await _dbContext.Usuarios.Where(x => x.Apelido == apelido)
                .Select(x => new Usuario
                {
                    Id = x.Id,
                    IdTipoUsuario = x.IdTipoUsuario,
                    Nome = x.Nome,
                    Email = x.Email,
                    Apelido = x.Apelido,
                    Senha = x.Senha,
                    DataCadastro = x.DataCadastro,
                    UsuarioCadastro = x.UsuarioCadastro,
                    UsuarioOpcoes = x.UsuarioOpcoes.Where(y => y.IdUsuario == x.Id).Select(y => new UsuarioOpcao
                    {
                        IdUsuario = y.IdUsuario,
                        IdOpcaoSistema = y.IdOpcaoSistema,
                        DataCadastro = y.DataCadastro,
                        UsuarioCadastro = y.UsuarioCadastro,
                        OpcaoSistema = new OpcaoSistema
                        {
                            Id = y.OpcaoSistema.Id,
                            IdOpcaoMae = y.OpcaoSistema.IdOpcaoMae,
                            Descricao = y.OpcaoSistema.Descricao,
                        }
                    }),
                    TipoUsuario = new TipoUsuario
                    {
                        Id = x.TipoUsuario.Id,
                        Tipo = x.TipoUsuario.Tipo
                    }
                }).FirstOrDefaultAsync();
        }
    }
}
