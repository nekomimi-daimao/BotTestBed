using System;
using UnityEngine;

namespace Runtime.Target
{
    [RequireComponent(typeof(Collider))]
    public sealed class Target : MonoBehaviour
    {
        public TargetData TargetData;

        public void Place()
        {
            this.transform.SetPositionAndRotation(TargetData.Position, Quaternion.identity);
        }
    }
}
