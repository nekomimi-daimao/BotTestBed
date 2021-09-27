using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Executor;

namespace Runtime.Target.Generate
{
    public interface ITargetGenerator : IStateWorker
    {
        UniTask<Target[]> Generate(CancellationToken token);
    }
}
