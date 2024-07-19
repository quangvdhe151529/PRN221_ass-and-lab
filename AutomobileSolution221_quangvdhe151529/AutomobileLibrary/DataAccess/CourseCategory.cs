﻿using System;
using System.Collections.Generic;

namespace AutomobileLibrary.DataAccess
{
    public partial class CourseCategory
    {
        public CourseCategory()
        {
            Courses = new HashSet<Course>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
