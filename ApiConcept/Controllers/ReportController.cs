using ApiConcept.Models;
using ApiConcept.Repos;
using Microsoft.AspNetCore.Mvc;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiConcept.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController: ControllerBase
    {
        private readonly IReportGenerator _generator;
        public ReportController(IReportGenerator generator)
        {
            _generator = generator;
        }
        
        [HttpGet("values")]
        public IActionResult GetDefaultValues()
        {
            ObjWrapper wrapper = _generator.GetDefaultObject();
            string t=JsonSerializer.Serialize<ObjWrapper>(wrapper);
            return Ok(t);
        }
        [HttpGet("default")]
        public FileResult GenerateDefaultReport()
        {
            return File(_generator.GenerateDefaultReport().ToArray(), "application/pdf", "generatedReport.pdf");
        }
        [HttpGet("template")]
        public ActionResult ShowTemplate()
        {
            return File(_generator.GetTemplate().ToArray(), "application/pdf", "template.pdf");
        }

        [HttpPost("student/")]
        public ActionResult GenerateStudentReport([FromQuery]string name, [FromQuery]string major, [FromQuery]string studies, [FromQuery] uint semester)
        {
            Student student = new Student
            {
                Name = name,
                Group = 6,
                Section = 2,
                Major = major,
                TypeOfStudies = studies,
                Semester = semester
            };
            return File(_generator.GenerateStudentReport(student).ToArray(), "application/pdf", "generatedReport.pdf");
        }
        [HttpPost("generate/json")]
        public ActionResult GenerateReportJson([FromBody]ObjWrapper obj)
        {
            try
            {
                return File(_generator.GenerateCleanReport(obj.student, obj.details).ToArray(), "application/pdf", "generatedReport.pdf");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            /*try
            {
                //ObjWrapper obj = (ObjWrapper)JsonSerializer.Deserialize<ObjWrapper>(json);
                return File(_generator.GenerateCleanReport(obj.student, obj.details), "application/pdf", "generatedReport.pdf");
            }
            catch(JsonException ex)
            {
                return BadRequest(ex.ToString());
            }*/

        }
    }
}
