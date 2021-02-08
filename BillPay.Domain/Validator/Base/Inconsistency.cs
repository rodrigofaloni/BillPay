using System;
using System.Collections.Generic;
using System.Text;

namespace BillPay.Domain.Validator
{
    [Serializable]
    public class Inconsistency
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Inconsistencia"/> baseado na propriedade validada e na mensagem de erro.
        /// </summary>
        /// <param name="propriedadeValidada">A propriedade validada.</param>
        /// <param name="mensagem">A mensagem para exibição.</param>
        public Inconsistency(string propriedadeValidada, string mensagem)
        {
            this.PropriedadeValidada = propriedadeValidada;
            this.Mensagem = mensagem;
        }

        /// <summary>
        /// Obtém ou define a propriedade validada, utilizada para vincular o a Inconsistencia a uma propriedade de um objeto.
        /// </summary>
        /// <value>
        /// O nome da propriedade validada.
        /// </value>
        public virtual string PropriedadeValidada { get; set; }

        /// <summary>
        /// Obtém ou define a Mensagem vinculada a Inconsistencia.
        /// </summary>
        /// <value>
        /// A mensagem da Inconsistencia.
        /// </value>
        public virtual string Mensagem { get; set; }
    }
}
