using Microsoft.AspNetCore.Mvc;
using SchoolWeb.Data;
using SchoolWeb.Models;

namespace SchoolWeb.Controllers
{
    public class TeacherController : Controller
    {
        /* 
         * Ask to the dependency injection container the AppDbContext object to access the 
         * database. This AppDbContext object was registered inside DI Container when the 
         * DB Context service was added in Program.cs (the main program).
         * Inside controller constructor an AppDbContext implementation will be asked 
         */
        private readonly AppDbContext _db;
        
        public TeacherController(AppDbContext db)
        {
            // Keep Db Context on local object to access data later
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Models.Teacher>? _teachers = _db.Teachers;
            return View(_teachers);
        }

        public IActionResult Create()
        {
            // Return only the view without data
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]          // Prevents Cross-site request forgery attack (also known as XSRF or CSRF)
        public IActionResult Create(Teacher obj)
        {
            // Check if the teacher's name already exists in the database.
            if (!string.IsNullOrWhiteSpace(obj.Name))
            {
                var sameTeacherCounter = (from teacher in _db.Teachers
                                            where teacher.Name == obj.Name
                                            select teacher).Count();

                if (sameTeacherCounter > 0)
                    ModelState.AddModelError("Name", $"{obj.Name} already exists in the database.");
            }
                        
            // Model state represents errors that come from two subsystems: model binding and model validation. 
            if (!ModelState.IsValid) return View(obj);
            
            // Add a new entity to this context (_db), which will insert a new record in the database when you call the SaveChanges() method.
            _db.Teachers.Add(obj);
            // Saves all changes made in this context to the database
            _db.SaveChanges();
            // Return redirection to the index view, which has the item list 
            return RedirectToAction("Index");                  
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            // Retrieve teacher from DB
            Teacher? obj = _db.Teachers.FirstOrDefault(t => t.Id == id);
            if (obj == null) return NotFound();

            // Return found teacher
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]          // Prevents Cross-site request forgery attack (also known as XSRF or CSRF)
        public IActionResult Edit(Teacher obj)
        {
            // Check if the teacher's name already exists in the database.
            if (!string.IsNullOrWhiteSpace(obj.Name))
            {
                var sameTeacherCounter = (from teacher in _db.Teachers
                                            where teacher.Name == obj.Name && teacher.Id != obj.Id
                                            select teacher).Count();

                if (sameTeacherCounter > 0)
                    ModelState.AddModelError("Name", $"{obj.Name} already exists in the database.");
            }
                        
            // Model state represents errors that come from two subsystems: model binding and model validation. 
            if (!ModelState.IsValid) return View(obj);
            
            // Modify existent entity in this context (_db), which will update the record in the database when you call the SaveChanges() method.
            _db.Teachers.Update (obj);
            // Saves all changes made in this context to the database
            _db.SaveChanges();
            return RedirectToAction("Index");                  
            // Return redirection to the index view, which has the item list 
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            // Retrieve teacher from DB
            Teacher? persistedTeacher = _db.Teachers.FirstOrDefault(t => t.Id == id);
            if (persistedTeacher == null) return NotFound();

            // Return found teacher
            return View(persistedTeacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]          // Prevents Cross-site request forgery attack (also known as XSRF or CSRF)
        public IActionResult DeletePost(int? id)
        {
            // Retrieve teacher from DB
            Teacher? obj = _db.Teachers.FirstOrDefault(t => t.Id == id);
            if (obj == null) return NotFound();

            // Mark existent entity as deleted in this context (_db), which will delete the record in the database when you call the SaveChanges() method.
            _db.Teachers.Remove (obj);
            // Saves all changes made in this context to the database
            _db.SaveChanges();
            // Return redirection to the index view, which has the item list 
            return RedirectToAction("Index");                  
        }
    }
}
