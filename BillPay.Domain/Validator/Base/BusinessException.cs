using BillPay.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillPay.Domain.Validator
{
    public class BusinessException<TDetail> : Exception where TDetail : ResultValidator
    {
        public BusinessException(TDetail resultado)
        {
            this.Detalhes = resultado;
        }

        public BusinessException(string mensagem) : this(null, mensagem)
        {
        }

        public BusinessException(string propriedade, string mensagem)
        {
            var detalhes = new ResultValidator();
            detalhes.AdicionarInconsistencia(new Inconsistency(propriedade, mensagem));
            Detalhes = (TDetail)detalhes;
        }


        public TDetail Detalhes { get; }
    }
}
