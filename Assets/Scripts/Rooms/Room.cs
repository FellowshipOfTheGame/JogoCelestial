using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Rooms
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        public UnityEvent onEnableRoom;

        public void SetFollowTransform(Transform target)
        {
            virtualCamera.Follow = target;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            virtualCamera.enabled = true;
            onEnableRoom?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            virtualCamera.enabled = false;
        }
    }
}
