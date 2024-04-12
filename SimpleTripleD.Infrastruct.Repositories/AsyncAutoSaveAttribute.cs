using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleTripleD.Domain.Entities;

namespace SimpleTripleD.Infrastruct.Repositories
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AsyncAutoSaveAttribute<TAggregateRoot> : Attribute, IAsyncActionFilter
        where TAggregateRoot : IAggregateRoot
    {
        private readonly IUnitOfWork _unitOfWork;

        public AsyncAutoSaveAttribute(IUnitOfWorkAccessor<TAggregateRoot> unitOfWorkAccessor)
            => _unitOfWork = unitOfWorkAccessor.UnitOfWork;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var transaction = await _unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            if (transaction is null)
                throw new InvalidOperationException("Transaction is already exist.");
            var executed = await next().ConfigureAwait(false);
            if (executed.Exception is null)
                await _unitOfWork.CommitTransactionAsync(transaction).ConfigureAwait(false);
        }
    }
}
