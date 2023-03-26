using Domain.InterfacesExternal;
using Entities.EntitiesExternal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProduto _iProduto;

        public ProdutosController(IProduto iProduto)
        {
            _iProduto = iProduto;
        }

        [Produces("application/json")]
        [HttpGet("/api/ListProd")]
        public List<Produto> ListProd()
        {
            return _iProduto.List();
        }
    }
}
