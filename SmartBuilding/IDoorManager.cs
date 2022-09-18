using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    /// <summary>
    /// Interface for the Door Manager. This interface is used to allow the Building Controller to interact with the Door Manager class without having to know the implementation details of the Door Manager class.
    /// </summary>
    public interface IDoorManager
    {
        /// <summary>
        /// Open the door with given id.
        /// </summary>
        /// <param name="doorId">The unique id of door in the building</param>
        /// <returns>True if the door is opened otherwise false</returns>
        bool OpenDoor(int doorId);

        /// <summary>
        /// Lock the door with given id.
        /// </summary>
        /// <param name="doorId">The unique id of door in the building</param>
        /// <returns>True if the door is Locked otherwise false</returns>
        bool LockDoor(int doorId);

        /// <summary>
        /// Open all the doors in the building.
        /// </summary>
        /// <returns>True if all doors are opened otherwise false</returns>
        bool OpenAllDoors();

        /// <summary>
        /// Lock all the door in the building.
        /// </summary>
        /// <returns>True if all doors are locked otherwise false</returns>
        bool LockAllDoors();
    }
}
