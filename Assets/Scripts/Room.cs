using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnableVirtualCamera(other, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnableVirtualCamera(other, false);
    }

    private void EnableVirtualCamera(Collider2D other, bool enable)
    {
        if (!other.CompareTag("Player")) return;
        virtualCamera.enabled = enable;
    }
}
