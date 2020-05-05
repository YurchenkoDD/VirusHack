using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class RoomData
    {
        public string RoomName { get; set; }
        public List<MessageData> Messages { get; set; } = new List<MessageData>();

        public List<RoomUserData> Users { get; set; } = new List<RoomUserData>();

        public RoomData(string roomName) {
            RoomName = roomName;
        }
    }
}
