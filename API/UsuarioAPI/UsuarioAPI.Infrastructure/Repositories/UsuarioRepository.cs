using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Application.Services;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Exceptions;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Infrastructure.AppDbContext;
using UsuarioAPI.Infrastructure.Security;

namespace UsuarioAPI.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<UsuarioBase> _dbSet;
        private UserManager<UsuarioBase> _userManager;
        private SignInManager<UsuarioBase> _signInManager;
        private TokenService _tokenService;

        public UsuarioRepository(ApplicationDbContext context,
            UserManager<UsuarioBase> userManager,
            SignInManager<UsuarioBase> signInManager,
            TokenService tokenService
            )
        {
            _context = context;
            _dbSet = _context.Set<UsuarioBase>();
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<UsuarioBase> Adicionar(UsuarioBase usuario)
        {
            await _dbSet.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<IdentityResult> CadastraAsync(UsuarioBase usuario)
        {

            IdentityResult resultado = await _userManager.CreateAsync(usuario, usuario.Senha);

            if (!resultado.Succeeded)
            {
                List<string> erros = new List<string>();

                foreach (var erro in resultado.Errors)
                {
                    erros.Add(erro.Description);
                }

                throw new UserBaseExceptions(erros);
            }

            return resultado;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);


            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }

            var usuario = _signInManager
                .UserManager
            .Users
                .FirstOrDefault(user => user.Email ==
                dto.Email.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
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
