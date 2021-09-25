using System.Threading;
using Cysharp.Threading.Tasks;

namespace Runtime
{
    public interface IStateWorker
    {
        public GameStateExecutor.GameState GameState { get; }

        public UniTask Work(CancellationToken token);
    }
}
