using Cysharp.Threading.Tasks;
using Runtime.Executor;
using UnityEngine;
using VContainer;

namespace Runtime
{
    public sealed class GameManager : MonoBehaviour
    {
        private GameStateExecutor _executor;

        [Inject]
        private void Init(GameStateExecutor executor)
        {
            _executor = executor;
        }

        public UniTask ExecuteGame()
        {
            return _executor.Execute(this.GetCancellationTokenOnDestroy());
        }
    }
}
