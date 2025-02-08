using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Domain.Entities.Paciente;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Infrastructure.AppDbContext;

namespace UsuarioAPI.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Paciente> _dbSet;

        public PacienteRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Paciente>();
        }

        public async Task<Paciente> Adicionar(Paciente paciente)
        {
            await _dbSet.AddAsync(paciente);
            await _context.SaveChangesAsync();

            return paciente;
        }

        public async Task<Paciente> GetByIdUsuarioAsync(string idUsuario)
        {
            return await _dbSet
                .Include(p => p.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);
        }

    }
}
