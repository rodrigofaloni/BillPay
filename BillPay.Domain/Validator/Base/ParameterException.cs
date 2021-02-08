using System;
using System.Collections.Generic;
using System.Text;

namespace BillPay.Domain.Validator.Base
{
    public class ParameterException : Exception
    {
        /// <summary>
        /// Obtém ou define o nome da propriedade para vincular a inconsistência.
        /// </summary>
        public string Propriedade { get; set; }

        /// <summary>
        /// Inicializa a instancia da classe <see cref="ParametroException"/>.
        /// </summary>
        /// <param name="propriedade">A propriedade.</param>
        /// <param name="mensagem">A mensagem.</param>
        public ParameterException(string propriedade, string mensagem) : base(mensagem)
        {
            this.Propriedade = propriedade;
        }
    }
}
