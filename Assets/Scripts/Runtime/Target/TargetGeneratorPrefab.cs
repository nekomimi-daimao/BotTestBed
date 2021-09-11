using System.Threading;
using Cysharp.Threading.Tasks;
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

        public async UniTask Generate(TargetData[] data, CancellationToken token)
        {
            for (var count = 0; count < data.Length; count++)
            {
                await UniTask.Yield();
            }
        }
    }
}
