using Microsoft.AspNetCore.Mvc;

namespace Loja.Controllers
{
    public class CadastrosController : Controller
    {
        public IActionResult Produto()
        {
            return View();
        }
    }
}
