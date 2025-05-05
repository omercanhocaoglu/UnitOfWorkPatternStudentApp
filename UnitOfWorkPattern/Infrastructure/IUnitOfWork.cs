namespace UnitOfWorkPattern.Infrastructure
{
    public interface IUnitOfWork
    {
        IStudentRepo StudentRepo { get; }
        void Save();
    }
}
