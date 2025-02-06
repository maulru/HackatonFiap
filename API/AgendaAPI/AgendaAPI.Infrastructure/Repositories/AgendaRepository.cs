using AgendaAPI.Domain.Entities.Agenda;
using AgendaAPI.Domain.Repositories;
using AgendaAPI.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace AgendaAPI.Infrastructure.Repositories
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Horario> _dbSet;

        public AgendaRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Horario>();
        }

        public async Task<Horario> CadastraHorarioAsync(Horario horario)
        {
            await _dbSet.AddAsync(horario);
            await _context.SaveChangesAsync();
            return horario;
        }
    }
}
