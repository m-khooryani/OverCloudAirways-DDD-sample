using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;
using MediatR;
using MediatR.Pipeline;

namespace DArch.Infrastructure.Mediation;

public class MediatorModule : Autofac.Module
{
    private readonly Type[] _mediatorOpenTypes;
    private readonly Assembly[] _assemblies;

    public MediatorModule(Assembly[] assemblies,
        Type[] mediatorOpenTypes)
    {
        _assemblies = new List<Assembly>(assemblies)
            {
                ThisAssembly
            }
            .ToArray();
        _mediatorOpenTypes = mediatorOpenTypes;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterSource(new ScopedContravariantRegistrationSource(_mediatorOpenTypes));

        var mediatorOpenTypes = _mediatorOpenTypes;

        foreach (var mediatorOpenType in mediatorOpenTypes)
        {
            builder
                .RegisterAssemblyTypes(_assemblies)
                .AsClosedTypesOf(mediatorOpenType)
                .AsImplementedInterfaces();
        }

        builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        builder.Register<ServiceFactory>(ctx =>
        {
            var c = ctx.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        }).InstancePerLifetimeScope();
    }

    private class ScopedContravariantRegistrationSource : IRegistrationSource
    {
        private readonly IRegistrationSource _source = new ContravariantRegistrationSource();
        private readonly List<Type> _types = new();

        public ScopedContravariantRegistrationSource(params Type[] types)
        {
            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }
            if (!types.All(x => x.IsGenericTypeDefinition))
            {
                throw new ArgumentException("Supplied types should be generic type definitions");
            }
            _types.AddRange(types);
        }

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service,
            Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
        {
            var components = _source.RegistrationsFor(service, registrationAccessor);
            foreach (var c in components)
            {
                var defs = c.Target.Services
                    .OfType<TypedService>()
                    .Select(x => x.ServiceType.GetGenericTypeDefinition());

                if (defs.Any(_types.Contains))
                {
                    yield return c;
                }
            }
        }

        public bool IsAdapterForIndividualComponents => _source.IsAdapterForIndividualComponents;
    }
}
