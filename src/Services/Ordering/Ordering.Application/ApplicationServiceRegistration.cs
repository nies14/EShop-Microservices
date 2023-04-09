using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using System.Reflection;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //will look for classes that is inherited from Profile class
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //will look for classes that is inherited from AbstractValidator class
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //will look for classes that is inherited from IRequestHandler & IRequest class
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
