using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiario
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required(ErrorMessage = "Informe o Nome")]
        public string Nome { get; set; }
        
        /// <summary>
        /// CPF
        /// </summary>
        [Required(ErrorMessage = "Informe o CPF")]
        [CustomValidationCpf(ErrorMessage = "CPF inválido")]
        public string Cpf { get; set; }

        public long IdCliente { get; set; }
    }
}