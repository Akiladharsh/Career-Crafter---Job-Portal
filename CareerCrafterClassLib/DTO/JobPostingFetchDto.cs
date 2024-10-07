

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.DTO
{
    public class JobPostingFetchDto
    {
        public string OrganisationName { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }

        public int EmployerId { get; set; }

        public int JobPostingId { get; set; }
    }

}
