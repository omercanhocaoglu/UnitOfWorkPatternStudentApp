using UnitOfWorkPattern.Data;
using UnitOfWorkPattern.Infrastructure;

namespace UnitOfWorkPattern.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context _context;
        private IStudentRepo _studentRepo;

        public UnitOfWork(Context context)
        {
            _context = context;
        }
        public IStudentRepo StudentRepo
        {
            get 
            {
                return _studentRepo = _studentRepo ?? new StudentRepo(_context);
            } 
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
