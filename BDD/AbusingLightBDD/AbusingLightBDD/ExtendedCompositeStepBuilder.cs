using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LightBDD.Core.Extensibility;
using LightBDD.Framework;
using LightBDD.Framework.Extensibility;
using LightBDD.Framework.Scenarios.Contextual;
using LightBDD.Framework.Scenarios.Extended;

namespace AbusingLightBDD
{
    public class ExtendedCompositeStepBuilder<T> : IExtendedCompositeStepBuilder<T>
    {
        private readonly ICompositeStepBuilder<T> _innerBuilder;

        public IServiceProvider ServiceProvider { get; }

        public static ExtendedCompositeStepBuilder<T> Construct(IServiceProvider serviceProvider,
            Func<IServiceProvider, T> contextProvider) =>
            new ExtendedCompositeStepBuilder<T>(serviceProvider, contextProvider);

        public ExtendedCompositeStepBuilder(IServiceProvider serviceProvider, Func<IServiceProvider, T> contextProvider)
        {
            ServiceProvider = serviceProvider;
            _innerBuilder = CompositeStep.DefineNew().WithContext(() => contextProvider(ServiceProvider));
        }

        private ExtendedCompositeStepBuilder(ICompositeStepBuilder<T> innerBuilder, IServiceProvider serviceProvider)
        {
            _innerBuilder = innerBuilder;
            ServiceProvider = serviceProvider;
        }

        public CompositeStep Build()
        {
            return _innerBuilder.Build();
        }

        public IExtendedCompositeStepBuilder<T> AddCompositeStep(CompositeStep compositeStep)
        {
            _innerBuilder.Integrate().AddSteps(Select(compositeStep));
            return this;
        }

        public IExtendedCompositeStepBuilder<T> AddSteps(params Expression<Action<T>>[] steps)
        {
            _innerBuilder.AddSteps(steps);
            return this;
        }

        public IExtendedCompositeStepBuilder<T> AddAsyncSteps(params Expression<Func<T, Task>>[] steps)
        {
            _innerBuilder.AddAsyncSteps(steps);
            return this;
        }

        public IExtendedCompositeStepBuilder<TNewContext> WithUpdatedContext<TNewContext>(
            Func<TNewContext> contextProvider)
        {
            return new ExtendedCompositeStepBuilder<TNewContext>(
                CompositeStep.DefineNew().WithContext(contextProvider),
                ServiceProvider).AddCompositeStep(_innerBuilder.Build());
        }

        private IEnumerable<StepDescriptor> Select(CompositeStep compositeStep)
        {
            return compositeStep.SubSteps.Select(step => new StepDescriptor(string.Empty, step.RawName, (o, objects) =>
            {
                var compositeStepSubStepsContextProvider = compositeStep.SubStepsContextProvider();
                return step.StepInvocation(compositeStepSubStepsContextProvider, step.Parameters);
            }));
        }
    }
}