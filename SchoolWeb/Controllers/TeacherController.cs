using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolWeb.Data;
using SchoolWeb.Models;

namespace SchoolWeb.Controllers
{
    public class TeacherController : Controller
    {
        /* 
         Ask to the dependency injection container the AppDbContext object to access the database.
         This AppDbContext object was registered inside DI Container when the DB Context service was added in Program.cs (the main program).
         Then, inside controller constructor the AppDbContext implementation will be asked.
        */
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            // Keep Db Context on local object to access data later
            _context = context;
        }

        // GET: Teacher
        public async Task<IActionResult> Index()
        {
            return _context.Teachers != null ?
                View(await _context.Teachers.OrderBy(t => t.Name).ToListAsync()) :
                Problem("Entity set 'AppDbContext.Teachers' is null.");
        }

        // GET: Teacher/Create
        public IActionResult Create()
        {
            // Return only the view without data
            return View();
        }

        // POST: Teacher/Create
        // To protect from over-posting attacks, enable the specific properties you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherModel teacher)
        {
            // Check if the teacher's name already exists in the database.
            if (!string.IsNullOrWhiteSpace(teacher.Name))
            {
                var sameTeacherCounter = (from t in _context.Teachers
                                          where t.Name == teacher.Name
                                          select t).Count();

                if (sameTeacherCounter > 0)
                    ModelState.AddModelError("", $"{teacher.Name} already exists in the database.");
            }
            // Model state represents errors that come from two subsystems: model binding and model validation. 
            if (!ModelState.IsValid) return View(teacher);

            // Add a new entity to this context (_db), which will insert a new record in the database when you call the SaveChangesAsync() method.
            _context.Add(teacher);
            // Saves all changes made in this context to the database
            await _context.SaveChangesAsync();
            // Reporting success. TempData is used to pass data between two consecutive requests.
            // Visit https://www.red-gate.com/simple-talk/blogs/what-is-viewdata-and-implement-viewdata-in-asp-net-mvc/
            TempData["success"] = $"{this.GetType().Name.Replace("Controller", "")} added successfully.";
            // Return redirection to the index view, which has the item list 
            return RedirectToAction(nameof(Index));
        }

        // GET: Teacher/Edit/5
        [HttpGet]
        [Route("Edit/{id:int:min(1)}")]             // Attribute routing with combination of constraints
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teachers == null) return NotFound();

            // Retrieve teacher from DB
            TeacherModel? teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();

            // Return found teacher
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/{id:int:min(1)}")]             // Attribute routing with combination of constraints
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeacherModel teacher)
        {
            // Check if the teacher's name already exists in the database.
            if (!string.IsNullOrWhiteSpace(teacher.Name))
            {
                Task<int> sameTeacherCounter = (from t in _context.Teachers
                    where t.Name == teacher.Name && t.Id != teacher.Id
                    select 5).CountAsync();

                if (sameTeacherCounter.Result > 0)
                    ModelState.AddModelError("", $"{teacher.Name} already exists in the database.");
            }

            // Model state represents errors that come from two subsystems: model binding and model validation. 
            if (!ModelState.IsValid) return View(teacher);

            try
            {
                teacher.Gender = teacher.Gender.ToUpper();
                // Update updating date and time
                teacher.UpdatedOn = DateTime.Now;
                // Modify existent entity in this context (_db), which will update the record in the database when you call the SaveChangesAsync() method.
                _context.Update(teacher);
                // Saves all changes made in this context to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(teacher.Id)) return NotFound();
                throw;
            }

            // Reporting success. TempData is used to pass data between two consecutive requests.
            // Visit https://www.red-gate.com/simple-talk/blogs/what-is-viewdata-and-implement-viewdata-in-asp-net-mvc/
            TempData["success"] = $"{this.GetType().Name.Replace("Controller", "")} updated successfully.";
            // Return redirection to the index view, which has the item list 
            return RedirectToAction(nameof(Index));
        }

        // GET: Teacher/Delete/5
        [HttpGet]
        [Route("Delete/{id:int:min(1)}")]           // Attribute routing with combination of constraints
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            // Retrieve teacher from DB
            TeacherModel? teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null) return NotFound();

            // Return found teacher
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost]   //, ActionName("Delete")]
        [Route("Delete/{id:int:min(1)}")]           // Attribute routing with combination of constraints
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Retrieve teacher from DB
            var teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null) return NotFound();

            // Mark existent entity as deleted in this context (_db), which will delete the record in the database when
            //  you call the SaveChangesAsync() method.
            _context.Remove(teacher);
            // Saves all changes made in this context to the database
            await _context.SaveChangesAsync();
            // Reporting success. TempData is used to pass data between two consecutive requests.
            // Visit https://www.red-gate.com/simple-talk/blogs/what-is-viewdata-and-implement-viewdata-in-asp-net-mvc/
            TempData["success"] = $"{this.GetType().Name.Replace("Controller", "")} deleted successfully.";
            // Return redirection to the index view, which has the item list 
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
          return (_context.Teachers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
