using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConcept.Models
{
    public class Student
    {
        public string Name { get; set; }
        public uint Group  { get; set; }
        public uint Section { get; set; }
        public string Major { get; set; }
        public string TypeOfStudies { get; set; }
        public uint Semester { get; set; }
    }
}
