using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Entities; //importando..
using Projeto.Business; //importando..
using Projeto.Presentation.Models; //importando..
using AutoMapper; //importando..
using Projeto.Presentation.Validations; //importando..

namespace Projeto.Presentation.Controllers
{
    public class ClienteController : Controller
    {
        //atributo..
        private readonly ClienteBusiness business;

        //construtor..
        public ClienteController()
        {
            business = new ClienteBusiness();
        }

        // GET: Cliente/Cadastro
        public ActionResult Cadastro()
        {
            return View();
        }

        // GET: Cliente/Consulta
        public ActionResult Consulta()
        {
            return View();
        }

        //JsonResult -> recebe requisições JavaScript
        public JsonResult CadastrarCliente(ClienteCadastroViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //convertendo os dados da model para cliente..
                    var cliente = Mapper.Map<Cliente>(model);

                    //gravando..
                    business.Cadastrar(cliente);

                    Response.StatusCode = 200; //OK (Sucesso)
                    return Json($"Cliente {cliente.Nome} cadastrado com sucesso.");
                }
                catch(Exception e)
                {
                    Response.StatusCode = 500; //Internal Server Error
                    return Json(e.Message);
                }
            }
            else
            {
                Response.StatusCode = 400; //BadRequest (Requisição inválida)
                return Json(ModelStateValidation.GetErrors(ModelState));
            }
        }

        //método executado pelo jquery para consultar os clientes..
        public JsonResult ConsultarClientes()
        {
            try
            {
                //executar a consulta de clientes na camada de negócio
                var clientes = business.ObterTodos();

                //transformar para uma lista da classe ViewModel
                var model = Mapper.Map<List<ClienteConsultaViewModel>>(clientes);

                Response.StatusCode = 200; //OK
                return Json(model); //enviado para a página..
            }
            catch(Exception e)
            {
                Response.StatusCode = 500; //Erro interno de Servidor
                return Json(e.Message);
            }
        }

    }
}