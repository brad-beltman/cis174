using FinalProject.Areas.Admin.Models.DTOs;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Areas.Admin.Models.ViewModels
{
    public class DeleteViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Report Type")]
        public string ReportType { get; set; }
    }
}
