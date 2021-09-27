using UnityEngine;
using UniRx;

namespace BotTestBed.Runtime.Controller
{
    public abstract class ControllerBase : MonoBehaviour
    {
        public  Vector2ReactiveProperty stickForce = new Vector2ReactiveProperty();

        public IReadOnlyReactiveProperty<Vector2> StickForce => stickForce;

        public  BoolReactiveProperty jumpPushed = new BoolReactiveProperty();

        public IReadOnlyReactiveProperty<bool> JumpPushed => jumpPushed;
    }
}
