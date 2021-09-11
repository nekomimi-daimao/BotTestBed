using UnityEngine;
using VContainer;

namespace Runtime.Target
{
    public  sealed class TargetSupplier
    {
        private Target _targetPrefab;

        [Inject]
        private void Init(Target targetPrefab, GameConfiguration gameConfiguration)
        {
            this._targetPrefab = targetPrefab;

            for (var count = 0; count < gameConfiguration.TargetCount; count++)
            {
                var target = Object.Instantiate(_targetPrefab);
            }
        }
    }
}
