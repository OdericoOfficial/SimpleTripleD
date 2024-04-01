using FluentValidation;
using MediatR;
using SimpleTripleD.Domain.Commands;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddValidationCommand<TCommand, TValidator, THandler>(this IServiceCollection services)
            where TCommand : ValidationCommand
            where TValidator : CommandValidator<TCommand>
            where THandler : CommandHandler<TCommand>
        {
            services.AddTransient<IValidator<TCommand>, TValidator>();
            services.AddTransient<TCommand>();
            services.AddScoped<IRequestHandler<TCommand, bool>, THandler>();
            return services;
        }
    }
}
