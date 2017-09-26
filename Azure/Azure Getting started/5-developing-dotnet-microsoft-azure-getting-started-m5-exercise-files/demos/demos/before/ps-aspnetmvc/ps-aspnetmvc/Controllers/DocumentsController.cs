using PSAspNetMvc.Data;
using PSAspNetMvc.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PSAspNetMvc.Controllers
{
    public class DocumentsController : Controller
    {
        public DocumentsController()
        {
            _store = new CourseStore();
        }

        public ActionResult Index()
        {
            var model = _store.GetAllCourses();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Insert()
        {
            var data = new SampleData().GetCourses();
            await _store.InsertCourses(data);

            return RedirectToAction(nameof(Index));
        }

        CourseStore _store;
    }
}