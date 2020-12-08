using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Data.Repositories;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UploadController : ControllerBase
    {
        private IReportOps _reportOps { get; set; }
        private IRepository<Report> _reports { get; set; }
        private IRepository<ReportType> _reportTypes { get; set; }

        public UploadController(IRepository<Report> rep, IRepository<ReportType> rt_rep, IReportOps reportOps)
        {
            _reports = rep;
            _reportTypes = rt_rep;
            _reportOps = reportOps;
        }

        // POST: api/Upload/NewReport
        [HttpPost]
        // [Consumes("application/json")]
        [Produces("application/json")]
        [Route("api/Upload/NewReport")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult NewReport([FromForm] [Bind(nameof(Report.Name), nameof(Report.ReportTypeID), nameof(Report.Date), nameof(Report.Content))] Report report, IFormFile file)
        {
            if (file != null)
            {
                if (file.Length > 0)
                {
                    // The file should be 20MB or less
                    if (file.Length < 20000000)
                    {
                        if (Path.GetExtension(file.FileName) == ".docx")
                        {
                            // Using this to dynamically get the file name without asking the user
                            // Encoding so the file name is always safe
                            report.Name = WebUtility.HtmlEncode(Path.GetFileNameWithoutExtension(file.FileName));

                            // Need to write the file as a memorystream and convert it to bytes before base64 encoding for storage
                            byte[] bytes;
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                bytes = ms.ToArray();

                                Dictionary<string, string> content = _reportOps.CreateSearchIndex(bytes);

                                report.Headings = content["headings"];
                                report.SearchIndex = content["content"];
                            }

                            // Convert to base64 for easy storage
                            report.Content = Convert.ToBase64String(bytes);

                            _reports.Insert(report);
                            _reports.Save();

                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }

            return CreatedAtAction("GetReport", new { id = report.ID }, report);
        }
    }
}
