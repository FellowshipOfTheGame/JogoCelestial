using System;
using UnityEngine;

namespace Rooms
{
    public class RoomsController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Room roomOne;
        [SerializeField] private Room roomTwo;

        private float _curRoomX;
        private float _quarter;
        private float _displacement;
        private const float Tolerance = .5f;
        
        private void Start()
        {
            var roomCollider = roomOne.GetComponent<Collider2D>();
            _displacement = roomCollider.bounds.size.x + .1f;
            _quarter = roomCollider.bounds.extents.x / 2;
            _curRoomX = roomOne.transform.position.x;

            roomOne.SetFollowTransform(target);
            roomTwo.SetFollowTransform(target);
            
            roomOne.onEnableRoom.AddListener(delegate { _curRoomX = roomOne.transform.position.x; });
            roomTwo.onEnableRoom.AddListener(delegate { _curRoomX = roomTwo.transform.position.x; });
        }

        //TODO: Almost there. There is something causing the wrong room to move
        private void Update()
        {
            CheckSideForRoomPosition(
                Math.Abs(_curRoomX - roomOne.transform.position.x) < Tolerance ? roomTwo : roomOne);
        }

        private void CheckSideForRoomPosition(Room room)
        {
            if (target.position.x >= _curRoomX + _quarter) // Most right side
            {
                if (_curRoomX + _displacement > room.transform.position.x)
                    SetRoomPosition(room, true);
            }
            else if (target.position.x <= _curRoomX - _quarter) // Most left side
            {
                if (_curRoomX - _displacement < room.transform.position.x)
                    SetRoomPosition(room, false);
            }
        }

        private void SetRoomPosition(Room room, bool right)
        {
            var currentPosition = room.transform.position;
            room.transform.position = new Vector3(currentPosition.x + (right ? 1 : -1) * _displacement,
                currentPosition.y, currentPosition.z);
        }
    }
}