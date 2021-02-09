using BillPay.Domain.Entity;
using BillPay.Domain.Validator;
using BillPay.Domain.Validator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BillPay.Application.Util
{
    public class Util
    {
        /// <summary>
        /// Executar servico seguro com retorno.
        /// </summary>
        /// <param name="execucao">A execucao.</param>
        /// <param name="mensagemSucesso">A mensagem sucesso.</param>
        /// <returns>
        /// O retorno da execução.
        /// </returns>
        public static ControlledReturn ExecutarServicoSeguroComRetorno(Func<object> execucao, string mensagemSucesso = "Operação realizada com sucesso.")
        {
            object retorno = null;
            try
            {
                retorno = execucao();
            }
            catch (ParameterException erro)
            {
                throw erro;
            }
            catch (BusinessException<ResultValidator> erro)
            {
                throw erro;
            }
            catch (Exception erro)
            {
                throw erro;
            }

            return new ControlledReturn
            {
                StatusCode = HttpStatusCode.OK,
                Inconsistencies = null,
                Data = retorno,
                Message = mensagemSucesso
            };
        }
    }
}
