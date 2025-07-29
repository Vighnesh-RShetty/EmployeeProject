using EmployeeProject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.DataAccess.Models
{
    [Table("Company")]
    public  class Company
    {
            [Key]
            public int CompanyId { get; set; }
            public string? CompanyName { get; set; }
            public string? ContactNumber { get; set; }
            public string? CountryCode { get; set; }
            public string? Email { get; set; }
            [DataType(DataType.Password)]
            public string? Password {  get; set; }
            [DataType(DataType.Password)]
            [NotMapped]
            public string? ConfirmPassword { get; set; }
           public string? CompanyLogoPath { get; set; }


           [ForeignKey("CompanySector")]
           public int SectorId { get; set; }
           public CompanySector CompanySector { get; set; }
    }
    }
