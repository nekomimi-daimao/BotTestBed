using System;
using UniRx;

namespace BotTestBed.Runtime.Controller
{
    public sealed class ControllerBot : ControllerBase
    {
        private void OnEnable()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .TakeUntilDisable(this)
                .AsUnitObservable()
                .Subscribe(OnBot);
        }

        private void OnBot(Unit _)
        {
            stickForce.Value = UnityEngine.Random.insideUnitCircle;
            jumpPushed.Value = !jumpPushed.Value;
        }
    }
}
