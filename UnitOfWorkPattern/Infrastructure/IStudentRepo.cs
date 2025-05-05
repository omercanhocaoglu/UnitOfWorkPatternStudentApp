using System.Collections.Generic;
using UnitOfWorkPattern.Models;

namespace UnitOfWorkPattern.Infrastructure
{
    public interface IStudentRepo
    {
        IList<Student> GetAll();
        Student GetById(int Id);
        void Insert(Student student);
        void Update(Student student);
        void Delete(Student student);   
    }
}
