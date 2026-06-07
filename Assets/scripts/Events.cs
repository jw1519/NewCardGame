using System;
using UnityEngine;

public static class Events
{
    public static event Action<int, int> roomCleared; // Event to notify when a room is cleared, passing the room's grid coordinates

    public static void OnRoomCleared(int x, int y)
    {
        roomCleared?.Invoke(x, y); // Invoke the event, passing the room's grid coordinates
    }
}
