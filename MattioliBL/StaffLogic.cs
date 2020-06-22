using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MattioliBL
{

    public class StaffLogic
    {
        private StaffFunctionInterface _staffFunction = new DAL.Functions.StaffFunctions();

        public string AddStaff()
        {
            string staff = _staffFunction.AddStaffInDb();
            return staff;
        }

    }
}
