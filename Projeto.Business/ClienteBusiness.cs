using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities; //importando
using Projeto.Repository; //importando

namespace Projeto.Business
{
    public class ClienteBusiness
    {
        //atributo..
        private readonly ClienteRepository repository;

        //construtor -> ctor + 2x[tab]
        public ClienteBusiness()
        {
            repository = new ClienteRepository();
        }

        public void Cadastrar(Cliente cliente)
        {            
            //verificando se o email do cliente já está
            //cadastrado na base de dados
            if(repository.HasEmail(cliente.Email))
            {
                //forçar na camada de apresentação
                throw new Exception($"O email {cliente.Email} já foi cadastrado. Tente outro.");
            }
            else
            {
                repository.Insert(cliente);
            }            
        }

        public void Atualizar(Cliente cliente)
        {
            repository.Update(cliente);
        }

        public void Excluir(int id)
        {
            repository.Delete(id);
        }

        public List<Cliente> ObterTodos()
        {
            return repository.FindAll();
        }

        public Cliente ObterPorId(int id)
        {
            return repository.FindById(id);
        }
    }
}
