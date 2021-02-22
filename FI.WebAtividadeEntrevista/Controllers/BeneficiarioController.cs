using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using System;
using System.Linq;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            var bo = new BoBeneficiario();

            if (bo.VerificarExistencia(model.Cpf, model.IdCliente))
                ModelState.AddModelError("CPF", "Este CPF já está cadastrado neste cliente");

            if (!ModelState.IsValid)
            {
                var erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            model.Id = bo.Incluir(new Beneficiario
            {
                Nome = model.Nome,
                Cpf = model.Cpf,
                IdCliente = model.IdCliente
            });

            return Json("Cadastro efetuado com sucesso");
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            var bo = new BoBeneficiario();

            if (bo.VerificarExistencia(model.Cpf, model.IdCliente))
                ModelState.AddModelError("CPF", "Este CPF já está cadastrado neste cliente");

            if (!ModelState.IsValid)
            {
                var erros = (from item in ModelState.Values
                             from error in item.Errors
                             select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            bo.Alterar(new Beneficiario
            {
                Id = model.Id,
                Nome = model.Nome,
                Cpf = model.Cpf,
                IdCliente = model.IdCliente
            });

            return Json("Cadastro alterado com sucesso");
        }

        [HttpPost]
        public JsonResult BeneficiarioList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd;
                var campo = string.Empty;
                var crescente = string.Empty;
                var array = jtSorting?.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                var clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }
    }
}