using System.Collections.Generic;
using FI.AtividadeEntrevista.DAL.Beneficiario;
using FI.AtividadeEntrevista.DAL.Clientes;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public long Incluir(Beneficiario beneficiario)
        {
            return new DaoBeneficiario().Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public void Alterar(Beneficiario beneficiario)
        {
            new DaoBeneficiario().Alterar(beneficiario);
        }

        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        public void Excluir(long id)
        {
            new DaoCliente().Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        /// <param name="idCliente">Lista os beneficiários do cliente</param>
        public List<Beneficiario> Listar(int idCliente)
        {
            return new DaoBeneficiario().Listar(idCliente);
        }

        /// <summary>
        /// Lista os beneficiários
        /// </summary>
        public List<Beneficiario> Pesquisa(int idCliente)
        {
            return new DaoBeneficiario().Pesquisa(idCliente);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        public bool VerificarExistencia(string cpf, long idCliente)
        {
            return new DaoBeneficiario().VerificarExistencia(cpf, idCliente);
        }
    }
}