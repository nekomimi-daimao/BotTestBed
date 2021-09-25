using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Runtime
{
    public class GameStateExecutor
    {
        [Flags]
        public enum GameState
        {
            Default,
            Wait,
            Generate,
            Start,
            Playing,
            Finish,
            Result,
        }

        public enum WaitType
        {
            All,
            Any,
        }

        private IStateWorker[] _stateWorker;

        private async UniTask Execute(CancellationToken baseToken)
        {
            var states = Enum.GetValues(typeof(GameState)).Cast<GameState>();

            foreach (var s in states)
            {
                var source = CancellationTokenSource.CreateLinkedTokenSource(baseToken);
                var token = source.Token;

                var workArray = _stateWorker
                    .Where((worker => worker.GameState.HasFlag(s)))
                    .Select(worker => worker.Work(token))
                    .ToArray();

                await UniTask.WhenAll(workArray);

                await UniTask.WhenAny(workArray);

                if (!source.IsCancellationRequested)
                {
                    source.Cancel();
                }
            }
        }
    }
}
