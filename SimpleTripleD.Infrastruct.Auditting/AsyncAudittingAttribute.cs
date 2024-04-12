using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleTripleD.Domain.Auditting;
using SimpleTripleD.Infrastruct.Repositories;
using System.Diagnostics;
using System.Text.Json;

namespace SimpleTripleD.Infrastruct.Auditting
{
    public class AsyncAudittingAttribute: ActionFilterAttribute
    {
        private readonly IAudttingRecordFactory _factory;
        private readonly IRepository<AudittingRecord> _repository;

        public AsyncAudittingAttribute(IAudttingRecordFactory factory, IRepository<AudittingRecord> repository)
        {
            _factory = factory;
            _repository = repository;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? exception = null;
            var executionTime = DateTime.UtcNow;
            var clientIpAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            var browserInfo = context.HttpContext.Request.Headers["User-Agent"].ToString();
            var httpMethod = context.HttpContext.Request.Method;
            var url = context.HttpContext.Request.GetDisplayUrl();
            var parameter = JsonSerializer.Serialize(context.ActionArguments);
            _factory.SetParameterInfo(parameter);

            var stopwatch = Stopwatch.StartNew();
            var executed = await next().ConfigureAwait(false);
            stopwatch.Stop();

            if (executed.Exception is not null)
                exception = executed.Exception.Message;
            
            var executionDuration = stopwatch.ElapsedMilliseconds;
            var httpStateCode = executed.HttpContext.Response.StatusCode;

            var record = _factory.Create(clientIpAddress, browserInfo, httpMethod, httpStateCode,
                url, executionTime, executionDuration, exception);
            await _repository.InsertAsync(record).ConfigureAwait(false);
        }
    }
}
