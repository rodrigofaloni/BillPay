using BillPay.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillPay.Domain.Validator
{
    [Serializable]
    public class ResultValidator
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ResultadoValidacao"/>, que por default é válida.
        /// </summary>
        public ResultValidator()
        {
            this.ListaInconsistecias = null;
            this.Valido = true;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ResultadoValidacao"/>, baseado em uma inconsistência.
        /// </summary>
        /// <param name="inconsistencia">The inconsistencia.</param>
        public ResultValidator(Inconsistency inconsistencia)
        {
            this.AdicionarInconsistencia(inconsistencia);
            this.Valido = false;
        }

        /// <summary>
        /// Obtém ou define se o resultado é válido ou não.
        /// </summary>
        /// <value>
        ///   <c>true</c> Caso for válido; caso contrário, <c>false</c>.
        /// </value>
        protected bool Valido { get; set; }

        /// <summary>
        /// Obtém ou define a lista de inconsistências.
        /// </summary>
        /// <value>
        /// A lista de inconsistências caso existam.
        /// </value>
        protected List<Inconsistency> ListaInconsistecias { get; set; }

        /// <summary>
        /// Adicionar uma nova instância a lista já existente.
        /// </summary>
        /// <param name="inconsistencia">A inconsistência para adição.</param>
        public void AdicionarInconsistencia(Inconsistency inconsistencia)
        {
            if (this.ListaInconsistecias == null)
            {
                this.ListaInconsistecias = new List<Inconsistency>();
            }

            this.ListaInconsistecias.Add(inconsistencia);
            this.Valido = false;
        }

        /// <summary>
        /// Obter a lista de inconsistências.
        /// </summary>
        /// <returns>Lista de inconsistências.</returns>
        public List<Inconsistency> ObterInconsistencia()
        {
            return this.ListaInconsistecias.ToList();
        }

        /// <summary>
        /// Diz se o resultado é válido.
        /// </summary>
        /// <returns>True caso seja válido, falso caso contrário.</returns>
        public bool EhValido()
        {
            return this.Valido;
        }
    }
}
