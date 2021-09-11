using System.Threading;
using Cysharp.Threading.Tasks;

namespace Runtime.Target
{
    public interface ITargetGenerator
    {
        UniTask Generate(TargetData[] data, CancellationToken token);
    }
}
