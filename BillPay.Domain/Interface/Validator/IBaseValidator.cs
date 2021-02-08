using BillPay.Domain.Entity;
using BillPay.Domain.Validator;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillPay.Domain.Interface.Validator
{
    public interface IBaseValidator<T> where T : BaseEntity
    {
        /// <summary>
        /// Validar o objeto informado como parâmetro.
        /// </summary>
        /// <param name="objeto">O objeto para validação.</param>
        /// <returns>Resultado da validação contendo os possíveis erros.</returns>
        ResultValidator Validar(T objeto);

        /// <summary>
        /// Validador contendo as regras para o fluxo de Adicionar.
        /// </summary>
        /// <returns>Validador com as regras assinadas para o fluxo de Adicionar.</returns>
        IBaseValidator<T> ValidadorAdicao();

        /// <summary>
        /// Validador contendo as regras para o fluxo de Atualizar.
        /// </summary>
        /// <returns>Validador com as regras assinadas para o fluxo de Atualizar.</returns>
        IBaseValidator<T> ValidadorAtualizacao();

        /// <summary>
        /// Validador contendo as regras para o fluxo de Remover.
        /// </summary>
        /// <returns>Validador com as regras assinadas para o fluxo de Remover.</returns>
        IBaseValidator<T> ValidadorRemocao();
    }
}
