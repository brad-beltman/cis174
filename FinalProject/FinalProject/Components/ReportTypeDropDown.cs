using FinalProject.Data;
using FinalProject.Data.Repositories;
using FinalProject.Models;
using FinalProject.Models.DTOs;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Components
{
    public class ReportTypeDropDown : ViewComponent
    {
        private IRepository<ReportType> _data { get; set; }
        public ReportTypeDropDown(IRepository<ReportType> rep) => _data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var reportTypes = _data.List(new QueryOptions<ReportType>
            {
                OrderBy = g => g.Name
            });

            var vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = ReportsGridDTO.DefaultFilter,
                Items = reportTypes.ToDictionary(g => g.ReportTypeID.ToString(), g => g.Name)
            };

            return View("~/Views/Shared/Components/DropDown.cshtml", vm);
        }
    }
}
