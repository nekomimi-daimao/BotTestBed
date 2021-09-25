using System.Threading;
using Cysharp.Threading.Tasks;

namespace Runtime.Target.Generate
{
    public interface ITargetGenerator : IStateWorker
    {
        UniTask<Target[]> Generate(CancellationToken token);
    }
}
