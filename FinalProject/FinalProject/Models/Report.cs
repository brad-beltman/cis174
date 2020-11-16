using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Report
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Report Type")]
        public int ReportTypeID { get; set; }  // Foreign key property
        [Display(Name = "Report Type")]
        public ReportType ReportType { get; set; }  // Navigation property This is for the report type acronym.  For example, WASA, EPT, IPT, etc.

        public string Name { get; set; }  // This can be automatically determined from the DOCX filename at upload time
        
        [Required(ErrorMessage = "An author name is required, please use 'Multiple' if there is more than one author")]
        public string Author { get; set; }  // This may be set to "Multiple" if there are more than one author

        [Required(ErrorMessage = "Please include the date of the report")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; } = DateTime.Today;  // Reflects the date of the report, not when it was uploaded

        [Required(ErrorMessage = "A file is required")]
        [Display(Name = "File")]
        public string Content { get; set; }  // This holds the report contents
    }
}
