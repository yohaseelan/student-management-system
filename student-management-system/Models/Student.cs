using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_management_system.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

      public string  Gender { get; set; }
        public string Address {get;set;}
        public string Email {get;set;}
        public string Phone { get; set; }
        public string EnrollmentDate { get; set; }
        public string CreatedAt { get; set; }
    }
}
