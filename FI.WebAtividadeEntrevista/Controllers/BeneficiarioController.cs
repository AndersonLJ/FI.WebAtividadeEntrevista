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
        public JsonResult BeneficiarioList(int idCliente)
        {
            try
            {
                var beneficiarios = new BoBeneficiario().Pesquisa(idCliente);

                return Json(new { Result = "OK", Records = beneficiarios });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", ex.Message });
            }
        }
    }
}