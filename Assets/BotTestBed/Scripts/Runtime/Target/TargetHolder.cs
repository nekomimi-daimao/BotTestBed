using System;
using System.Linq;
using BotTestBed.Runtime.Controller;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Runtime.Target
{
    public sealed class TargetHolder : MonoBehaviour
    {
        public readonly Subject<(TargetData, PlayerColor)> OnTargetGet = new Subject<(TargetData, PlayerColor)>();

        public Target[] Targets { get; private set; } = Array.Empty<Target>();

        public void SetTarget(Target[] targets)
        {
            if (Targets.Any())
            {
                foreach (var t in Targets)
                {
                    if (t == null)
                    {
                        continue;
                    }

                    Destroy(t.gameObject);
                }
            }
            Targets = null;

            var parentTs = this.transform;
            Targets = targets
                .Where(target => target != null)
                .Select(target =>
                {
                    target.OnGet.TakeUntilDestroy(target).Subscribe(tuple => OnTargetGet.OnNext(tuple));
                    target.transform.SetParent(parentTs);
                    return target;
                })
                .ToArray();
        }
    }
}
