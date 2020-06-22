using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class Client
    {
     
        public int Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PostCode { get; set; }
    }
}
