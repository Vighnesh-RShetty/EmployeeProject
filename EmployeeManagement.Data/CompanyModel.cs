using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Data
{
    public  class CompanyModel
    {
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? SectorId { get; set; }
        public string? ContactNumber { get; set; }
        public string? CountryCode { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? CompanyLogoPath { get; set; }
        public CompanySectorModel CompanySector { get; set; }
        public List<DropdownItem> CompanySectors { get; set; }
        public string SectorName=>CompanySector?.SectorName ?? "No Sector";
        
        public class DropdownItem
        {
            public int Value { get; set; }
            public string? Text { get; set; }
        }

        public class CompanySectorModel
        {
            public int SectorId { get; set; }
            public string SectorName { get; set; }
        }
    }
}
