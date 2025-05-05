using System.Collections.Generic;
using System.Linq;
using UnitOfWorkPattern.Data;
using UnitOfWorkPattern.Infrastructure;
using UnitOfWorkPattern.Models;

namespace UnitOfWorkPattern.Services
{
    public class StudentRepo : IStudentRepo
    {
        private Context _context;

        // This is constructure of Context
        public StudentRepo(Context context)
        {
            _context = context;
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
        }

        public IList<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetById(int Id)
        {
            var student = _context.Students.Where( x => x.Id == Id).FirstOrDefault();
            return student;
        }

        public void Insert(Student student)
        {
            _context.Students.Add(student);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }
    }
}
