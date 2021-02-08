using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BillPay.Domain.Entity
{
    public class ControlledReturn
    {
        /// <summary>
        /// Cria uma nova instância da classe <see cref="Retorno"/>.
        /// </summary>
        public ControlledReturn()
        {
            this.Inconsistencias = new List<InconsistencyModel>();
        }

        /// <summary>
        /// Obtém ou define o status code.
        /// </summary>
        /// <value>
        /// O status code.
        /// </value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Obtém ou define a mensagem do retorno.
        /// </summary>
        /// <value>
        /// A mensagem do retorno.
        /// </value>
        public string Mensagem { get; set; }

        /// <summary>
        /// Obtém ou define as inconsistência do retorno.
        /// </summary>
        /// <value>
        /// The inconsistencias.
        /// </value>
        public List<InconsistencyModel> Inconsistencias { get; set; }

        /// <summary>
        /// Obtém ou define dados complementares.
        /// </summary>
        /// <value>
        /// Os dados complementares.
        /// </value>
        public object Data { get; set; }
    }
}
