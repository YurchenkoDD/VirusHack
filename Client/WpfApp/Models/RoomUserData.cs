using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class RoomUserData : IComparable
    {
        public string UserName { get; private set; }
        public RoomUserData(string userName) {
            UserName = userName;
        }
        public int CompareTo(object o)
        {
            RoomUserData p = o as RoomUserData;
            if (p != null)
                return this.UserName.CompareTo(p.UserName);
            else
                throw new Exception("Невозможно сравнить два объекта");
        }
    }
}
