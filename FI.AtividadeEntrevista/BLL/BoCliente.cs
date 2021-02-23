using System.Collections.Generic;
using FI.AtividadeEntrevista.DAL.Clientes;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(Cliente cliente)
        {
            return new DaoCliente().Incluir(cliente);
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(Cliente cliente)
        {
            new DaoCliente().Alterar(cliente);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public Cliente Consultar(long id)
        {
            return new DaoCliente().Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            new DaoCliente().Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<Cliente> Listar()
        {
            return new DaoCliente().Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            return new DaoCliente().Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        /// <param name="id">Identificado do cliente</param>
        /// <returns></returns>
        public bool VerificarExistencia(string cpf, long id)
        {
            return new DaoCliente().VerificarExistencia(cpf, id);
        }
    }
}