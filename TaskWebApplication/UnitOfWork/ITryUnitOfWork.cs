using TaskWebApplication.Repository;

namespace TaskWebApplication.UnitOfWork
{
    public interface ITryUnitOfWork : IDisposable
    {
        IToDoListRepository toDoListRepository { get; }

        IUserRepository userRepository { get; }

        int effectedRows();
    }
}
