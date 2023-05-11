using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myfirstAPI.Models
{
    public class Member
    { 
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string buildingNumber { get; set; }
        public DateTime dateOfBirth 
        {
            get;
            set;
        }
        public string telephone { get; set; }
        public string mobilePhone { get; set; }
    }
    public class VaccineDetails
    {
        public string memberID { get; set; }
        public DateTime vaccDate { get; set; }
        public string vaccProducer { get; set; }
    }

    public class IllnessDetails
    {
        public string memberID { get; set; }
        public DateTime IllDate { get; set; }
        public DateTime RecoveryDate { get; set; }

    }
}