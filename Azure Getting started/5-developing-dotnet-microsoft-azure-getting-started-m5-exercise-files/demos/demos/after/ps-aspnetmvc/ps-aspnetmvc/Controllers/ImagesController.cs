using PSAspNetMvc.Data;
using PSAspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PSAspNetMvc.Controllers
{
    // [Authorize]
    public class ImagesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(HttpPostedFileBase image)
        {
            if (image != null)
            {
                var imageId = await _store.SaveImage(image.InputStream);
                return RedirectToAction("Show", new { id = imageId });
            }
            return View();
        }

        public ActionResult Show(string id)
        {
            var model = new ShowModel { Uri = _store.UriFor(id) };
            return View(model);
        }

        ImageStore _store = new ImageStore();
    }
}