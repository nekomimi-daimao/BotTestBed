using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Runtime.Target
{
    public sealed class TargetGeneratorPrefab : ITargetGenerator
    {
        private readonly Target _targetPrefab;
        private readonly GameConfiguration _gameConfiguration;

        [Inject]
        public TargetGeneratorPrefab(Target targetPrefab, GameConfiguration gameConfiguration)
        {
            _targetPrefab = targetPrefab;
            _gameConfiguration = gameConfiguration;
        }

        public UniTask Generate(TargetData[] data, CancellationToken token)
        {
            foreach (var t in data)
            {
                var go = Object.Instantiate(_targetPrefab);
                go.TargetData = t;
                go.Place();
            }

            return UniTask.CompletedTask;
        }
    }
}
