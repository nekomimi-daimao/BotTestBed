using BotTestBed.Runtime.Controller;
using Runtime.Executor;
using VContainer;
using VContainer.Unity;

namespace Runtime
{
    public sealed class GameLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentOnNewGameObject<GameManager>(Lifetime.Scoped);
            builder.Register<GameStateExecutor>(Lifetime.Scoped);
            builder.Register<GameConfiguration>(Lifetime.Singleton).WithParameter(10);

            builder.RegisterComponentOnNewGameObject<ControllerKeyboard>(Lifetime.Scoped).As<ControllerBase>();
            builder.RegisterComponentInHierarchy<Player>();
        }
    }
}
