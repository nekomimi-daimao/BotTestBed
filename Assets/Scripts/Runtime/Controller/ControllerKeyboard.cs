using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace BotTestBed.Runtime.Controller
{
    public sealed class ControllerKeyboard : ControllerBase
    {
        private void OnEnable()
        {
            this.UpdateAsObservable()
                .TakeUntilDestroy(this)
                .Subscribe(CheckInput);
        }

        private void CheckInput(Unit _)
        {
            var stick = Vector2.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                stick += Vector2.right;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                stick += Vector2.left;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                stick += Vector2.up;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                stick += Vector2.down;
            }

            stickForce.Value = stick.normalized;

            jumpPushed.Value = Input.GetKey(KeyCode.Z);
        }
    }
}
