using System;
using System.Linq;
using UnityEngine;

namespace Runtime.Target
{
    public sealed class TargetHolder : MonoBehaviour
    {
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
                    target.transform.SetParent(parentTs);
                    return target;
                })
                .ToArray();
        }
    }
}
