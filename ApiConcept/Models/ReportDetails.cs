using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConcept.Models
{
    public class ReportDetails
    {
        public string Subject { get; set; }
        public int TopicNo { get; set; }
        public string TeacherName { get; set; }
        public string LabDate { get; set; }
        public DateTime DeadlineDate { get; set; }
    }
}
