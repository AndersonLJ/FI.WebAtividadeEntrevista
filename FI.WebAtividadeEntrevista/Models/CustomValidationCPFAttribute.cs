using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Validação customizada para CPF
    /// </summary>
    public class CustomValidationCpfAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Validação server
        /// </summary>
        /// <param name="value"></param>
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;
            var valido = Cpf(value.ToString());
            return valido;
        }

        /// <summary>
        /// Validação client
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage("CPF inválido"),
                ValidationType = "customvalidationcpf"
            };
        }

        /// <summary>
        /// Valida se um cpf é válido
        /// </summary>
        /// <param name="cpf"></param>
        private static bool Cpf(string cpf)
        {
            if (cpf == null)
                return false;

            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;
            var dv1 = 0;
            var dv2 = 0;

            var digitosIdenticos = true;
            var ultimoDigito = -1;

            foreach (var digito in from c in cpf where char.IsDigit(c) select c - '0')
            {
                if (posicao != 0 && ultimoDigito != digito)
                {
                    digitosIdenticos = false;
                }

                ultimoDigito = digito;
                if (posicao < 9)
                {
                    totalDigito1 += digito * (10 - posicao);
                    totalDigito2 += digito * (11 - posicao);
                }
                else if (posicao == 9)
                {
                    dv1 = digito;
                }
                else if (posicao == 10)
                {
                    dv2 = digito;
                }

                posicao++;
            }

            if (posicao > 11 || digitosIdenticos)
                return false;
            
            var digito1 = totalDigito1 % 11;
            digito1 = digito1 < 2
                ? 0
                : 11 - digito1;

            if (dv1 != digito1)
                return false;

            totalDigito2 += digito1 * 2;
            var digito2 = totalDigito2 % 11;
            digito2 = digito2 < 2
                ? 0
                : 11 - digito2;

            return dv2 == digito2;
        }
    }
}