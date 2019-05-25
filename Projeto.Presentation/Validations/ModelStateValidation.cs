using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Presentation.Validations
{
    public class ModelStateValidation
    {
        public static Hashtable GetErrors(ModelStateDictionary ModelState)
        {
            Hashtable resultado = new Hashtable();

            foreach(var state in ModelState)
            {
                if(state.Value.Errors.Count > 0)
                {
                    resultado[state.Key] = state.Value.Errors
                                            .Select(e => e.ErrorMessage)
                                            .ToList();
                }
            }

            return resultado;
        }
    }
}