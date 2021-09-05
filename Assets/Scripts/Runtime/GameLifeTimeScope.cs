using BotTestBed.Runtime.Controller;
using VContainer;
using VContainer.Unity;

namespace BotTestBed.Runtime
{
    public class GameLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentOnNewGameObject<ControllerKeyboard>(Lifetime.Scoped).As<ControllerBase>();
            builder.RegisterComponentInHierarchy<Player>();
        }
    }
}
