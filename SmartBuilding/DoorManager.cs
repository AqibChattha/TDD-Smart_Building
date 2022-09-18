using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    /// <summary>
    /// The Door Manager class is a manager that manages the doors in the building. It is a subclass of the Manager class. It provides the functionality to open or lock the doors.
    /// </summary>
    public class DoorManager : Manager, IDoorManager
    {
        /// <summary>
        /// Default constructor, to be used in testing.
        /// </summary>
        public DoorManager()
        {
            
        }

        /// <summary>
        /// Lock all the door in the building. The method is not implemented as it is only used for testing purposes.
        /// </summary>
        /// <returns>True if all doors are locked otherwise false</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool LockAllDoors()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lock the door with given id. The method is not implemented as it is only used for testing purposes.
        /// </summary>
        /// <param name="doorId">The unique id of door in the building</param>
        /// <returns>True if the door is Locked otherwise false</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool LockDoor(int doorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Open all the doors in the building. The method is not implemented as it is only used for testing purposes.
        /// </summary>
        /// <returns>True if all doors are opened otherwise false</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool OpenAllDoors()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Open the door with given id. The method is not implemented as it is only used for testing purposes.
        /// </summary>
        /// <param name="doorId">The unique id of door in the building</param>
        /// <returns>True if the door is opened otherwise false</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool OpenDoor(int doorId)
        {
            throw new NotImplementedException();
        }
    }
}
