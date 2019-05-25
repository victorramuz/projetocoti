using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto.Entities;
using Projeto.Presentation.Models;
using AutoMapper;

namespace Projeto.Presentation.Mappings
{
    public class AutoMapperConfig : Profile
    {
        //construtor
        public AutoMapperConfig()
        {
            //mapeamento para o processo de cadastro
            CreateMap<ClienteCadastroViewModel, Cliente>();

            //mapeamento para o processo de consulta
            CreateMap<Cliente, ClienteConsultaViewModel>();
        }
    }
}