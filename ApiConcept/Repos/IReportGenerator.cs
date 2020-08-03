using ApiConcept.Models;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConcept.Repos
{
    public interface IReportGenerator
    {
        MemoryStream GenerateDefaultReport();
        MemoryStream GenerateStudentReport(Student student);
        MemoryStream GenerateCleanReport(Student student, ReportDetails details);
        MemoryStream GetTemplate();
        ObjWrapper GetDefaultObject();
    }
}
