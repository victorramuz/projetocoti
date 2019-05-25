using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities; //entidades
using System.Configuration; //connectionstring
using System.Data.SqlClient; //acesso ao sqlserver
using Dapper; //framework para mapeamento de banco

namespace Projeto.Repository
{
    public class ClienteRepository
    {
        //atributo..
        private readonly string connectionString;      

        //construtor -> ctor + 2x[tab]
        public ClienteRepository()
        {
            //inicializar o atributo readonly
            connectionString = ConfigurationManager
                .ConnectionStrings["projeto"].ConnectionString;
        }

        //método para inserir um cliente na base
        public void Insert(Cliente cliente)
        {            
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "insert into Cliente(Nome, Email) "
                             + "values(@Nome, @Email)";

                //executando..
                conn.Execute(query, cliente);
            }
        }

        //método para atualizar um cliente na base
        public void Update(Cliente cliente)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "update Cliente set Nome = @Nome, Email = @Email "
                             + "where IdCliente = @IdCliente";

                //executando..
                conn.Execute(query, cliente);
            }
        }

        //método para excluir um cliente na base
        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "delete from Cliente where IdCliente = @IdCliente";

                //executando..
                conn.Execute(query, new { IdCliente = id });
            }
        }

        //método para consultar todos os clientes
        public List<Cliente> FindAll()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "select * from Cliente";

                //executando..
                return conn.Query<Cliente>(query).ToList();
            }
        }

        //método para consultar 1 cliente pelo id
        public Cliente FindById(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "select * from Cliente where IdCliente = @IdCliente";

                //executando..
                //SingleOrDefault -> retorna apenas 1 registro e se nenhum for
                //encontrado retorna null. Se a consulta obtiver mais de 1 registro
                //o SingleOrDefault lança uma exceção
                return conn.QuerySingleOrDefault<Cliente>
                    (query, new { IdCliente = id });
            }
        }

        //método para verificar se um determinado 
        //email já está cadastrado no banco
        public bool HasEmail(string email)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "select count(Email) from Cliente "
                          + "where Email = @Email";

                var qtd = conn.QuerySingleOrDefault<int>
                    (query, new { Email = email });

                return qtd == 1;
            }
        }

    }
}
