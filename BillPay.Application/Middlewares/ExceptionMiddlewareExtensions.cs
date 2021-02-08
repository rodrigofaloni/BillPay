using BillPay.Domain.Entity;
using BillPay.Domain.Validator;
using BillPay.Domain.Validator.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BillPay.Application.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature == null) return;

                var (statusCode, objetoFalha) = ObterTipoFalha(contextFeature.Error);
                var response = SerializarComIdentacao(objetoFalha);

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response);
            });
        });
    }

    private static (int, ControlledReturn) ObterTipoFalha(Exception excecao)
    {
            var statusCode = HttpStatusCode.InternalServerError;
            var falhas = new List<InconsistencyModel> { new InconsistencyModel { Mensagem = MensagemErroExcecao(excecao), Propriedade = null } };
            var mensagem = "Erro interno na api do whatsapp. ";

            if (excecao is ParameterException parametroException)
            {
                statusCode = HttpStatusCode.BadRequest;
                falhas = new List<InconsistencyModel> { new InconsistencyModel { Mensagem = parametroException.Message, Propriedade = parametroException.Propriedade } };
                mensagem = "Parâmetro(s) incorreto(s). ";
            }

            if (excecao is BusinessException<ResultValidator> validacaoException)
            {
                statusCode = HttpStatusCode.PreconditionFailed;
                falhas = ConverterInconsistencias(validacaoException.Detalhes.ObterInconsistencia());
                mensagem = "Existem inconsistências.";
            }

            if (excecao is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                mensagem = "Nenhum resultado encontrado. ";
                falhas = new List<InconsistencyModel>();
            }

            return ((int)statusCode, new ControlledReturn { StatusCode = statusCode, Inconsistencias = falhas, Mensagem = mensagem });
        }

    private static string MensagemErroExcecao(Exception excecao)
    {
        var mensagem = new StringBuilder();
        mensagem.AppendFormat("Erro: {0}\n", excecao.Message);
        mensagem.AppendFormat("Exceção: {0}\n", excecao.Message);
        mensagem.AppendFormat("Pilha: {0}\n", excecao.StackTrace);
        mensagem.AppendFormat("Source: {0}\n", excecao.Source);

        while (excecao.InnerException != null)
        {
            excecao = excecao.InnerException;
            mensagem.AppendFormat("Exceção: {0}\n", excecao.Message);
            mensagem.AppendFormat("Pilha: {0}\n", excecao.StackTrace);
            mensagem.AppendFormat("Source: {0}\n", excecao.Source);
        }

        return mensagem.ToString();
    }

    private static List<InconsistencyModel> ConverterInconsistencias(List<Inconsistency> lista)
    {
        var listaConvertida = new List<InconsistencyModel>();
        if (lista != null && lista.Count > 0)
        {
            lista.ForEach(i => listaConvertida.Add(new InconsistencyModel() { Mensagem = i.Mensagem, Propriedade = i.PropriedadeValidada }));
        }

        return listaConvertida;
    }

    private static string SerializarComIdentacao(object objeto)
    {
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        return JsonConvert.SerializeObject(objeto, settings);
    }
}
}
