using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Runtime.Target
{
    public sealed class TargetPlaceRandom : ITargetPlaceLogic
    {
        private readonly GameConfiguration _gameConfiguration;

        [Inject]
        public TargetPlaceRandom(GameConfiguration gameConfiguration)
        {
            _gameConfiguration = gameConfiguration;
        }

        public TargetData[] Place()
        {
            var amount = _gameConfiguration.TargetCount;

            var list = new List<Vector2>();

            var retryCount = 0;
            while (true)
            {
                if (list.Count >= amount || retryCount > 100)
                {
                    break;
                }

                var v2 = UnityEngine.Random.insideUnitCircle * 9f;
                if (list.Any(vector2 => (vector2 - v2).sqrMagnitude < 1f))
                {
                    retryCount++;
                    continue;
                }
                list.Add(v2);
            }

            return list
                .Select((vector2, i) => new TargetData
                {
                    Id = i,
                    Position = new Vector3(vector2.x, 1f, vector2.y),
                })
                .ToArray();
        }
    }
}
