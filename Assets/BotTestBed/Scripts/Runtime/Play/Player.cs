using System;
using System.Threading;
using BotTestBed.Runtime.Controller;
using Cysharp.Threading.Tasks;
using Runtime;
using UniRx;
using UnityEngine;
using VContainer;

namespace BotTestBed.Runtime
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public sealed class Player : MonoBehaviour
    {
        private ControllerBase _controller;

        [Inject]
        private void Init(ControllerBase controller, GameConfiguration configuration)
        {
            this._controller = controller;

            this._rigidbody = GetComponent<Rigidbody>();
            this._collider = GetComponent<Collider>();

            var token = this.GetCancellationTokenOnDestroy();

            OnStickForceAsync(token).Forget();
            OnJumpAsync(token).Forget();
            ResetPositionAsync(token).Forget();
        }

        public PlayerColor PlayerColor;

        private Rigidbody _rigidbody;
        private Collider _collider;

        private const float MoveForce = 10f;
        private const float ForceRatio = 0.3f;

        private async UniTaskVoid OnStickForceAsync(CancellationToken token)
        {
            var stickInput = _controller.StickForce;

            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await UniTask.WaitForFixedUpdate();

                var input = stickInput.Value;
                if (input == Vector2.zero)
                {
                    continue;
                }

                var v = new Vector3(input.x, 0f, input.y);
                if (v.sqrMagnitude > 1f)
                {
                    v = v.normalized;
                }
                _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, v * MoveForce, ForceRatio);
            }
        }


        private const float JumpForce = 20f;
        private const float JumpInterval = 0.5f;

        private async UniTaskVoid OnJumpAsync(CancellationToken token)
        {
            var jumpInput = _controller.JumpPushed;

            while (true)
            {
                var input = await jumpInput;

                if (token.IsCancellationRequested)
                {
                    break;
                }

                if (!input)
                {
                    continue;
                }

                var velocity = _rigidbody.velocity;
                velocity.y = 0f;
                _rigidbody.velocity = velocity;
                _rigidbody.AddForce(Vector3.up * JumpForce);

                await UniTask.Delay(TimeSpan.FromSeconds(JumpInterval), cancellationToken: token);
            }
        }

        private async UniTaskVoid ResetPositionAsync(CancellationToken token)
        {
            var origin = Vector3.up;
            var distanceLimit = Mathf.Pow(12f, 2f);

            var ts = this.transform;

            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);

                if (!((ts.position - origin).sqrMagnitude > distanceLimit))
                {
                    continue;
                }

                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
                ts.SetPositionAndRotation(origin, Quaternion.identity);
            }
        }
    }
}
