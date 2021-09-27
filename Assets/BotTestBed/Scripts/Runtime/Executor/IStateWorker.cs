using System.Threading;
using Cysharp.Threading.Tasks;

namespace Runtime.Executor
{
    public interface IStateWorker
    {
        public GameState GameState { get; }

        public UniTask Work(CancellationToken token);
    }
}
