namespace SimpleTripleD.Infrastruct.Repositories
{
    public interface IUnitOfWorkAccessor<TAggregateRoot>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
