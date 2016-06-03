using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers
{
    [Authorize]
    public class ComentariosController : Controller
    {
        private SistemaDBContext db = new SistemaDBContext();

        // GET: Comentarios
        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Reclamacao);
            return View(comentarios.ToList());
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comentario comentario = db.Comentarios.Find(id);
            comentario.Data.ToShortDateString();
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // GET: Comentarios/Create
        public ActionResult Create(int id)
        {
            
            var comment = new Comentario() { ReclamacaoID = id };
            return View(comment);
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComentarioID,Descricao,Data,ReclamacaoID,ImageFile,ImageMimeType")] Comentario comentario, int id, HttpPostedFileBase image)
        {
            
            comentario.Data = DateTime.Now;
            comentario.Data.ToShortDateString();
            if (image != null)
            {
                comentario.ImageMimeType = image.ContentType;
                comentario.ImageFile = new byte[image.ContentLength];
                image.InputStream.Read(comentario.ImageFile, 0, image.ContentLength);
            }
            if (ModelState.IsValid)
            {
                
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Details","Reclamacaos" , new { id = comentario.ReclamacaoID });
            }
            
                
            return View();
        }
        
        // GET: Comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReclamacaoID = new SelectList(db.Reclamacoes, "ReclamacaoID", "Titulo", comentario.ReclamacaoID);
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComentarioID,Descricao,Data,ReclamacaoID,ImageFile,ImageMimeType")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReclamacaoID = new SelectList(db.Reclamacoes, "ReclamacaoID", "Titulo", comentario.ReclamacaoID);
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentario);
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

        public ActionResult GetImage(int id)
        {
            Comentario rec = db.Comentarios.Find(id);
            if (rec != null && rec.ImageFile != null)
            {
                return File(rec.ImageFile, rec.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
