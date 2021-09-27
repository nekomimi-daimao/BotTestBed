using Cysharp.Threading.Tasks;
using Runtime.Target;
using UniRx;

namespace Runtime.Network
{
    public interface INetworkNotifier
    {
        public IntReactiveProperty Time();

        public UniTask GetItem(TargetData targetData);
    }
}
