using Microsoft.AspNetCore.Mvc;

using OrderManagementMVC.Models;
using OrderManagementMVC.Repositories;

namespace OrderManagementMVC.Controllers
{
    public class LabelsController : Controller
    {
        private readonly LabelsRepository _repository;
        public LabelsController(LabelsRepository repository)
        {
            _repository = repository;
        }

        // GET: Labels
        public IActionResult Index()
        {
            var labels = _repository.GetAllLabels();
              return labels != null ? 
                          View("Index", labels) :
                          Problem("Entity set 'OrderManagementContext.Labels' is null.");
        }

        // GET: Labels/Details/5
        public IActionResult Details(int id)
        {
            var label = _repository.GetLabelById(id);
            return label == null ? NotFound() : View("Details", label);
        }

        // GET: Labels/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            LabelsModel label = new LabelsModel();
            await TryUpdateModelAsync(label);
            _repository.AddLabel(label);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var label = _repository.GetLabelById(id);
            return label == null ? NotFound() : View("Edit",label);
        }

        // POST: Labels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            LabelsModel label = _repository.GetLabelById(id);
            TryUpdateModelAsync(label);
            _repository.UpdateLabel(id);
            return RedirectToAction("Index");            
        }

        // GET: Labels/Delete/5
        public IActionResult Delete(int id)
        {
            var label = _repository.GetLabelById(id);
            return label == null ? NotFound() : View("Delete", label);
        }

        // POST: Labels/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            var labels = _repository.GetLabelById(id);
            if (labels != null)
            {
                _repository.DeleteLabel(id);
            }
            return RedirectToAction("Index");
        }        
    }
}
