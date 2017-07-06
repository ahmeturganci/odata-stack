using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace odata_stack.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public int CategoryId { get; set; }
        public int YearId { get; set; }

        public Project CopyFrom(Project prj)
        {
            this.Name = prj.Name;
            this.Summary = prj.Summary;
            this.CategoryId = prj.CategoryId;
            this.YearId = prj.YearId;
            return this;
        }

    }
}