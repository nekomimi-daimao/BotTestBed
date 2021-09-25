using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Runtime
{
    public class GameStateExecutor
    {
        public static readonly Dictionary<GameState, WaitType> ExecutionState = new Dictionary<GameState, WaitType>
        {
            { GameState.ShareTarget, WaitType.All },
            { GameState.GenerateTarget, WaitType.All },
        };

        private readonly IStateWorker[] _stateWorker;

        [Inject]
        public GameStateExecutor(IStateWorker[] workers)
        {
            _stateWorker = workers;
        }

        private async UniTask Execute(CancellationToken baseToken)
        {
            foreach (var pair in ExecutionState)
            {
                var state = pair.Key;
                var waitType = pair.Value;

                var source = CancellationTokenSource.CreateLinkedTokenSource(baseToken);
                var token = source.Token;

                var workArray = _stateWorker
                    .Where(worker => worker.GameState.HasFlag(state))
                    .Select(worker => worker.Work(token))
                    .ToArray();

                switch (waitType)
                {
                    case WaitType.All:
                        await UniTask.WhenAll(workArray);
                        break;
                    case WaitType.Any:
                        await UniTask.WhenAny(workArray);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (!source.IsCancellationRequested)
                {
                    source.Cancel();
                }
            }
        }
    }
}
