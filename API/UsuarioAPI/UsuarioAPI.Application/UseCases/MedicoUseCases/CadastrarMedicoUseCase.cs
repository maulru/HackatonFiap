using AutoMapper;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Application.DTOs.Medico;
using UsuarioAPI.Application.Services;
using UsuarioAPI.Domain.Entities.Base;
using UsuarioAPI.Domain.Entities.Medico;
using UsuarioAPI.Domain.Enums;
using UsuarioAPI.Domain.Repositories;
using UsuarioAPI.Domain.Services;

namespace UsuarioAPI.Application.UseCases.MedicoUseCases
{
    public class CadastrarMedicoUseCase
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly ISecurityService _securityRepository;
        private readonly IMapper _mapper;
        private readonly UsuarioServices _usuarioServices;

        public CadastrarMedicoUseCase(IMedicoRepository medicoRepository, ISecurityService securityRepository, IMapper mapper, UsuarioServices usuarioServices) 
        {
            _medicoRepository = medicoRepository;
            _securityRepository = securityRepository;
            _mapper = mapper;
            _usuarioServices = usuarioServices;
        }

        public async Task<RetornoMedicoCadastrado> Cadastrar(CadMedicoDTO medicoDTO)
        {
            UsuarioBase usuario = _mapper.Map<UsuarioBase>(medicoDTO);
            usuario.Tipo = TipoUsuario.Medico;
            usuario.UserName = medicoDTO.NumeroCRM;

            RetornoUsuarioCadastrado usuarioCadastrado = await _usuarioServices.Cadastrar(usuario);

            Medico medico = new Medico 
            { 
              IdUsuario = usuarioCadastrado.Id, 
              NumeroCRM = medicoDTO.NumeroCRM,
              Especialidade = medicoDTO.Especialidade
            };

            Medico medicoCadastrado = await _medicoRepository.Adicionar(medico);

            return _mapper.Map<RetornoMedicoCadastrado>(medicoCadastrado);
        }

    }
}
