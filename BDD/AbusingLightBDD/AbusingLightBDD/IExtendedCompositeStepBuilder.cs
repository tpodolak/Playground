using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LightBDD.Framework;

namespace AbusingLightBDD
{
    public interface IExtendedCompositeStepBuilder<T>
    {
        IServiceProvider ServiceProvider { get; }

        CompositeStep Build();

        IExtendedCompositeStepBuilder<T> AddCompositeStep(CompositeStep compositeStep);

        IExtendedCompositeStepBuilder<T> AddSteps(params Expression<Action<T>>[] steps);

        IExtendedCompositeStepBuilder<T> AddAsyncSteps(params Expression<Func<T, Task>>[] steps);

        IExtendedCompositeStepBuilder<TNewContext> WithUpdatedContext<TNewContext>(Func<TNewContext> contextProvider);
    }
}