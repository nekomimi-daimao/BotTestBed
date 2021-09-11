using UnityEngine;

namespace Runtime.Target
{
    [RequireComponent(typeof(Collider))]
    public sealed class Target : MonoBehaviour
    {
        public TargetData TargetData;
    }
}
