using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    abstract class Manager
    {
        private bool engineerRequired;

        public Manager()
        {

        }

        public string GetStatus()
        {
            return null;
        }

        public bool SetEngineerRequired(bool needsEngineer)
        {
            engineerRequired = needsEngineer;
            return true;
        }
    }
}
