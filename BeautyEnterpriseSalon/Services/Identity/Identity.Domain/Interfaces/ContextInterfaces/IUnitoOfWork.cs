namespace Identity.Domain.Interfaces.ContextInterfaces;

public interface IUnitoOfWork
{
    Task<int> Commit();
}
