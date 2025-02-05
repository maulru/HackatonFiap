using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Enums.Medico;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Infrastructure.AppDbContext;

namespace UsuarioAPI.Infrastructure.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Medico> _dbSet;

        public MedicoRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Medico>();
        }

        public async Task<Medico> Adicionar(Medico medico)
        {
            await _dbSet.AddAsync(medico);
            await _context.SaveChangesAsync();

            return medico;
        }

        public async Task<List<Medico>> ObterMedicosDisponiveis()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<Medico>> ObterMedicosDisponiveisPorEspecialidade(List<Especialidades> filtroEspecialidades)
        {
            IQueryable<Medico> query = _dbSet.Include(m => m.Usuario);

            if (filtroEspecialidades != null && filtroEspecialidades.Any())
                query = query.Where(m => filtroEspecialidades.Contains(m.Especialidade));

            return await query.ToListAsync();
        }


        public async Task<bool> VerificarExistenciaCRM(string numeroCRM)
        {
            return await _dbSet.AnyAsync(e => e.NumeroCRM == numeroCRM);
        }
    }
}
