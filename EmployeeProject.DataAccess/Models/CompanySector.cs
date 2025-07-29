using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.DataAccess.Models
{   
    [Table("CompanySector")]
    public class CompanySector
    {
        [Key]
        public int SectorId { get; set; }
        public string SectorName { get; set; }
    }
}
