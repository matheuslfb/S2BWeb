using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoFinal.Models;
using ProjetoFinal.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ProjetoFinal.Migrations;


namespace ProjetoFinal.Controllers
{
    public class ReclamacaosController : Controller
    {
        private SistemaDBContext db = new SistemaDBContext();

        // GET: Reclamacaos
        /*public ActionResult Index()
        {
            var reclamacoes = db.Reclamacoes.Include(r => r.Categoria);
            return View(reclamacoes.ToList());
        }*/
        public ViewResult Index(string searchString, int? SelectedCategoria)
        {
            var cat = db.Categorias.OrderBy(g => g.Titulo).ToList();
            ViewBag.SelectedCategoria = new SelectList(cat, "CategoriaID", "Titulo", SelectedCategoria);
            int catID = SelectedCategoria.GetValueOrDefault();
            var rec = db.Reclamacoes.Where(c => !SelectedCategoria.HasValue || c.CategoriaID == catID);
            //var rec = from Reclamacao in db.Reclamacoes select Reclamacao;
            if (!String.IsNullOrEmpty(searchString))
            {
                rec = rec.Where(s => s.Titulo.Contains(searchString));
            }
            return View(rec.ToList());
        }

        // GET: Reclamacaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            if (reclamacao == null)
            {
                return HttpNotFound();
            }
            return View(reclamacao);
        }

        // GET: Reclamacaos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Titulo");
            ViewBag.DataRequisito = DateTime.Now.ToShortDateString();
            return View();
        }

        // POST: Reclamacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReclamacaoID,Titulo,Descricao,Status,DataRequisicao,ComentarioID,CategoriaID,ImageFile,ImageMimeType")] Reclamacao reclamacao, HttpPostedFileBase image)
        {
            reclamacao.DataRequisicao = DateTime.Now;
            if (image != null)
            {
                reclamacao.ImageMimeType = image.ContentType;
                reclamacao.ImageFile = new byte[image.ContentLength];
                image.InputStream.Read(reclamacao.ImageFile, 0, image.ContentLength);
            }
            if (ModelState.IsValid)
            {

                db.Reclamacoes.Add(reclamacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Titulo", reclamacao.CategoriaID);
            return View(reclamacao);
        }

        // GET: Reclamacaos/Edit/5
        [Authorize (Users = User.Identity.Name.ToString(), Roles ="Admin")]
        public ActionResult Edit(int? id)
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
            Reclamacao reclamacao = db.Reclamacoes.Find(id);

            if (User.Identity.Name.ToString() == reclamacao.Usuario || user.Tipo == TipoUsuario.adm)
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (reclamacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Titulo", reclamacao.CategoriaID);
            }
            return View(reclamacao);
        }

        // POST: Reclamacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int ReclamacaoID, string Titulo, string Descricao, Status Status, DateTime DataRequisicao, int CategoriaID, HttpPostedFileBase image)
        {
            var rec = db.Reclamacoes.Find(ReclamacaoID);
            if (ModelState.IsValid)
            {
                rec.Titulo = Titulo;
                rec.Descricao = Descricao;
                rec.Status = Status;
                rec.DataRequisicao = DataRequisicao;
                rec.CategoriaID = CategoriaID;
                if (image != null || rec.ImageFile != null || rec.ImageMimeType != null)
                {
                    rec.ImageMimeType = image.ContentType;
                    rec.ImageFile = new byte[image.ContentLength];
                    image.InputStream.Read(rec.ImageFile, 0, image.ContentLength);
                }

                db.Entry(rec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Titulo", rec.CategoriaID);
            return View(rec);
        }

        // GET: Reclamacaos/Delete/5
        [Authorize(Users = "adm@s2b.pucrs.br, smov@s2b.pucrs.br,  sman@s2b.pucrs.br")]
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            if (reclamacao == null)
            {
                return HttpNotFound();
            }
            return View(reclamacao);

        }

        // POST: Reclamacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            Comentario coment = db.Comentarios.Find(reclamacao.ReclamacaoID);
            if (coment != null)
            {
                db.Comentarios.Remove(coment);
            }
            db.Reclamacoes.Remove(reclamacao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Browse(int id = 1)
        {
            var catModel = db.Reclamacoes.Include("Comentarios").Single(g => g.ReclamacaoID == id);

            return View(catModel);
        }



        public ActionResult GetImage(int id)
        {
            Reclamacao rec = db.Reclamacoes.Find(id);
            if (rec != null && rec.ImageFile != null)
            {
                return File(rec.ImageFile, rec.ImageMimeType);
            }
            else
            {
                return new FilePathResult("~/Images/nao-disponivel.jpg", "image/jpeg");
            }
        }

        public ActionResult MinhasReclamacoes(string name)
        {

            var rec = db.Reclamacoes.Where(c => c.Usuario == name);
            return View(rec.ToList());
        }

        public ActionResult ConsultaBairro()
        {

            var data = from rec in db.Reclamacoes
                       group rec by rec.Categoria into dateGroup
                       select new GenreRecInfo()
                       {
                           name = dateGroup.Key.Titulo,
                           count = dateGroup.Count(),
                       };
            return View(data.ToList());


        }
    }
}
