using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    public abstract class Manager
    {
        private bool engineerRequired;

        public Manager()
        {

        }
        
        public virtual string GetStatus()
        {
            return "Status";
        }

        public bool SetEngineerRequired(bool needsEngineer)
        {
            if (engineerRequired != needsEngineer)
            {
                engineerRequired = needsEngineer;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
