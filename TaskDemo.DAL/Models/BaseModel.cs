using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTask.DAL.Models
{
    public class BaseModel
    {
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt = DateTime.Now;
        public string CreatedBy { get; set; }
    }
}
