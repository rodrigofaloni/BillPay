using BillPay.Domain.Entity;
using BillPay.Domain.Interface.Repository;
using BillPay.Domain.Interface.Validator;
using BillPay.Domain.Validator.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillPay.Domain.Validator
{
    public class BillValidator : EntityValidator<Bill>
    {
        private IBillRepository billRepository;

        public BillValidator(IBillRepository billRepository)
        {
            this.billRepository = billRepository;
        }

        public override IBaseValidator<Bill> ValidadorAdicao()
        {
            this.AssinarRegrasComum();
            return base.ValidadorAdicao();
        }

        private void AssinarRegrasComum()
        {
            this.CampoObrigatorio(x => x.Name).When(x => x != null);
            ////this.CampoObrigatorio(x => x.Login).When(x => x != null);
            ////this.CampoObrigatorio(x => x.Senha).When(x => x != null);

            //////Valida tamanho do login
            ////this.RuleFor(x => x.Login).Must(x => x.Length <= 200)
            ////    .WithMessage("O tamanho para o login é de 20 carateres")
            ////    .When(x => x != null && !string.IsNullOrWhiteSpace(x.Login));

            //////Valida tamanho do login
            ////this.RuleFor(x => x).Must(x => repositorioUsuario.QtdeUsuarioMesmoLogin(x) <= 0)
            ////    .WithMessage("O login informado já está sendo usado por outro usuário")
            ////    .WithName("Login")
            ////    .When(x => x != null && !string.IsNullOrWhiteSpace(x.Nome));
        }
    }
}
