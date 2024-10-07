using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.DTO
{
    public class ExperienceDto
    {
        public string PreviousCompanyName { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
    }
}