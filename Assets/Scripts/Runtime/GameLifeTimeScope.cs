using BotTestBed.Runtime.Controller;
using Runtime;
using VContainer;
using VContainer.Unity;

namespace BotTestBed.Runtime
{
    public sealed class GameLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameConfiguration>(Lifetime.Singleton).WithParameter(10);
            builder.RegisterComponentOnNewGameObject<ControllerKeyboard>(Lifetime.Scoped).As<ControllerBase>();
            builder.RegisterComponentInHierarchy<Player>();
        }
    }
}
