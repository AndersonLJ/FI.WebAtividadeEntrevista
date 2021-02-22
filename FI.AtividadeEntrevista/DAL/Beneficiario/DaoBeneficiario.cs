using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FI.AtividadeEntrevista.DAL.Padrao;

namespace FI.AtividadeEntrevista.DAL.Beneficiario
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiário
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto do beneficiário</param>
        internal long Incluir(DML.Beneficiario beneficiario)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("NOME", beneficiario.Nome),
                new SqlParameter("CPF", beneficiario.Cpf),
                new SqlParameter("IDCLIENTE", beneficiario.IdCliente)
            };

            var ds = Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Verificar se CPF está cadastrado
        /// </summary>
        /// <param name="cpf">CPF do beneficiário</param>
        /// <param name="idCliente">Id do cliente do beneficiário</param>
        internal bool VerificarExistencia(string cpf, long idCliente)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("CPF", cpf),
                new SqlParameter("IDCLIENTE", idCliente)
            };

            var ds = Consultar("FI_SP_VerificaBeneficiario", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<DML.Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("iniciarEm", iniciarEm),
                new SqlParameter("quantidade", quantidade),
                new SqlParameter("campoOrdenacao", campoOrdenacao),
                new SqlParameter("crescente", crescente)
            };

            var ds = Consultar("FI_SP_PesqBeneficiario", parametros);
            var beneficiarios = Converter(ds);

            var iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return beneficiarios;
        }

        /// <summary>
        /// Lista todos os Beneficiário do cliente
        /// </summary>
        internal List<DML.Beneficiario> Listar(int idCliente)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("IDCLIENTE", idCliente)
            };

            var ds = Consultar("FI_SP_ConsBeneficiario", parametros);
            var beneficiarios = Converter(ds);

            return beneficiarios;
        }

        /// <summary>
        /// Atualizar os dados do beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto beneficiário</param>
        internal void Alterar(DML.Beneficiario beneficiario)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("NOME", beneficiario.Nome),
                new SqlParameter("CPF", beneficiario.Cpf),
                new SqlParameter("ID", beneficiario.Id),
                new SqlParameter("IDCLIENTE", beneficiario.IdCliente)
            };

            Executar("FI_SP_AltBeneficiario", parametros);
        }

        /// <summary>
        /// Excluir Beneficiário
        /// </summary>
        /// <param name="id">Identificação do beneficiário</param>
        internal void Excluir(long id)
        {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("Id", id)
            };

            Executar("FI_SP_DelBeneficiario", parametros);
        }

        private List<DML.Beneficiario> Converter(DataSet ds)
        {
            var lista = new List<DML.Beneficiario>();
            if (ds?.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lista.AddRange(from DataRow row in ds.Tables[0].Rows
                               select new DML.Beneficiario
                               {
                                   Id = row.Field<long>("ID"),
                                   Nome = row.Field<string>("NOME"),
                                   Cpf = row.Field<string>("CPF"),
                                   IdCliente = row.Field<long>("IDCLIENTE")
                               });
            }

            return lista;
        }
    }
}