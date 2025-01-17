﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuControle.Dominio.Comandos.PlanoContaComando;
using MeuControle.Dominio.Compartilhado.Contratos;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Manipuladores;
using MeuControle.Dominio.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuControle.Api.Controllers
{
    [ApiController]
    [Route("v1/PlanoConta")]
    public class PlanoContaController : ControllerBase
    {
        [Authorize]
        [Route("")]
        [HttpPost]
        public async Task<ActionResult<PlanoConta>> Criar(
            [FromBody] CriarPlanoContaComando comando,
            [FromServices] PlanoContaManipulador manipulador)
        {
            var resultado = await manipulador.Manipular(comando);

            if (resultado.Sucesso)
                return Ok(resultado.Dado);

            return BadRequest(resultado.Mensagem);
        }

        [Authorize]
        [Route("")]
        [HttpPut]
        public async Task<ActionResult<GenericoComandoResultado>> Atualizar(
            [FromBody] AtualizarPlanoContaComando comando,
            [FromServices] PlanoContaManipulador manipulador) 
            => await manipulador.Manipular(comando);

        [Authorize]
        [Route("{usuario}/{planoConta}")]
        [HttpDelete]
        public async Task<ActionResult<GenericoComandoResultado>> Deletar(
            [FromServices] PlanoContaManipulador manipulador,
            Guid usuario, Guid planoConta) 
            => await manipulador.Manipular(new DeletarPlanoContaComando(planoConta, usuario));

        [Authorize]
        [Route("{usuario}")]
        [HttpGet]
        public IEnumerable<PlanoConta> ObterTodos([FromServices] IPlanoContaRepositorio repositorio, Guid usuario) 
            => repositorio.ObterTodos(usuario);

        [Authorize]
        [Route("{usuario}/{operacao}")]
        [HttpGet]
        public IEnumerable<PlanoConta> ObterPorOperacao([FromServices] IPlanoContaRepositorio repositorio, Guid usuario, string operacao)
            => repositorio.ObterPorOperacao(usuario, operacao);
    }
}
