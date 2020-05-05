using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Controllers;

namespace WpfApp.Models
{
    public class MainData
    {
        private static List<RoomData> rooms = new List<RoomData>();
        private static RoomData currentRoom = null;

        public RoomData[] Rooms { get => rooms.ToArray(); }
        public RoomData CurrentRoom { get => currentRoom; }

        public event Action OnRoomChange;
        public event Action OnNewCurrentRoom;

        public void AddRoom(RoomData room) {
            rooms.Add(room);
            AppController.SaveRoom(room);
            OnRoomChange?.Invoke();
        }

        public void RemoveRoom(RoomData room) {
            rooms.Remove(room);
            OnRoomChange?.Invoke();
        }

        public void SelectRoom(RoomData room) {
            if (currentRoom != null) {
                AddRoom(currentRoom);
            }
            currentRoom = room;
            if (room != null)
            {
                RemoveRoom(room);
            }
            OnNewCurrentRoom?.Invoke();
        }
    }
}
