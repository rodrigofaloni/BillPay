using System;
using System.Collections.Generic;
using System.Text;

namespace BillPay.Domain.Entity
{
    public class InconsistencyModel
    {
        /// <summary>
        /// Obtém ou define o nome da propriedade para vincular a inconsistência.
        /// </summary>
        /// <value>
        /// O valor da propriedade.
        /// </value>
        public string Propriedade { get; set; }

        /// <summary>
        /// Obtém ou define a mensagem que será vinculada a propriedade.
        /// </summary>
        /// <value>
        /// O valor da mensagem.
        /// </value>
        public string Mensagem { get; set; }
    }
}
