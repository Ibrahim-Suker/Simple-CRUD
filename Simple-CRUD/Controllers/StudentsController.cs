using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Simple_CRUD.Data;
using Simple_CRUD.Models;
using System.Threading.Tasks;

namespace Simple_CRUD.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentsController(ApplicationDbContext db)
        {
            _db = db;
        }


        // 🔹 Add Student - GET
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        // 🔹 Add Student - POST
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Subscribed = viewModel.Subscribed
                };
                await _db.Students.AddAsync(student);
                await _db.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(viewModel);
        }



        // 🔹 List Students - GET
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await _db.Students.ToListAsync();
            return View(students);
        }



        // 🔹 Edit Student - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id) // 🔥 Fixed issue: 'Key' was incorrect
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        // 🔹 Edit Student - POST
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            if (ModelState.IsValid)
            {
                var student = await _db.Students.FindAsync(viewModel.Id);
                if (student != null)
                {
                    student.Name = viewModel.Name;
                    student.Email = viewModel.Email;
                    student.Subscribed = viewModel.Subscribed;
                    await _db.SaveChangesAsync();
                    return RedirectToAction("List");
                }
            }
            return View(viewModel);
        }



        // 🔹 Delete Student - GET (Confirmation Page)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        // 🔹 Delete Student - POST (Confirmed)
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student != null)
            {
                _db.Students.Remove(student);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }
    }
}