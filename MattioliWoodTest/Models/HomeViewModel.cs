using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MattioliWoodTest.Models
{
    public class HomeViewModel
    {
        //public string forename { get; set; }

        public string ErrorMsg { get; set; }

        public  string UserExistMsg { get; set; }


        public List<Staff> StaffList { get; set; }


        public List<Client> clientList { get; set; }
    }
}
