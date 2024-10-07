using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCrafterClassLib.DTO
{
    using System;

    namespace Resume.Models
    {
        public class Resume
        {
            public int ResumeId { get; set; }
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public DateTime UploadDate { get; set; }

            public int JobSeeker_Id { get; set; }
        }
    }

}
