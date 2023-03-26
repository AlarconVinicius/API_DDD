using Domain.InterfacesExternal;
using Entities.EntitiesExternal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryExternal
{
    public class RepositoryProduto : IProduto
    {
        private readonly string urlApi = "https://servicodados.ibge.gov.br/api/v1/produtos/";
        public List<Produto> List()
        {
            var retorno = new List<Produto>();

            try
            {
                using(var client = new HttpClient())
                {
                    var resposta = client.GetStringAsync(urlApi);
                    resposta.Wait();
                    retorno = JsonConvert.DeserializeObject<Produto[]>(resposta.Result).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }
    }
}
