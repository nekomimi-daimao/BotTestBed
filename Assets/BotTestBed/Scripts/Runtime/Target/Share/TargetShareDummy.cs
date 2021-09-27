using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Executor;
using UnityEngine;

namespace Runtime.Target.Share
{
    public sealed class TargetShareDummy : ITargetSharer
    {
        GameState IStateWorker.GameState => GameState.ShareTarget;

        UniTask IStateWorker.Work(CancellationToken token)
        {
            _targetData = Enumerable.Range(0, 10)
                .Select(i => new TargetData
                {
                    Id = i,
                    Position = new Vector3(UnityEngine.Random.value, 0f, UnityEngine.Random.value)
                })
                .ToArray();
            return UniTask.CompletedTask;
        }

        TargetData[] ITargetSharer.SharedTarget()
        {
            return _targetData;
        }

        private TargetData[] _targetData = Array.Empty<TargetData>();
    }
}
