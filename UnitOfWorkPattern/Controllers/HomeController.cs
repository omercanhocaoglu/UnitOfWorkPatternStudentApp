using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UnitOfWorkPattern.Infrastructure;
using UnitOfWorkPattern.Models;


namespace UnitOfWorkPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var students = _unitOfWork.StudentRepo.GetAll();
            return View(students); // Burası View döner
        }

        [HttpGet]
        public IActionResult GetStudents(string search)
        {
            var students = _unitOfWork.StudentRepo.GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                students = students
                    .Where(s => s.FullName.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var result = students.Select(s => new
            {
                s.Id,
                FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.FullName.ToLower()),
                City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.City.ToLower())
            });

            return Json(result); // Bu sadece Ajax için
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            try
            {
                _unitOfWork.StudentRepo.Insert(student);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch (Exception exception) 
            {
                Console.WriteLine(exception.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Delete(int Id) 
        {
            var student = _unitOfWork.StudentRepo.GetById(Id);
            if (student == null) 
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        public IActionResult Delete(Student student)
        {
            try
            {
                var exitingStudent = _unitOfWork.StudentRepo.GetById(student.Id);
                if (exitingStudent == null)
                {
                    return NotFound();
                }
                _unitOfWork.StudentRepo.Delete(exitingStudent);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Update(int Id)
        {
            var student = _unitOfWork.StudentRepo.GetById(Id);
            if (student == null) 
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        public IActionResult Update(Student student)
        {
            try
            {
                var exitingStudent = _unitOfWork.StudentRepo.GetById(student.Id);
                if (exitingStudent == null)
                {
                    return NotFound();
                }
                exitingStudent.FullName = student.FullName;
                exitingStudent.City = student.City;
                _unitOfWork.StudentRepo.Update(exitingStudent);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
