using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    public class DoorManager : Manager, IDoorManager
    {
        public DoorManager()
        {
            
        }

        public bool LockAllDoors()
        {
            throw new NotImplementedException();
        }

        public bool LockDoor(int doorId)
        {
            throw new NotImplementedException();
        }

        public bool OpenAllDoors()
        {
            throw new NotImplementedException();
        }

        public bool OpenDoor(int doorId)
        {
            throw new NotImplementedException();
        }
    }
}
