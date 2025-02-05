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

        public Task<List<Medico>> ObterMedicosDisponiveis(List<Especialidades> filtroEspecialidades)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerificarExistenciaCRM(string numeroCRM)
        {
            return await _dbSet.AnyAsync(e => e.NumeroCRM == numeroCRM);
        }
    }
}
