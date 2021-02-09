using BillPay.Domain.Entity;
using BillPay.Domain.Validator;
using BillPay.Domain.Validator.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BillPay.Application.Middlewares
{
    /// <summary>
    /// The extension of middleware exception.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Configures the exception handler.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null) return;

                    var (statusCode, objetoFalha) = GetErrorType(contextFeature.Error);
                    var response = SerializeWithIdentification(objetoFalha);

                    context.Response.StatusCode = statusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(response);
                });
            });
        }

        /// <summary>
        /// Responsible to get the error type.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        /// Return the controlled return.
        /// </returns>
        private static (int, ControlledReturn) GetErrorType(Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var falhas = new List<InconsistencyModel> { new InconsistencyModel { Message = ErrorMessageException(exception), Property = null } };
            var mensagem = "Internal error in API.";

            if (exception is ParameterException parametroException)
            {
                statusCode = HttpStatusCode.BadRequest;
                falhas = new List<InconsistencyModel> { new InconsistencyModel { Message = parametroException.Message, Property = parametroException.Property } };
                mensagem = "Incorrect parameter(s).";
            }

            if (exception is BusinessException<ResultValidator> validacaoException)
            {
                statusCode = HttpStatusCode.PreconditionFailed;
                falhas = ConvertInconsistencies(validacaoException.Details.GetInconsistencies());
                mensagem = "There are inconsistencies.";
            }

            if (exception is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                mensagem = "No results found.";
                falhas = new List<InconsistencyModel>();
            }

            return ((int)statusCode, new ControlledReturn { StatusCode = statusCode, Inconsistencies = falhas, Message = mensagem });
        }

        /// <summary>
        /// Errors the message exception.
        /// </summary>
        /// <param name="excecao">The excecao.</param>
        /// <returns>Return the error message exception.</returns>
        private static string ErrorMessageException(Exception excecao)
        {
            var mensagem = new StringBuilder();
            mensagem.AppendFormat("Error: {0}\n", excecao.Message);
            mensagem.AppendFormat("Exception: {0}\n", excecao.Message);
            mensagem.AppendFormat("StackTrace: {0}\n", excecao.StackTrace);
            mensagem.AppendFormat("Source: {0}\n", excecao.Source);

            while (excecao.InnerException != null)
            {
                excecao = excecao.InnerException;
                mensagem.AppendFormat("Exception: {0}\n", excecao.Message);
                mensagem.AppendFormat("StackTrace: {0}\n", excecao.StackTrace);
                mensagem.AppendFormat("Source: {0}\n", excecao.Source);
            }

            return mensagem.ToString();
        }

        /// <summary>
        /// Converts the inconsistencies.
        /// </summary>
        /// <param name="listItem">The lista.</param>
        /// <returns>Return the list of inconsistencies.</returns>
        private static List<InconsistencyModel> ConvertInconsistencies(List<Inconsistency> listItem)
        {
            var convertedList = new List<InconsistencyModel>();
            if (listItem != null && listItem.Count > 0)
            {
                listItem.ForEach(i => convertedList.Add(new InconsistencyModel() { Message = i.Message, Property = i.ValidateProperty }));
            }

            return convertedList;
        }

        /// <summary>
        /// Serializes the with identification.
        /// </summary>
        /// <param name="objeto">The objeto.</param>
        /// <returns>Return the json object.</returns>
        private static string SerializeWithIdentification(object objeto)
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
