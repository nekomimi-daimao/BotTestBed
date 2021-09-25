using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Target.Share;
using UnityEngine;

namespace Runtime.Target.Generate
{
    public sealed class TargetGeneratorPrimitive : ITargetGenerator
    {
        private readonly ITargetSharer _targetSharer;
        private readonly TargetHolder _targetHolder;

        public TargetGeneratorPrimitive(ITargetSharer targetSharer, TargetHolder targetHolder)
        {
            _targetSharer = targetSharer;
            _targetHolder = targetHolder;
        }

        public GameState GameState => GameState.GenerateTarget;

        public async UniTask Work(CancellationToken token)
        {
            var targets = await Generate(token);
            _targetHolder.SetTarget(targets);
        }

        public async UniTask<Target[]> Generate(CancellationToken token)
        {
            var listResult = new List<Target>();

            var type = PrimitiveType.Sphere;
            var data = _targetSharer.SharedTarget();
            foreach (var targetData in data)
            {
                await UniTask.Yield();
                var go = GameObject.CreatePrimitive(type);
                var target = go.AddComponent<Target>();
                target.TargetData = targetData;
                target.Place();

                listResult.Add(target);
            }

            return listResult.ToArray();
        }
    }
}
