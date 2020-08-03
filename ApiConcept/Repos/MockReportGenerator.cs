using ApiConcept.Models;
using Spire.Pdf;
using Spire.Pdf.General.Find;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConcept.Repos
{
    public class MockReportGenerator : IReportGenerator
    {
        private Student defaultStudent;
        private ReportDetails defaultDetails;
        private PdfDocument template;

        public MockReportGenerator()
        {
            defaultStudent = new Student { Name = "Jonatan Chrobak", Semester = 7, Group = 6, Section = 2, Major = "Informatyka", TypeOfStudies = "SSI" };
            defaultDetails = new ReportDetails { Subject = "Metod Statystycznych", TeacherName = "Krzysztof Pawelczyk", TopicNo = 18, LabDate = "Sroda, 13:15-14:45", DeadlineDate = new DateTime(2018, 6, 20) };
            template = new PdfDocument("C:\\Users\\Lenovo\\source\\repos\\ApiConcept\\ApiConcept\\Templates\\template.pdf");
        }

        public MemoryStream GenerateDefaultReport()
        {
            return FillReport(defaultStudent, defaultDetails);
        }

        public MemoryStream FillReport(Student student, ReportDetails details)
        {
            
            List<string> tagsToFind= new List<string>() { "$subject","$topicno","$author","$teacher","$year","$major","$typeofstudies","$semester","$labdate","$group","$section","$deadlinedate","$returneddate"};
            foreach(String tag in tagsToFind)
            {
                PdfPageBase page = template.Pages[0];
                PdfTextFind location = page.FindText(tag,TextFindParameter.None)?.Finds[0];
                if(location!=null)
                {
                    string replacement;
                    PdfTrueTypeFont font;
                    switch (tag)
                    {
                        case "$subject": replacement = details.Subject;
                            font = new PdfTrueTypeFont(new Font("Calibri", 18f, FontStyle.Bold)); break;
                        case "$topicno": replacement = details.TopicNo.ToString(); 
                            font = new PdfTrueTypeFont(new Font("Calibri", 18f, FontStyle.Regular));break;
                        case "$author": replacement = student.Name;
                            font = new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Regular)); break;
                        case "$teacher": replacement = details.TeacherName;
                            font = new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Regular)); break;
                        case "$year": replacement = DateTime.Now.Month < 10 ? (DateTime.Now.Year - 1).ToString() + "/" + DateTime.Now.Year.ToString() : DateTime.Now.Year.ToString() + "/" + (DateTime.Now.Year + 1).ToString();
                            font = new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Regular)); break;
                        case "$major": replacement = student.Major;
                            font = new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Regular)); break;
                        case "$typeofstudies": replacement=student.TypeOfStudies;
                            font = new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Regular)); break;
                        case "$semester": replacement = student.Semester.ToString();
                            font = new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Regular)); break;
                        case "$labdate": replacement = details.LabDate.ToString();
                            font = new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Regular)); break;
                        case "$group": replacement = student.Group.ToString();
                            font = new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Regular)); break;
                        case "$section": replacement = student.Section.ToString();
                            font = new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Regular)); break;
                        case "$deadlinedate": replacement = details.DeadlineDate.ToString("dd/MM/yy");
                            font = new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Regular)); break;
                        case "$returneddate": replacement=DateTime.Now.ToString("dd/MM/yy");
                            font = new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Regular)); break;
                        default: replacement = "ERROR"; font=new PdfTrueTypeFont(new Font("Calibri", 12f, FontStyle.Underline));break;
                    }
                    RectangleF rec = location.Bounds;
                    page.Canvas.DrawRectangle(PdfBrushes.White, rec);
                    page.Canvas.DrawString(replacement, font, PdfBrushes.Black, new PointF(rec.Left,rec.Top-3f) );
                }
            }
            //template.SaveToFile("C:\\Users\\Lenovo\\source\\repos\\ApiConcept\\ApiConcept\\Templates\\report.pdf");
            
            MemoryStream stream = new MemoryStream();
            template.SaveToStream(stream);
            return stream;
        }

        public MemoryStream GenerateStudentReport(Student student)
        {
            return FillReport(student,defaultDetails);
        }

        public ObjWrapper GetDefaultObject()
        {
            return new ObjWrapper { student = defaultStudent, details = defaultDetails };
        }

        public MemoryStream GenerateCleanReport(Student student, ReportDetails details)
        {
            return FillReport(student, details);
        }

        public MemoryStream GetTemplate()
        {
            MemoryStream stream = new MemoryStream();
            template.SaveToStream(stream);
            return stream;
        }
    }
}
