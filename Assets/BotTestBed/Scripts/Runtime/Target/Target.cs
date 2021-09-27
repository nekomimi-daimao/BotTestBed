using System;
using BotTestBed.Runtime;
using BotTestBed.Runtime.Controller;
using UniRx;
using UnityEngine;

namespace Runtime.Target
{
    [RequireComponent(typeof(Collider))]
    public sealed class Target : MonoBehaviour
    {
        public TargetData TargetData;

        public IObservable<(TargetData, PlayerColor)> OnGet => _onGet;
        private readonly Subject<(TargetData, PlayerColor)> _onGet = new Subject<(TargetData, PlayerColor)>();

        public void Place()
        {
            this.transform.SetPositionAndRotation(TargetData.Position, Quaternion.identity);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }
            var player = other.gameObject.GetComponent<Player>();
            if (player == null)
            {
                return;
            }
            _onGet.OnNext((TargetData, player.PlayerColor));
        }
    }
}
