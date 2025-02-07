using AgendaAPI.Domain.Entities.Agenda;
using AgendaAPI.Domain.Enums.Agenda;
using AgendaAPI.Domain.Repositories;
using AgendaAPI.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace AgendaAPI.Infrastructure.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Horario> _dbSet;
        private readonly DbSet<Agendamento> _agendamentoDbSet;

        public ConsultaRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Horario>();
            _agendamentoDbSet = _context.Set<Agendamento>();
        }

        public async Task<List<Horario>> ObterHorariosDisponiveisAsync(int idMedico)
        {
            return await _dbSet
                .Where(h => h.IdMedico == idMedico && h.Disponibilidade == Disponibilidade.Disponivel)
                .ToListAsync();
        }

        public async Task<Agendamento> CadastrarAgendamentoAsync(Agendamento agendamento)
        {
            var horario = await _dbSet.FindAsync(agendamento.IdHorario);
            if (horario == null || horario.Disponibilidade != Disponibilidade.Disponivel)
                return null; 

            horario.Disponibilidade = Disponibilidade.Pendente;
            agendamento.Situacao = Disponibilidade.Pendente;
            agendamento.DataAgendamento = DateTime.UtcNow;

            await _agendamentoDbSet.AddAsync(agendamento);
            await _context.SaveChangesAsync();
            return agendamento;
        }

        public async Task<bool> CancelarAgendamentoAsync(int idAgendamento, string justificativa)
        {
            var agendamento = await _agendamentoDbSet.FindAsync(idAgendamento);
            if (agendamento == null || agendamento.Situacao == Disponibilidade.Cancelada)
                return false; // O agendamento já foi cancelado ou não existe

            var horario = await _dbSet.FindAsync(agendamento.IdHorario);
            if (horario == null)
                return false;

            // Atualiza o status do agendamento para Cancelado
            agendamento.Situacao = Disponibilidade.Cancelada;
            agendamento.Observacoes = justificativa;

            // Atualiza o status do horário para Disponível
            horario.Disponibilidade = Disponibilidade.Disponivel;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
