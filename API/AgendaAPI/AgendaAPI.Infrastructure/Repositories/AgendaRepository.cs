using AgendaAPI.Domain.Entities.Agenda;
using AgendaAPI.Domain.Enums.Agenda;
using AgendaAPI.Domain.Repositories;
using AgendaAPI.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace AgendaAPI.Infrastructure.Repositories
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Horario> _dbSet;
        private readonly DbSet<Agendamento> _agendamentoDbSet;

        public AgendaRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Horario>();
            _agendamentoDbSet = _context.Set<Agendamento>();
        }

        public async Task<Horario> CadastraHorarioAsync(Horario horario)
        {
            await _dbSet.AddAsync(horario);
            await _context.SaveChangesAsync();
            return horario;
        }

        public async Task<List<Horario>> ObterHorariosAsync(int idMedico)
        {
            return await _dbSet
                .Where(h => h.IdMedico == idMedico)
                .ToListAsync();
        }

        public async Task<Horario> AlterarHorarioAsync(Horario horario)
        {
            var horarioExistente = await _dbSet.FindAsync(horario.Id);
            if (horarioExistente == null)
                return null;

            _context.Entry(horarioExistente).CurrentValues.SetValues(horario);
            await _context.SaveChangesAsync();
            return horarioExistente;
        }

        public async Task<List<Horario>> ObterHorariosPendentesOuAgendadosAsync(int idMedico)
        {
            return await _dbSet
                .Where(h => h.IdMedico == idMedico &&
                            (h.Disponibilidade == Disponibilidade.Pendente || h.Disponibilidade == Disponibilidade.Agendada))
                .ToListAsync();
        }

        public async Task<bool> AlterarStatusAgendamentoAsync(int idAgendamento, Disponibilidade novoStatus, string observacoes = "")
        {
            var agendamento = await _agendamentoDbSet.FindAsync(idAgendamento);
            if (agendamento == null)
                return false;

            var horario = await _dbSet.FindAsync(agendamento.IdHorario);
            if (horario == null)
                return false;

            // Atualiza status do agendamento
            agendamento.Situacao = novoStatus;
            agendamento.Observacoes = observacoes;

            // Atualiza status do horário de acordo com a mudança no agendamento
            horario.Disponibilidade = novoStatus == Disponibilidade.Cancelada ? Disponibilidade.Disponivel : Disponibilidade.Indisponivel;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
