namespace br.com.fiap.cloudgames.Domain.UnitsOfWork;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();

    Task CommitAsync();

    Task RollbackAsync();
}