using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Runtime.Target
{
    public class TargetGeneratorPrimitive : ITargetGenerator
    {
        public UniTask Generate(TargetData[] data, CancellationToken token)
        {
            var type = PrimitiveType.Sphere;

            foreach (var targetData in data)
            {
                var go = GameObject.CreatePrimitive(type);
                var target = go.AddComponent<Target>();
                target.TargetData = targetData;
                target.Place();
            }

            return UniTask.CompletedTask;
        }
    }
}
