using CamadaDeControle.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace CamadaDeControle.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lista(double? precoMinimo,double? precoMaximo)
        {
            K19Context ctx = new K19Context();
            var produtos = ctx.Produtos.AsEnumerable();

            if(precoMinimo != null && precoMaximo != null)
            {
                produtos = produtos.Where(p => p.Preco >= precoMinimo && p.Preco <= precoMaximo).ToList();
            }

            return View(produtos);
        }

        [HttpGet]
        public ActionResult Cadastra()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastra(Produto p)
        {
            K19Context ctx = new K19Context();
            ctx.Produtos.Add(p);
            ctx.SaveChanges();

            TempData["Mensagem"] = "Produto cadastrado com sucesso!";
            TempData["Produto"] = p;

            return RedirectToAction("Lista");
        }

        public ActionResult Editar(int id = 0)
        {
            K19Context ctx = new K19Context();
            Produto p = ctx.Produtos.Find(id);
            if(p == null)
            {
                return HttpNotFound();
            }

            return View(p);
        }

        [HttpPost]
        public ActionResult Editar(Produto p)
        {
            K19Context ctx = new K19Context();
            ctx.Entry(p).State = EntityState.Modified;

            ctx.SaveChanges();

            return RedirectToAction("Lista");
        }
    }
}