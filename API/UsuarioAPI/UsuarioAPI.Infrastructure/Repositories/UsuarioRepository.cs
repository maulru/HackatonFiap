using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Infrastructure.AppDbContext;

namespace UsuarioAPI.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<UsuarioBase> _dbSet;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<UsuarioBase>();
        }

        public async Task<UsuarioBase> Adicionar(UsuarioBase usuario)
        {
            await _dbSet.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioBase> AdicionarMedico(UsuarioBase usuario)
        {
            await _dbSet.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<bool> VerificarExistenciaCPF(string CPF)
        {
            return await _dbSet.AnyAsync(c => c.CPF == CPF);
        }

        public async Task<bool> VerificarExistenciaEmail(string Email)
        {
            return await _dbSet.AnyAsync(e => e.Email == Email);
        }
    }
}
