using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    public interface IDoorManager
    {
        bool OpenDoor(int doorId);
        bool LockDoor(int doorId);
        bool OpenAllDoors();
        bool LockAllDoors();
    }
}
