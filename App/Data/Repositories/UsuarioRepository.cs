
using Core.Interfaces.Repositories;
using Data.Server;

namespace Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _dbContext;

        public UsuarioRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync() => await _dbContext.Usuarios.ToListAsync();

        public async Task<Usuario> GetUsuarioAsync(int id) => await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

        public async Task PutUsuarioAsync(Usuario usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
        }
    }
}
