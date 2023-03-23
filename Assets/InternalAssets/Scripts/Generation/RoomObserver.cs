using System;
using TheRat.Helpers;
using TheRat.LocationGeneration;
using UnityEngine;

public class RoomObserver : MonoBehaviour
{
    public event Action<Room> OnRoomEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagHelper.Room))
            OnRoomEntered?.Invoke(collision.GetComponent<Room>());
    }
}